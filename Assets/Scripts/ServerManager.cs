using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;
using System;
// using static UnityEditor.Progress;
using System.IO;

public class ServerManager : NetworkBehaviour
{
    public readonly SyncList<CardAttributes> PackCards = new SyncList<CardAttributes>();
    public GameCard CurrentGame;
    public List<CardAttributes> CardVars;
    public readonly SyncList<CardAttributes> Discard = new SyncList<CardAttributes>();
    public readonly SyncList<CardList> Inventorys = new SyncList<CardList>();
    public readonly SyncList<HandList> Hands = new SyncList<HandList>();
    public readonly SyncList<HealthList> Healths = new SyncList<HealthList>();


    public List<RangePlayers> rangePlayers = new List<RangePlayers>();


    private Coroutine Coroutine;

    private int TempHealth;

    public struct CardList
    {
        public List<CardAttributes> Cards;
        public uint Id;

        public CardList(uint id, List<CardAttributes> inv)
        {
            Id = id;
            Cards = inv;

        }
    }
    public struct HandList
    {
        public List<CardAttributes> Cards;
        public uint Id;

        public HandList(uint id, List<CardAttributes> inv)
        {
            Id = id;
            Cards = inv;

        }
    }

    public struct HealthList
    {
        public uint Id;
        public int Health;

        public HealthList(uint id, int health)
        {
            Id = id;
            Health = health;
        }
    }


    [SyncVar]
    public uint turnPlayerId;

    [SyncVar]
    public uint attackedPlayerId;

    [SyncVar]
    public uint duelTargetPlayerId;

    [SyncVar]
    public string turnModificator = "No";

    int Move, MoveTime = 30;
    private List<Vector2> spawnPoint;
    public class GameCard
    {
        public List<CardAttributes> Pack;



        public GameCard()
        {
            Pack = GivePack();
        }
        List<CardAttributes> GivePack()
        {
            List<CardAttributes> list = new List<CardAttributes>();
            for (int i = 0; i < 22; i++)
            {
                int card = UnityEngine.Random.Range(0, CardDesk.AllServerCards.Count);
                list.Add(CardDesk.AllServerCards[card]);
                CardDesk.AllServerCards.RemoveAt(card);
            }
            return list;
        }

    }
    [Server]
    public void CardAdded(CardAttributes item)
    {
        PackCards.Add(item);
    }
    [Command(requiresAuthority = false)]
    public void CmdCardAdded()
    {
        if (PackCards.Count == 0)
        {
            CurrentGame = new GameCard();
            foreach (var item in CurrentGame.Pack)
            {
                CardAdded(item);
            }
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (isServer)
            {
                GiveHandCards(PackCards, player.GetComponent<NetworkIdentity>().netId, 4);
                if (player.GetComponent<NetworkIdentity>().netId == 1)
                {
                    GiveTurn(player.GetComponent<NetworkIdentity>().netId, false);
                }
            }
        }
    }
    [Client]
    void Update()
    {
        /*        foreach (var item in Healths)
                {
                    print($"{item.Id} have {item.Health} health   {Healths.Count}");
                }*/
        /*        print(PackCards.Count + " " + "ServerManager");
                print($"{turnPlayerId}");
                print($"{Discard.Count} Discard");*/
        /*        foreach (var item in Hands)
                {
                    print($"{item.Cards.Count} in {item.Id} hand");
                    foreach (var item1 in item.Cards)
                    {
                        print($"{item1.Name} card in hand {item.Id}");
                    }
                }*/
        /*        foreach (var item in Inventorys)
                {
                    print($"{item.Cards.Count} cards have {item.Id} player in inventory");
                    foreach (var item1 in item.Cards)
                    {
                        print($"{item.Id} player inventory have {item1.Name} card. In array {item.Cards.Count} cards");
                    }
                }*/
        foreach (var item in FindObjectsOfType<PlayerNetworkController>())
        {
            Debug.LogWarning($"range {item.Range} to {item.netId} player");
        }
    }
    public struct RangePlayers
    {
        public int Range;
        public Vector2 Pos;
        public RangePlayers(int range, Vector2 pos)
        {
            Range = range;
            Pos = pos;
        }
    }
    void Start()
    {
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        if (isServer) 
        {
            CmdCardAdded();
        }
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        if (FindObjectOfType<NetworkManagerCard>().numPlayers <= 4)
        {
            rangePlayers = new List<RangePlayers> { new RangePlayers(0, new Vector2(0, -237)), 
                new RangePlayers(1, new Vector2(-696, 0)),
                new RangePlayers(2, new Vector2(0, 421)),
                new RangePlayers(1, new Vector2(696, 0))
            };
        }
        /*        spawnPoint = new List<Vector2>() { new Vector2(0, -237), new Vector2(-696, 0), new Vector2(0, 421), new Vector2(696, 0) };*/
        var localPlayerId = (uint)0;
        foreach (var item in players)
        {
            if (item.isLocalPlayer)
            {
                localPlayerId = item.netId; break;
            }
        }
        for (int i = (int)localPlayerId; rangePlayers.Count < 1 ; i++)
        {
            if (i >= FindObjectOfType<NetworkManagerCard>().numPlayers) i = 1;
            foreach (var player in players)
            {
                if (player.netId == localPlayerId)
                {
                    player.setPlayerPosition(rangePlayers[0].Range, rangePlayers[0].Pos);
                    // rangePlayers.RemoveAt(0);
                    break;
                }
                else if (player.netId == i)
                {
                    player.setPlayerPosition(rangePlayers[1].Range, rangePlayers[1].Pos);
                    rangePlayers.RemoveAt(1);
                    break;
                }
            }
        }
        foreach (var player in players)
        {
            if(isServer) Healths.Add(new HealthList(player.netId, 4));
            if (player.isLocalPlayer)
            {
                print(player.netId);
                player.setPlayerPosition(rangePlayers[0].Range, rangePlayers[0].Pos);
            }
            else
            {
                print(player.netId);
                player.setPlayerPosition(rangePlayers[1].Range, rangePlayers[1].Pos);
                rangePlayers.RemoveAt(1);
            }
        }

    }
    [Server]
    IEnumerator MoveFunc()
    {
        MoveTime = 15;
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        while (MoveTime-- > 0)
        {
            foreach (var player in players)
            {
                if(attackedPlayerId != 0)
                    player.TimerUpdateClientRpc(MoveTime, attackedPlayerId);
                else
                    player.TimerUpdateClientRpc(MoveTime, turnPlayerId);
            }
            yield return new WaitForSeconds(1);
        }
        ChangeMove();
    }

