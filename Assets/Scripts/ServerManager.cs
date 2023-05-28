using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    public readonly SyncList<CardAttributes> PackCards = new SyncList<CardAttributes>();
    public GameCard CurrentGame;
    public List<CardAttributes> CardVars;
    public List<CardAttributes> InvCards1 = new List<CardAttributes>();
    public List<CardAttributes> InvCards2 = new List<CardAttributes>();
    public List<CardAttributes> InvCards3 = new List<CardAttributes>();
    public readonly SyncList<CardAttributes> Hand1 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Hand2 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Hand3 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Inventory1 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Inventory2 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Inventory3 = new SyncList<CardAttributes>();
    private List<Vector2> spawnPoint = new List<Vector2>() { new Vector2(0, -237), new Vector2(-696, -77), new Vector2(0, 421) };
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
                int card = Random.Range(0, CardDesk.AllServerCards.Count);
                list.Add(CardDesk.AllServerCards[card]);
                CardDesk.AllServerCards.RemoveAt(card);
            }
            return list;
        }

    }
    void SyncPackList(SyncList<CardAttributes>.Operation op, int index, CardAttributes oldItem, CardAttributes newItem)
    {
        print($"{oldItem}, {newItem}");
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
        print("Pack check");
        if (PackCards.Count == 0)
        {
            print("Pack is spawned");
            CurrentGame = new GameCard();
            foreach (var item in CurrentGame.Pack)
            {
                CardAdded(item);
            }
        }
        else
        {
            print("Pack be ready");
        }
    }
    void Update()
    {
        print(PackCards.Count+" "+"ServerManager");
        print($"{InvCards1.Count} InvCards1");
        print($"{Inventory1.Count} Inventory1");

    }
    void Start()
    {
        if (isServer) CmdCardAdded();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Field");
        foreach (var player in players)
        {
            if (player.GetComponent<PlayerNetworkController>().isLocalPlayer)
            {
                player.GetComponent<PlayerNetworkController>().setPlayerPosition(spawnPoint[0]);
            }
            else
            {
                player.GetComponent<PlayerNetworkController>().setPlayerPosition(spawnPoint[1]);
                spawnPoint.RemoveAt(1);
            }
            if (isServer)
            {
                GiveHandCards(PackCards, player);
            }
        }
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        foreach (var item in Inventory1)
        {
            InvCards1.Add(item);
        }
        foreach (var item in Inventory2)
        {
            InvCards2.Add(item);
        }
        foreach (var item in Inventory3)
        {
            InvCards3.Add(item);
        }
        
        


    }
    void GiveHandCards(SyncList<CardAttributes> pack, GameObject hand) // ���������� ���� � ����
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(pack, hand);
    }

    void GiveCardToHand(SyncList<CardAttributes> pack, GameObject hand) // ������ ����� � ����
    {
        if (pack.Count == 0)
            return;

        CardAttributes card = pack[0];
        if(hand.GetComponent<NetworkIdentity>().netId == 1)
        {
            Hand1.Add(pack[0]);
        }
        else if(hand.GetComponent<NetworkIdentity>().netId == 2)
        {
            Hand2.Add(pack[0]);
        }
        else if(hand.GetComponent<NetworkIdentity>().netId == 3)
        {
            Hand3.Add(pack[0]);
        }
        pack.RemoveAt(0);

    }
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
