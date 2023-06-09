using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;
using System;
// using static UnityEditor.Progress;
using System.IO;
using TMPro;

public class ServerManager : NetworkBehaviour
{
    public readonly SyncList<CardAttributes> PackCards = new SyncList<CardAttributes>();
    public GameCard CurrentGame;
    public List<CardAttributes> CardVars;
    public readonly SyncList<CardAttributes> Discard = new SyncList<CardAttributes>();
    public readonly SyncList<CardList> Inventorys = new SyncList<CardList>();
    public readonly SyncList<HandList> Hands = new SyncList<HandList>();
    public readonly SyncList<HealthList> Healths = new SyncList<HealthList>();

    private TextMeshProUGUI stage;

    private int playersCount = 0;

    public enum Roles {SINDICATE, HELPER, RENEGADE, CAPTAIN}

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
    public bool useBang = false;

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
            int allServerCards = CardDesk.AllServerCards.Count;
            for (int i = 0; i < allServerCards; i++)
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
        //    foreach (var item in Hands)
        //     {
        //         print($"{item.Cards.Count} in {item.Id} hand");
        //         foreach (var item1 in item.Cards)
        //         {
        //             print($"{item1.Name} card in hand {item.Id}");
        //         }
        //     }
        /*        foreach (var item in Inventorys)
                {
                    print($"{item.Cards.Count} cards have {item.Id} player in inventory");
                    foreach (var item1 in item.Cards)
                    {
                        print($"{item.Id} player inventory have {item1.Name} card. In array {item.Cards.Count} cards");
                    }
                }*/
        /*      foreach (var item in FindObjectsOfType<PlayerNetworkController>())
              {
                   Debug.LogWarning($"range {item.Range} to {item.netId} player");
              }*/
        /*        if (isClient)
                foreach (var item in FindObjectsOfType<PlayerNetworkController>())
                {
                    print($"{item.netId} player have {item.Role} role");
                }*/
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        // print($"{players.Length} players");
        switch (turnModificator)
        {
            case "No":
                {
                    stage.text = "Фаза хода";
                    break;
                }
            case "Bang":
                {
                    stage.text = "Фаза атаки";
                    break;
                }
            case "Discarding":
                {
                    stage.text = "Фаза сброса";
                    break;
                }
            case "Indians":
                {
                    stage.text = "Фаза атаки заключенных";
                    break;
                }
            case "Duel":
                {
                    stage.text = "Фаза дуэли";
                    break;
                }
            case "Gatling":
                {
                    stage.text = "Фаза массивной атаки";
                    break;
                }
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
        stage = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>();
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        playersCount = players.Length;
        if (isServer) 
        {
            CmdCardAdded();
        }
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        List<Roles> RolesList = new List<Roles>();
        if (players.Length == 6)
        {
            RolesList = new List<Roles>() { Roles.SINDICATE, Roles.SINDICATE, Roles.SINDICATE, Roles.CAPTAIN, Roles.HELPER, Roles.RENEGADE };
            rangePlayers = new List<RangePlayers> { 
                new RangePlayers(0, new Vector2(0, -230)),
                new RangePlayers(1, new Vector2(-512, -40)),
                new RangePlayers(2, new Vector2(-512, 176)),
                new RangePlayers(3, new Vector2(0, 392)),
                new RangePlayers(2, new Vector2(512, 176)),
                new RangePlayers(1, new Vector2(512, -40)),
                /*new RangePlayers(1, new Vector2(593, -92))*/
                
                
                /*new RangePlayers(0, new Vector2(0, -237)), 
                new RangePlayers(1, new Vector2(-696, 0)),
                new RangePlayers(2, new Vector2(0, 421)),
                new RangePlayers(1, new Vector2(696, 0))*/
            };
        }
        else if(players.Length == 2)
        {
            RolesList = new List<Roles>() { Roles.CAPTAIN, Roles.SINDICATE };
            rangePlayers = new List<RangePlayers> {
                new RangePlayers(0, new Vector2(0, -237)), 
                new RangePlayers(2, new Vector2(-696, 0)),

            };
        }
        else if (players.Length == 4)
        {
            RolesList = new List<Roles>() { Roles.SINDICATE, Roles.CAPTAIN, Roles.SINDICATE, Roles.RENEGADE };
            rangePlayers = new List<RangePlayers> { 
                new RangePlayers(0, new Vector2(0, -230)),
                new RangePlayers(1, new Vector2(-512, 0)),
                new RangePlayers(2, new Vector2(0, 360)),
                new RangePlayers(1, new Vector2(512, 0)),
              
            };
        }
        else if (players.Length == 5)
        {
            RolesList = new List<Roles>() { Roles.SINDICATE, Roles.CAPTAIN, Roles.SINDICATE, Roles.HELPER, Roles.RENEGADE };
            rangePlayers = new List<RangePlayers> { 
                new RangePlayers(0, new Vector2(0, -230)),
                new RangePlayers(1, new Vector2(-512, -40)),
                new RangePlayers(2, new Vector2(-512, 176)),
                new RangePlayers(2, new Vector2(512, 176)),
                new RangePlayers(1, new Vector2(512, -40)),
              
            };

        }
        else if (players.Length == 7)
        {
            RolesList = new List<Roles>() { Roles.SINDICATE, Roles.SINDICATE, Roles.CAPTAIN, Roles.SINDICATE, Roles.HELPER, Roles.HELPER, Roles.RENEGADE };
            rangePlayers = new List<RangePlayers>{
                new RangePlayers(0, new Vector2(0, -230)),
                new RangePlayers(1, new Vector2(-512, -40)),
                new RangePlayers(2, new Vector2(-512, 176)),
                new RangePlayers(3, new Vector2(-300, 392)),
                new RangePlayers(3, new Vector2(300, 392)),
                new RangePlayers(2, new Vector2(512, 176)),
                new RangePlayers(1, new Vector2(512, -40)),
            };
        }
        if (isClient)
        {
            var localPlayerId = (uint)0;
            bool checkLocalPlayer = false;

            foreach (var item in players)
            {
                if (item.isLocalPlayer)
                {
                    localPlayerId = item.netId; break;
                }
            }
            
            for (int i = (int)localPlayerId; rangePlayers.Count > 0 ; i++)
            {
                if (i > players.Length) i = 1;
                foreach (var player in players)
                {
                    if (player.netId == localPlayerId && !checkLocalPlayer)
                    {
                        checkLocalPlayer = true;
                        player.setPlayerPosition(rangePlayers[0].Range, rangePlayers[0].Pos);
                        rangePlayers.RemoveAt(0);
                        break;
                    }
                    else if (player.netId == i)
                    {
                        player.setPlayerPosition(rangePlayers[0].Range, rangePlayers[0].Pos);
                        rangePlayers.RemoveAt(0);
                        break;
                    }
                }
            }
        }
        if (isServer)
        {
            foreach (var player in players)
            {

                var index = UnityEngine.Random.Range(0, RolesList.Count);
                player.GiveRole(player.netId, RolesList[index]);
                if (RolesList[index] == Roles.CAPTAIN)
                {
                    Healths.Add(new HealthList(player.netId, 5));
                    GiveTurn(player.netId, false);
                    GiveHandCards(PackCards, player.netId, 5);
                }
                else
                {
                    Healths.Add(new HealthList(player.netId, 4));
                    GiveHandCards(PackCards, player.netId, 4);
                }
                RolesList.RemoveAt(index);
            }
        }
        

    }
    [Server]
    IEnumerator MoveFunc(int time)
    {
        MoveTime = time;
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        while (MoveTime-- > 0)
        {
            foreach (var player in players)
            {
                if(attackedPlayerId != 0)
                    player.TimerUpdateClientRpc(MoveTime, attackedPlayerId);
                else
                    player.TimerUpdateClientRpc(MoveTime, turnPlayerId);
                if(turnModificator == "Discarding")
                {
                    var discardCard = -1;
                    if (player.netId == turnPlayerId && attackedPlayerId == 0)
                    {
                        foreach (var hand in Hands)
                        {
                            if (hand.Id == player.netId)
                            {
                                foreach (var health in Healths)
                                {
                                    if (health.Id == player.netId)
                                    {
                                        discardCard = hand.Cards.Count - Healths[Healths.IndexOf(health)].Health;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        // print($"{discardCard} discardCard");
                        if (discardCard <= 0)
                        {
                            // print("Done");
                            MoveTime = 0;
                        }
                    }
                }

            }
            yield return new WaitForSeconds(1);
        }
        if(turnModificator == "Discarding")
        {
            foreach (var player in players)
                foreach (var hand in Hands)
                {
                    if(player.netId == hand.Id)
                    {
                        List<CardAttributes> list = new List<CardAttributes>();
                        foreach (var card in hand.Cards)
                            list.Add(card);
                        foreach (var health in Healths)
                            if(player.netId == turnPlayerId)
                                if(health.Id == player.netId)
                                {
                                    for (int i = 0; i < hand.Cards.Count - Healths[Healths.IndexOf(health)].Health; i++)
                                    {
                                        int index = UnityEngine.Random.Range(0, list.Count);
                                        GiveCardToDiscard(list[index]);
                                        player.RemoveCardFromHandClientRpc(list[index]);
                                        list.RemoveAt(index);
                                    }
                                    break;
                                }
                        Hands[Hands.IndexOf(hand)] = new HandList(player.netId, list);
                        FindObjectOfType<PlayerNetworkController>().UpdateCountCardsClientRpc(list.Count, player.netId);
                        break;
                    }
                }
        }
        ChangeMove();
    }

    [Server]
    public void ChangeMove()
    {
        turnModificator = "No";
        duelTargetPlayerId = 0;
        if(Coroutine != null) StopCoroutine(Coroutine);
        foreach (var player in FindObjectsOfType<PlayerNetworkController>())
        {
            int discardCard = 0;
            if(player.netId == turnPlayerId && attackedPlayerId == 0)
            {
                foreach (var hand in Hands)
                {
                    if(hand.Id == player.netId)
                    {
                        foreach (var health in Healths)
                        {
                            if(health.Id == player.netId)
                            {
                                discardCard = hand.Cards.Count - Healths[Healths.IndexOf(health)].Health;
                                break;
                            }
                        }
                    break;
                    }
                }
                if(discardCard > 0) 
                {
                    turnModificator = "Discarding";
                    Coroutine = StartCoroutine(MoveFunc(10));
                    player.EndTurnClientRpc();
                    return;
                }
            }
        }
        // Debug.LogWarning(playersCount + " numPlayers");
        if (attackedPlayerId != 0)
        {
            foreach (var item in Healths)
            {
                if (item.Id == attackedPlayerId)
                {
                    Healths[Healths.IndexOf(item)] = new HealthList(attackedPlayerId, item.Health - 1);
                    foreach (var item1 in FindObjectsOfType<PlayerNetworkController>())
                    {
                        item1.HealthUpdateClientRpc(attackedPlayerId, item.Health - 1);
                    }
                    if (item.Health - 1 <= 0)
                    {
                        DeathAction(attackedPlayerId);
                    }
                }
            }
            GiveTurn(turnPlayerId, false);
        }
        else if (turnPlayerId + 1 > playersCount)
        {
            for (int i = 1; i <= playersCount; i++)
                foreach (var item in FindObjectsOfType<PlayerNetworkController>())
                {
                    if (item.netId == i)
                    {
                        // print($"{i}");
                        GiveTurn((uint)i, false);
                        useBang = false;
                        i = playersCount;
                        break;
                    }
                }
        }
        else 
        {
            for (int i = (int)turnPlayerId + 1; i <= playersCount; i++)
                foreach (var item in FindObjectsOfType<PlayerNetworkController>())
                {
                    if (item.netId == i)
                    {
                        // print($"{i}");
                        GiveTurn((uint)i, false);
                        useBang = false;
                        i = playersCount;
                        break;
                    }
                }        
        } 

    }
    [Server]
    public void DeathAction(uint id)
    {
        foreach (var item in FindObjectsOfType<PlayerNetworkController>())
        {
            item.DeathActionClientRpc(id);
        }
    }
    [Server]
    public void GiveTurn(uint id, bool target)
    {
        if (Coroutine != null) StopCoroutine(Coroutine);
        if (!target)
        {
            turnPlayerId = id;
            if(attackedPlayerId == 0) GiveHandCards(PackCards, turnPlayerId, 2);
            Coroutine = StartCoroutine(MoveFunc(15));
        }
        attackedPlayerId = 0;
        turnModificator = "No";
        if(target)
        {
            attackedPlayerId = id;
            Coroutine = StartCoroutine(MoveFunc(15));
            // turnModificator = "Attack";
        }
        FindObjectOfType<PlayerNetworkController>().ButtonActivationClientRpc(id);
        
    }
    [Server]
    public void GiveHandCards(SyncList<CardAttributes> pack, uint id, int cardsCount) // ���������� ���� � ����
    {
        int i = 0;
        while (i++ < cardsCount)
        {
            if (pack.Count == 0)
            {
                ResetPack();
                ResetDiscardClientRpc();
            }
            GiveCardToHand(pack, id);
        } 
    }

    [ClientRpc]
    void ResetDiscardClientRpc()
    {
        FindObjectOfType<GameManagerScript>().CardDiscard.GetComponentsInChildren<CardScript>();
    }

    [Server]
    public void ResetPack()
    {
        // Debug.LogWarning(Discard.Count);
        int count = Discard.Count;
        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, Discard.Count);
            PackCards.Add(Discard[index]);
            Discard.RemoveAt(index);
        }
    }
    [Server]
    void GiveCardToHand(SyncList<CardAttributes> pack, uint id) // ������ ����� � ����
    {
        
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
                    FindObjectOfType<PlayerNetworkController>().UpdateCountCardsClientRpc(dumpList.Count, id);
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
    public void PanicAction(CardAttributes card, uint id)
    {
        print($"card {card.Name} {card.Dignity} {card.Suit} id {id}");
        foreach (var hand in Hands)
        {
            if (hand.Id == id)
            {
                List<CardAttributes> dumpList = new List<CardAttributes>();
                foreach (var handCard in hand.Cards)
                {
                    dumpList.Add(handCard);
                }
                dumpList.Add(card);
                Hands[Hands.IndexOf(hand)] = new HandList(id, dumpList);
                FindObjectOfType<PlayerNetworkController>().UpdateCountCardsClientRpc(dumpList.Count, id);
            }
        }
        FindObjectOfType<PlayerNetworkController>().GiveHandCardsClientRpc(id, card);
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
        GiveTurn(id, true);
        turnModificator = card;
        useBang = true;
    }

    [Server]
    public void DuelAction (uint idAttacking, uint idDefenser)
    {
        if(duelTargetPlayerId == 0) duelTargetPlayerId = idDefenser;
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
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    [Server]
    public void RandomRemoveCardFromHand(PlayerNetworkController player, CardAttributes card)
    {
        foreach (var hand in Hands)
        {
            if(hand.Id == player.netId)
            {
                int index = UnityEngine.Random.Range(0, hand.Cards.Count);
                if (card.Name == "Panic")
                    PanicAction(Hands[Hands.IndexOf(hand)].Cards[index], turnPlayerId);
                if (card.Name == "Women")
                    GiveCardToDiscard(Hands[Hands.IndexOf(hand)].Cards[index]);
                player.RemoveCardFromHandClientRpc(Hands[Hands.IndexOf(hand)].Cards[index]);
                RemoveCardFromHand(Hands[Hands.IndexOf(hand)].Cards[index], player.netId);
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
                FindObjectOfType<PlayerNetworkController>().UpdateCountCardsClientRpc(list.Count, id);
                foreach(var player in FindObjectsOfType<PlayerNetworkController>())
                {
                    if (player.netId==id)
                    {
                        player.AnimAction(card);
                        break;

                    }
                }
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
                // Debug.LogWarning(list.Count);
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
    [Server]
    public void CardDiscarding() 
    {
        if (Coroutine != null) StopCoroutine(Coroutine);
        foreach (var player in FindObjectsOfType<PlayerNetworkController>())
            if(player.netId == turnPlayerId)
            {
                foreach (var hand in Hands)
                {
                    if (player.netId == hand.Id)
                    {
                        List<CardAttributes> list = new List<CardAttributes>();
                        foreach (var card in hand.Cards)
                            list.Add(card);
                        foreach (var health in Healths)
                            if (health.Id == player.netId)
                            {
                                for (int i = 0; i < hand.Cards.Count - Healths[Healths.IndexOf(health)].Health; i++)
                                {
                                    int index = UnityEngine.Random.Range(0, list.Count);
                                    GiveCardToDiscard(list[index]);
                                    player.RemoveCardFromHandClientRpc(list[index]);
                                    list.RemoveAt(index);
                                }
                                Hands[Hands.IndexOf(hand)] = new HandList(player.netId, list);
                                FindObjectOfType<PlayerNetworkController>().UpdateCountCardsClientRpc(list.Count, player.netId);
                                ChangeMove();
                                break;
                            }
                    }
                }
                break;
            }
    }
    [Command(requiresAuthority = false)]
    public void CmdChangeMove()
    {
        if (Coroutine != null) StopCoroutine(Coroutine);
        if(turnModificator == "Discarding")
        {
            CardDiscarding();
        }
        else
        {
            ChangeMove();
        }
    }
    [Server]
    public void RegenerationHealth(uint id, CardAttributes card)
    {
        int maxHP = 0;
        foreach (var player in FindObjectsOfType<PlayerNetworkController>())
            if (player.netId == id)
                maxHP = player.maxHealth;

        foreach (var item in Healths)
        {
            if (item.Health < maxHP)
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
