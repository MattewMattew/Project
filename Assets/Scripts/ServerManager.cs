using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;
using System;
// using static UnityEditor.Progress;
using UnityEngine.SceneManagement;
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
    private int SindicateCount, HelperCount, RenegadeCount, CaptainCount;
    public GameObject GB, GS, GR, winMenu;
    public TextMeshProUGUI textB, textS, textR,winText;

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

    public struct AttributesGameCharacters
    {
        public string Name;
        public AttributesGameCharacters(string name)
        {
            Name = name;
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

    int MoveTime = 30;
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
      public void exitInScene(int index){

        SceneManager.LoadScene(index);
    }


    [Client]
    void Update()
    {
       /* CountRoles();*/
/*                foreach (var item in Healths)
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
    [Server]
    public void SendNameToClient(uint id, string name)
    {
        foreach (var item in FindObjectsOfType<PlayerNetworkController>())
        {
            if(item.netId == id)
            {
                item.SetNameClientRpc(id, name);
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
        init();
        stage = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>();
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        playersCount = players.Length; 
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        if (players.Length == 6)
        {
            rangePlayers = new List<RangePlayers> { 
                new RangePlayers(0, new Vector2(0, -230)),
                new RangePlayers(1, new Vector2(-512, -40)),
                new RangePlayers(2, new Vector2(-512, 176)),
                new RangePlayers(3, new Vector2(0, 392)),
                new RangePlayers(2, new Vector2(512, 176)),
                new RangePlayers(1, new Vector2(512, -40)),
            };

        }
        else if(players.Length == 2)
        {
            rangePlayers = new List<RangePlayers> {
                new RangePlayers(0, new Vector2(0, -237)), 
                new RangePlayers(1, new Vector2(-696, 0)),
            };
        }
        else if (players.Length == 4)
        {
            rangePlayers = new List<RangePlayers> { 
                new RangePlayers(0, new Vector2(0, -230)),
                new RangePlayers(1, new Vector2(-512, 0)),
                new RangePlayers(2, new Vector2(0, 360)),
                new RangePlayers(1, new Vector2(512, 0)),
            };
        }
        else if (players.Length == 5)
        {
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
                        player.SetName();
                        rangePlayers.RemoveAt(0);
                        break;
                    }
                    else if (player.netId == i)
                    {
                        player.setPlayerPosition(rangePlayers[0].Range, rangePlayers[0].Pos);
                        player.SetName();
                        rangePlayers.RemoveAt(0);
                        break;
                    }
                    
                }
            }
        }
        GameObject.Find("MainCanvas").SetActive(false);

    }
    [Server]
    public void init()
    {
        CmdCardAdded();
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        playersCount = players.Length;
        List<Roles> RolesList = new List<Roles>();

        List<AttributesGameCharacters> ListGameCharacters = new List<AttributesGameCharacters>();
        
        ListGameCharacters.Add(new AttributesGameCharacters("Character1"));
        ListGameCharacters.Add(new AttributesGameCharacters("Character2"));
        ListGameCharacters.Add(new AttributesGameCharacters("Character3"));
        ListGameCharacters.Add(new AttributesGameCharacters("Character4"));
        ListGameCharacters.Add(new AttributesGameCharacters("Character5"));
        ListGameCharacters.Add(new AttributesGameCharacters("Character6"));
        ListGameCharacters.Add(new AttributesGameCharacters("Character7"));

        if (players.Length == 6)
        {
            RolesList = new List<Roles>() { Roles.SINDICATE, Roles.SINDICATE, Roles.SINDICATE, Roles.CAPTAIN, Roles.HELPER, Roles.RENEGADE };

        }
        else if (players.Length == 2)
        {
            RolesList = new List<Roles>() { Roles.CAPTAIN, Roles.SINDICATE };
        }
        else if (players.Length == 4)
        {
            RolesList = new List<Roles>() { Roles.SINDICATE, Roles.CAPTAIN, Roles.SINDICATE, Roles.RENEGADE };
        }
        else if (players.Length == 5)
        {
            RolesList = new List<Roles>() { Roles.SINDICATE, Roles.CAPTAIN, Roles.SINDICATE, Roles.HELPER, Roles.RENEGADE };

        }
        else if (players.Length == 7)
        {
            RolesList = new List<Roles>() { Roles.SINDICATE, Roles.SINDICATE, Roles.CAPTAIN, Roles.SINDICATE, Roles.HELPER, Roles.HELPER, Roles.RENEGADE };
        }
        foreach (var player in players)
        {
            var CharIndex = UnityEngine.Random.Range(0, ListGameCharacters.Count);
            player.GiveCharactersClientRpc(player.netId, ListGameCharacters[CharIndex]);
            ListGameCharacters.RemoveAt(CharIndex);
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



    public void CountRolesClientRpc()
    {
        SindicateCount = 0;
        RenegadeCount = 0;
        HelperCount = 0;
        CaptainCount = 0;
        foreach (var item in FindObjectsOfType<PlayerNetworkController>())
        {
            print($"{item.isDead} isDead");
            if (item.Role == Roles.SINDICATE && !item.isDead)
            {
                GB.SetActive(true);
                SindicateCount++;
            }

            if (item.Role == Roles.RENEGADE && !item.isDead)
            {
                GR.SetActive(true);
                RenegadeCount++;
            }
            if (item.Role == Roles.HELPER && !item.isDead)
            {
                GS.SetActive(true);
                HelperCount++;
            }
            if (item.Role == Roles.CAPTAIN && !item.isDead) CaptainCount++;
        }
        print($"SindicateCount {SindicateCount} RenegadeCount {RenegadeCount} HelperCount {HelperCount} CaptainCount {CaptainCount}");
        textS.text = HelperCount.ToString();
        textR.text = RenegadeCount.ToString();
        textB.text = SindicateCount.ToString();
        if (RenegadeCount == 0 && SindicateCount == 0) {
            winMenu.SetActive(true);
            winText.text = "ЧЛЕНЫ ЭКИПАЖА ПОБЕДИЛИ!";
        }
        if (CaptainCount == 0 && SindicateCount == 0) {
            winMenu.SetActive(true);
            winText.text = "РЕНЕГАТ ПОБЕДИЛ!";
        };
        if (CaptainCount == 0 && SindicateCount != 0) {
            winMenu.SetActive(true);
            winText.text = "СИНДИКАТ ПОБЕДИЛ!";
        };
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
                if(discardCard > 0 && turnModificator != "Jail") 
                {
                    turnModificator = "Discarding";
                    Coroutine = StartCoroutine(MoveFunc(10));
                    player.EndTurnClientRpc();
                    return;
                }
            }
        }
        turnModificator = "No";
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
                        print(item.Health - 1);
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
                    if (item.netId == i && !item.isDead)
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
                    if (item.netId == i && !item.isDead)
                    {
                        // print($"{i}");
                        GiveTurn((uint)i, false);
                        useBang = false;
                        i = playersCount;
                        break;
                    }
                }        
        }
        CheckNextCardInPackAttributes(turnPlayerId); 

    }
    [Server]
    public void DeathAction(uint id)
    {
        
        foreach (var item in Inventorys)
        {
            if(item.Id == id)
            {
                foreach (var item1 in item.Cards)
                {
                    GiveCardToDiscard(item1);
                }
                Inventorys[Inventorys.IndexOf(item)] = new CardList(id, new List<CardAttributes>());
            }
        }
        foreach (var item in Hands)
        {
            if (item.Id == id)
            {
                foreach (var item1 in item.Cards)
                {
                    GiveCardToDiscard(item1);
                }
                Hands[Hands.IndexOf(item)] = new HandList(id, new List<CardAttributes>());
            }
        }
        // int RenegadeCount = 0; 
        // int SindicateCount = 0;
        // int HelperCount = 0;
        // int CaptainCount = 0;
        foreach (var item in FindObjectsOfType<PlayerNetworkController>())
        {
            item.DeathActionClientRpc(id);
            // if (item.netId != id)
            // {
            //     if (item.Role == Roles.SINDICATE) SindicateCount++;
            //     if (item.Role == Roles.HELPER) HelperCount++;
            //     if (item.Role == Roles.RENEGADE) RenegadeCount++;
            //     if (item.Role == Roles.CAPTAIN) CaptainCount++;
            // }
        }
        
        // if (RenegadeCount == 0 && SindicateCount == 0) print("Captane win");
        // if (CaptainCount == 0 && SindicateCount == 0) print("Renegade win");
        // if (CaptainCount == 0 && SindicateCount != 0) print("Sindicate win");
    }
    [Server]
    public void GiveTurn(uint id, bool target)
    {
        // CheckNextCardInPackAttributes(id);
        if (Coroutine != null) StopCoroutine(Coroutine);
        if (!target)
        {
            turnPlayerId = id;
            if (attackedPlayerId == 0 && turnModificator != "Jail")
            {
                GiveHandCards(PackCards, turnPlayerId, 2);
            }
            Coroutine = StartCoroutine(MoveFunc(15));
        }
        attackedPlayerId = 0;
        if(target)
        {
            attackedPlayerId = id;
            Coroutine = StartCoroutine(MoveFunc(15));
            // turnModificator = "Attack";
        }
        turnModificator = "No";
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
            GiveCardToHand(pack[0], id);
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
    void GiveCardToHand(CardAttributes cardPack, uint id) // ������ ����� � ����
    {
        
        bool check = false;
        List<CardAttributes> list = new List<CardAttributes> { cardPack };
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
                    dumpList.Add(cardPack);
                    Hands[Hands.IndexOf(hand)] = new HandList(id, dumpList);
                    FindObjectOfType<PlayerNetworkController>().UpdateCountCardsClientRpc(dumpList.Count, id);
                }
            }
        }
        else
        {
            Hands.Add(new HandList(id, list));
        }
        FindObjectOfType<PlayerNetworkController>().GiveHandCardsClientRpc(id, cardPack);
        PackCards.Remove(cardPack);

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
        CheckNextCardInPackAttributes(id);
        useBang = true;
    }
    [Server]
    void CheckNextCardInPackAttributes(uint id)
    {
        foreach (var inv in GameObject.FindGameObjectsWithTag("Field"))
        {
            if(inv.GetComponentInParent<PlayerNetworkController>().netId == id)
            {
                foreach (var cardInv in inv.GetComponentsInChildren<CardInfoScripts>())
                {
                    if (turnModificator == "Bang" || turnModificator == "Gatling")
                    {
                        if (cardInv.SelfCard.Name == "Barrel")
                        {
                            if(PackCards.Count <= 0) ResetPack();
                            if (PackCards[0].Suit == "Hearts")
                            {
                                GiveTurn(turnPlayerId, false);
                                print($"player {turnPlayerId} || turn mod {turnModificator}");
                            }
                            GiveCardToDiscard(PackCards[0]);
                            PackCards.RemoveAt(0);
                            break;
                        }
                    }
                    if(turnModificator == "No")
                    {
                        if (cardInv.SelfCard.Name == "Dynamite")
                        {
                            if (PackCards.Count <= 0) ResetPack();
                            RemoveCardFromInventory(cardInv.GetComponent<CardInfoScripts>().SelfCard, id);
                            if (PackCards[0].Suit == "Spades" && (Int32.Parse(PackCards[0].Dignity) >= 2 && Int32.Parse(PackCards[0].Dignity) <= 9))
                            {
                                print($"BOOM {PackCards[0].Suit} {PackCards[0].Dignity}");
                                foreach (var item in Healths)
                                {
                                    if (item.Id == id)
                                    {
                                        foreach (var item1 in FindObjectsOfType<PlayerNetworkController>())
                                        {
                                            if (item.Id == item1.netId)
                                            {
                                                Healths[Healths.IndexOf(item)] = new HealthList(item1.netId, item.Health - 3);
                                                item1.HealthUpdateClientRpc(item1.netId, item.Health - 3);
                                                GiveCardToDiscard(cardInv.SelfCard);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in FindObjectsOfType<PlayerNetworkController>())
                                {
                                    if (item.netId == id)
                                    {
                                        for (int i = (int)turnPlayerId + 1; i <= playersCount + 1; i++)
                                        {
                                            if (i > playersCount) i = 1;
                                            foreach (var item1 in FindObjectsOfType<PlayerNetworkController>())
                                            {
                                                if (item1.netId == i)
                                                {
                                                    UpdateInventory(cardInv.GetComponent<CardInfoScripts>().SelfCard, item1, item1.transform);
                                                        i = playersCount+2;
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                
                            }
                            GiveCardToDiscard(PackCards[0]);
                            PackCards.RemoveAt(0);
                        }
                        if (cardInv.SelfCard.Name == "Jail")
                        {
                            if (PackCards.Count <= 0) ResetPack();
                            RemoveCardFromInventory(cardInv.GetComponent<CardInfoScripts>().SelfCard, id);
                            GiveCardToDiscard(cardInv.SelfCard);
                            if (PackCards[0].Suit != "Hearts")
                            {
                                turnModificator = cardInv.SelfCard.Name;
                                ChangeMove();
                            }
                            GiveCardToDiscard(PackCards[0]);
                            PackCards.RemoveAt(0);
                        }
                    }
                }
            }
        }
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
                CheckNextCardInPackAttributes(item.netId);
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
                foreach (var player in FindObjectsOfType<PlayerNetworkController>())
                {
                    if (player.netId == id)
                    {
                        player.CmdAnimAction(id ,card);
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