    [Server]
    public void ChangeMove()
    {
        duelTargetPlayerId = 0;
        if(Coroutine != null) StopCoroutine(Coroutine);
        foreach (var player in FindObjectsOfType<PlayerNetworkController>())
        {
            if(player.netId == turnPlayerId)
            {
                player.EndTurnClientRpc();
                break;
            }
        }

        if (attackedPlayerId != 0)
        {
            foreach (var item in Healths)
            {
                print($"now {item.Id} and attacked {attackedPlayerId}");
                if (item.Id == attackedPlayerId)
                {
                    Healths[Healths.IndexOf(item)]= new HealthList(attackedPlayerId, item.Health-1);
                    foreach (var item1 in FindObjectsOfType<PlayerNetworkController>())
                    {
                        item1.HealthUpdateClientRpc(attackedPlayerId, item.Health - 1);
                    }
                }
            }
            GiveTurn(turnPlayerId, false);
        }
        else if (turnPlayerId + 1 > FindObjectOfType<NetworkManagerCard>().numPlayers)
            GiveTurn(1, false);
        else GiveTurn(turnPlayerId + 1, false);

        GiveHandCards(PackCards, turnPlayerId, 2);
    }

    [Server]
    public void GiveTurn(uint id, bool target)
    {
        if (Coroutine != null) StopCoroutine(Coroutine);

        attackedPlayerId = 0;
        turnModificator = "No";
        if (!target)
        {
            turnPlayerId = id;
        }
        else
        {
            attackedPlayerId = id;
            // turnModificator = "Attack";
        }

        Coroutine = StartCoroutine(MoveFunc());
    }
    [Server]
    public void GiveHandCards(SyncList<CardAttributes> pack, uint id, int cardsCount) // ���������� ���� � ����
    {
        int i = 0;
        while (i++ < cardsCount)
            GiveCardToHand(pack, id);
        
    }
    [Server]
    void GiveCardToHand(SyncList<CardAttributes> pack, uint id) // ������ ����� � ����
    {
        if (pack.Count == 0)
            return;
        bool check = false;
        List<CardAttributes> list = new List<CardAttributes> { pack[0] };
        foreach (var item in Hands)
        {
            if (id == item.Id)
            {
                check = true; break;
            }
        }
        if (check)
        {
            foreach (var hand in Hands)
            {
                if (hand.Id == id)
                {
                    List<CardAttributes> dumpList = new List<CardAttributes>();
                    foreach (var card in hand.Cards)
                    {
                        dumpList.Add(card);
                    }
                    dumpList.Add(pack[0]);
                    Hands[Hands.IndexOf(hand)] = new HandList(id, dumpList);
                }
            }
        }
        else
        {
            Hands.Add(new HandList(id, list));
        }
        FindObjectOfType<PlayerNetworkController>().GiveHandCardsClientRpc(id, pack[0]);
        pack.Remove(pack[0]);

    }
    [Server]
    public void UpdateInventory(CardAttributes card, PlayerNetworkController playerController, Transform playerInventory)
    {
        bool check = false;
        List<CardAttributes> list = new List<CardAttributes> { card };
        foreach (var item in Inventorys)
        {
            if (playerController.netId == item.Id)
            {
                check = true; break;
            }
        }
        if (check)
        {
            foreach (var inventory in Inventorys)
            {
                if (inventory.Id == playerController.netId)
                {
                    List<CardAttributes> dumpList = new List<CardAttributes>();
                    foreach (var item in inventory.Cards)
                    {
                        dumpList.Add(item);
                    }
                    dumpList.Add(card);
                    Inventorys[Inventorys.IndexOf(inventory)] = new CardList(playerController.netId, dumpList);
                }
            }
        }
        else
        {
            FindObjectOfType<ServerManager>().Inventorys.Add(new CardList(playerController.netId, list));
        }
        var players = FindObjectsOfType<PlayerNetworkController>();
        foreach (var player in players)
        {
            if (player.GetComponent<NetworkIdentity>().netId == playerController.netId)
            {
                player.UpdateInvClientRpc(playerController, card, playerInventory);
            }

        }
    }
    [Server]
    public void AttackAction(uint id, string card)
    {
        print($"{id} has been attacked");
        GiveTurn(id, true);
        turnModificator = card;
    }

