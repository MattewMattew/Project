using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;
using System;

public class ServerManager : NetworkBehaviour
{
    public readonly SyncList<CardAttributes> PackCards = new SyncList<CardAttributes>();
    public GameCard CurrentGame;
    public List<CardAttributes> CardVars;
    public readonly SyncList<CardAttributes> Discard = new SyncList<CardAttributes>();
    public readonly SyncList<CardList> Inventorys = new SyncList<CardList>();
    public readonly SyncList<CardList> Hands = new SyncList<CardList>();
    public readonly SyncList<HealthList> Healths = new SyncList<HealthList>();

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
        foreach (var item in Healths)
        {
            print($"{item.Id} have {item.Health} health   {Healths.Count}");
        }
        /*        print(PackCards.Count + " " + "ServerManager");
                print($"{turnPlayerId}");
                print($"{Discard.Count} Discard");
                foreach (var item in Hands)
                {
                    print($"{item.Id} hand id");
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
    }
    void Start()
    {
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        if (isServer) 
        {
            CmdCardAdded();
        }
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        spawnPoint = new List<Vector2>() { new Vector2(0, -237), new Vector2(-696, 0), new Vector2(0, 421), new Vector2(696, 0) };
        foreach (var player in players)
        {
            if(isServer) Healths.Add(new HealthList(player.netId, 4));
            if (player.isLocalPlayer)
            {
                player.setPlayerPosition(spawnPoint[0]);
            }
            else
            {
                player.setPlayerPosition(spawnPoint[1]);
                spawnPoint.RemoveAt(1);
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
        if(attackedPlayerId != 0)
        {
            foreach (var item in Healths)
            {
                print($"now {item.Id} and attacked {attackedPlayerId}");
                if (item.Id == attackedPlayerId)
                {
                    Healths[Healths.IndexOf(item)]= new HealthList(attackedPlayerId, item.Health-1);
                    foreach (var item1 in players)
                    {
                        item1.HealthUpdateClientRpc(attackedPlayerId, item.Health - 1);
                    }
                }
            }
            GiveTurn(turnPlayerId, false);
        }else ChangeMove();
    }

    [Server]
    public void ChangeMove()
    {
        StopAllCoroutines();
        foreach (var player in FindObjectsOfType<PlayerNetworkController>())
        {
            if(player.netId == turnPlayerId)
            {
                player.EndTurnClientRpc();
                break;
            }
        }
        if (turnPlayerId + 1 > FindObjectOfType<NetworkManagerCard>().numPlayers)
            GiveTurn(1, false);
        else GiveTurn(turnPlayerId + 1, false);
        

        GiveHandCards(PackCards, turnPlayerId, 2);
    }
    [Server]
    public void GiveTurn(uint id, bool target)
    {
        StopAllCoroutines();
        attackedPlayerId = 0;
        turnModificator = "No";
        if (!target)
        {
            turnPlayerId = id;
        }
        else
        {
            attackedPlayerId = id;
            turnModificator = "Attack";
        }

        StartCoroutine(MoveFunc());
    }
    [Server]
    void GiveHandCards(SyncList<CardAttributes> pack, uint id, int cardsCount) // ���������� ���� � ����
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
                    hand.Cards.Add(pack[0]);
                }
            }
        }
        else
        {
            Hands.Add(new CardList(id, list));
        }
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
                    inventory.Cards.Add(list[0]);
                }
            }
        }
        else
        {
            FindObjectOfType<ServerManager>().Inventorys.Add(new ServerManager.CardList(playerController.netId, list));
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
    public void AttackAction(PlayerNetworkController playerController)
    {
        print($"{playerController.netId} has been attacked");
        GiveTurn(playerController.netId, true);
    }
    [ClientRpc]
    void Check2ClientRpc(List<CardAttributes> hand)
    {
        print(hand.Count);
    }
    [Server]
    public void GiveCardToDiscard(CardAttributes card/*, uint id, uint target*/)
    {
        Discard.Add(card);
        FindObjectOfType<PlayerNetworkController>().UpdateDiscardClientRpc(card);
    }
}
