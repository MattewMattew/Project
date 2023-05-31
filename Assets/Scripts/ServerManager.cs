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


    [SyncVar]
    public uint turnPlayerId;

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
    void SyncPackList(SyncList<CardAttributes>.Operation op, int index, CardAttributes oldItem, CardAttributes newItem)
    {
        // print($"{oldItem}, {newItem}");
        switch (op)
        {
            case SyncList<CardAttributes>.Operation.OP_ADD:
                {
                    CardVars.Add(newItem);
                    break;
                }
            case SyncList<CardAttributes>.Operation.OP_CLEAR:
                {

                    break;
                }
            case SyncList<CardAttributes>.Operation.OP_INSERT:
                {

                    break;
                }
            case SyncList<CardAttributes>.Operation.OP_REMOVEAT:
                {

                    break;
                }
            case SyncList<CardAttributes>.Operation.OP_SET:
                {

                    break;
                }
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
                GiveHandCards(PackCards, player);
                if (player.GetComponent<NetworkIdentity>().netId == 1)
                {
                    GiveTurn(player.GetComponent<NetworkIdentity>().netId);
                    StartCoroutine(MoveFunc());
                }
            }
        }
    }
    [Client]
    void Update()
    {
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
        if (isServer) CmdCardAdded();
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
        spawnPoint = new List<Vector2>() { new Vector2(0, -237), new Vector2(-696, 0), new Vector2(0, 421), new Vector2(696, 0) };
        foreach (var player in players)
        {
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
    IEnumerator MoveFunc()
    {
        MoveTime = 5;
        while (MoveTime-- > 0)
        {
            PlayerNetworkController[] players = FindObjectsOfType<PlayerNetworkController>();
            foreach (var player in players)
            {
                player.TimerUpdateClientRpc(MoveTime, turnPlayerId);
            }
            yield return new WaitForSeconds(1);
        }
        ChangeMove();
    }
    [Command(requiresAuthority = false)]
    public void ChangeMove()
    {
        StopAllCoroutines();

        if (turnPlayerId + 1 > FindObjectOfType<NetworkManagerCard>().numPlayers)
            GiveTurn(1);
        else GiveTurn(turnPlayerId + 1);

        StartCoroutine(MoveFunc());

    }
    [Server]
    void GiveTurn(uint id)
    {
        turnPlayerId = id;
    }
    [Server]
    void GiveHandCards(SyncList<CardAttributes> pack, GameObject player) // ���������� ���� � ����
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(pack, player);
    }
    [Server]
    void GiveCardToHand(SyncList<CardAttributes> pack, GameObject player) // ������ ����� � ����
    {
        if (pack.Count == 0)
            return;
        bool check = false;
        List<CardAttributes> list = new List<CardAttributes> { pack[0] };
        foreach (var item in Hands)
        {
            if (player.GetComponent<NetworkIdentity>().netId == item.Id)
            {
                check = true; break;
            }
        }
        if (check)
        {
            foreach (var hand in Hands)
            {
                if (hand.Id == player.GetComponent<NetworkIdentity>().netId)
                {
                    hand.Cards.Add(pack[0]);
                }
            }
        }
        else
        {
            Hands.Add(new CardList(player.GetComponent<NetworkIdentity>().netId, list));
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
//     [ClientRpc]
//     void CheckClientRpc(List<CardAttributes> cardAttributes, CardAttributes card, uint id)
//     {
// /*        print($"{card.Name} {card.Suit} {card.Dignity} || {id}");
//         foreach (var item in Hands)
//         {
//             print($"{item.Cards.Count} cound cards of {item.Id} player");
//             foreach (var item1 in item.Cards)
//             {
//                 print($"{item1.Name} {item1.Suit} {item1.Dignity} $$$ {id}");
//             }
//         }*/
//         foreach (var item in cardAttributes)
//         {
//             print($"{item.Name}");
//         }
//     }

    [ClientRpc]
    void Check2ClientRpc(List<CardAttributes> hand)
    {
        // foreach (var item in Hands)
        // {
        //     print($"{item.Cards.Count} cound cards of {item.Id} player");
        //     foreach (var item1 in item.Cards)
        //     {
        //         print($"{item1.Name} {item1.Suit} {item1.Dignity} ^^^ ");
        //     }
        // }
        print(hand.Count);
    }
    [Server]
    public void GiveCardToDiscard(CardAttributes card)
    {
        // Check2ClientRpc();
        // foreach (var hand in Hands)
        // {
        //     if (hand.Id == id)
        //     {
        //         print($"{card.Name} {card.Suit} {card.Dignity} % {id}");
        //         CheckClientRpc(hand.Cards, card, id);
        //         hand.Cards.Remove(card);
                Discard.Add(card);
        //         Check2ClientRpc();
                FindObjectOfType<PlayerNetworkController>().UpdateDiscardClientRpc(card);
        //     }
        // }
    }
    // [Server]
    // public void RemoveCard(CardAttributes card, uint id)
    // {
    //     foreach (var hand in Hands)
    //     {
    //         if (hand.Id == id)
    //         {
    //             hand.Cards.Remove(card);
    //             print($"hand {hand.Id} {hand.Cards.Count}");
    //             FindObjectOfType<PlayerNetworkController>().UpdateCountCardPlayerClientRpc(hand.Cards, id);

    //         }
            
    //     }
    // }

    // public IEnumerator DeleteCard(CardAttributes cardd, uint id)
    // {
    //     yield return new WaitForSeconds(4);
    //     GiveCardToDiscard(cardd, id);
    // }
/*    public void DeleteCard(CardAttributes cardd, uint id)
    {
        foreach (var hand in Hands)
        {
            if (hand.Id == id)
            {
                print($"{cardd.Name} {cardd.Suit} {cardd.Dignity} % {id}");
                CheckClientRpc(hand.Cards, cardd, id);
                hand.Cards.Remove(cardd);
                Check2ClientRpc();

            }
        }
    }*/
    /*    public override void OnStartClient()
        {

            base.OnStartClient();

            Cards.Callback += SyncTransformVars; //������ hook, ��� SyncList ���������� �������� �� Callback
            CardVars = new List<Card>(Cards.Count); //��� ��� Callback ��������� ������ �� ��������� �������,  
            for (int i = 0; i < Cards.Count; i++) //� � ��� �� ������ ����������� ��� ����� ���� �����-�� ������ � �������, ��� ����� ��� ������ ������ � ��������� ������
            {
                print(Cards[i].Name);
                CardVars.Add(Cards[i]);
            }
            Debug.Log(CardVars.Count + " " + "�� ������");
            GiveHandCards(CardVars, SelfHand);
            print(CardVars.Count + " " + "����� ������");
        }*/
}