    [Server]
    public void DuelAction (uint idAttacking, uint idDefenser)
    {
        duelTargetPlayerId = idDefenser;
        GiveTurn(idDefenser, true);
        turnModificator = "Duel";
    }
    [Server]
    public IEnumerator MassiveAttackAction(string card)
    {
        foreach (var item in FindObjectsOfType<PlayerNetworkController>())
        {
            if (item.netId != turnPlayerId)
            {
                GiveTurn(item.netId, true);
                turnModificator = card;
                while (attackedPlayerId != 0)
                {
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }

    [Server]
    public void RemoveCardFromHand(CardAttributes card, uint id)
    {
        foreach (var item in Hands)
        {
            if(item.Id == id)
            {
                List<CardAttributes> list = new List<CardAttributes>();
                foreach(var item1 in item.Cards)
                {
                    if(item1.Name != card.Name || item1.Suit != card.Suit || item1.Dignity != card.Dignity)
                    {
                        list.Add(item1);
                    }
                }
                Hands[Hands.IndexOf(item)] = new HandList(id, list);
            }
        }
    }
    [Server]
    public void RemoveCardFromInventory(CardAttributes card, uint id)
    {
        foreach (var item in Inventorys)
        {
            if(item.Id == id)
            {
                List<CardAttributes> list = new List<CardAttributes>();
                foreach(var item1 in item.Cards)
                {
                    if(item1.Name != card.Name || item1.Suit != card.Suit || item1.Dignity != card.Dignity)
                    {
                        list.Add(item1);
                    }
                }
                Debug.LogWarning(list.Count);
                Inventorys[Inventorys.IndexOf(item)] = new CardList(id, list);
            }
        }
        FindObjectOfType<PlayerNetworkController>().RemoveCardFromInventoryClientRpc(id, card);
    }
    [Server]
    public void GiveCardToDiscard(CardAttributes card)
    {
        Discard.Add(card);
        FindObjectOfType<PlayerNetworkController>().UpdateDiscardClientRpc(card);
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeMove()
    {
        if (Coroutine != null) StopCoroutine(Coroutine);
        print("Coroutine has been stoped");
        ChangeMove();
    }
    [Server]
    public void RegenerationHealth(uint id, CardAttributes card)
    {
        foreach (var item in Healths)
        {
            if (item.Health+1 < 5)
            {
                if (card.Name == "Saloon")
                {
                    foreach (var item1 in FindObjectsOfType<PlayerNetworkController>())
                    {
                        if (item.Id == item1.netId)
                        {
                            Healths[Healths.IndexOf(item)] = new HealthList(item1.netId, item.Health+1);
                            item1.HealthUpdateClientRpc(item1.netId, item.Health + 1);
                        }
                    }
                }
                else if (item.Id == id)
                {
                    Healths[Healths.IndexOf(item)] = new HealthList(id, item.Health+1);
                    foreach (var item1 in FindObjectsOfType<PlayerNetworkController>())
                    {
                        item1.HealthUpdateClientRpc(id, item.Health + 1);
                    }
                }
            }

        }
    }
}
