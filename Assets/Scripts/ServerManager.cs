using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    public readonly SyncList<CardAttributes> PackCards = new SyncList<CardAttributes>();
    public GameCard CurrentGame;
    public List<CardAttributes> CardVars;
    public readonly SyncList<CardAttributes> Hand1 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Hand2 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Hand3 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Inventory1 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Inventory2 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Inventory3 = new SyncList<CardAttributes>();

    [SyncVar]
    public uint turnPlayerId;

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
        if (PackCards.Count == 0)
        {
            CurrentGame = new GameCard();
            foreach (var item in CurrentGame.Pack)
            {
                CardAdded(item);
            }
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Field");
        foreach (var player in players)
        {
            if (isServer)
            {
                GiveHandCards(PackCards, player);
                turnPlayerId = player.GetComponent<NetworkIdentity>().netId;
            }
        }
    }
    void Update()
    {
        print(PackCards.Count+" "+"ServerManager");

    }
    void Start()
    {
        if (isServer) CmdCardAdded();
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Field");
        spawnPoint = new List<Vector2>() { new Vector2(0, -237), new Vector2(-696, -77), new Vector2(0, 421) };
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
        }

    }
/*    void Turn(int oldValue, int newValue)
    {
        turnPlayerId = newValue;
    }*/
    void GiveHandCards(SyncList<CardAttributes> pack, GameObject hand) // Количество карт в руке
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(pack, hand);
    }

    void GiveCardToHand(SyncList<CardAttributes> pack, GameObject hand) // Выдача карты в руку
    {
        if (pack.Count == 0)
            return;
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

            Cards.Callback += SyncTransformVars; //вместо hook, для SyncList используем подписку на Callback
            CardVars = new List<Card>(Cards.Count); //так как Callback действует только на изменение массива,  
            for (int i = 0; i < Cards.Count; i++) //а у нас на момент подключения уже могут быть какие-то данные в массиве, нам нужно эти данные внести в локальный массив
            {
                print(Cards[i].Name);
                CardVars.Add(Cards[i]);
            }
            Debug.Log(CardVars.Count + " " + "До выдачи");
            GiveHandCards(CardVars, SelfHand);
            print(CardVars.Count + " " + "После выдачи");
        }*/
}
