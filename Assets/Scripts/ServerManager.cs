using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;

public class ServerManager : NetworkBehaviour
{
    public readonly SyncList<CardAttributes> PackCards = new SyncList<CardAttributes>();
    public GameCard CurrentGame;
    public List<CardAttributes> CardVars;
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
                if(player.GetComponent<NetworkIdentity>().netId == 1)
                {
                    GiveTurn(player.GetComponent<NetworkIdentity>().netId);
                    StartCoroutine(MoveFunc());
                }
            }
        }
    }
    void Update()
    {
        print(PackCards.Count+" "+"ServerManager");
        print($"{turnPlayerId}");
        foreach (var item in Hands)
        {
            print($"{item.Id} hand id");
            foreach (var item1 in item.Cards)
            {
                print($"{item1.Name} card in hand {item.Id}");
            }
        }
    }
    void Start()
    {
        if (isServer) CmdCardAdded();
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Field");
        spawnPoint = new List<Vector2>() { new Vector2(0, -237), new Vector2(-696, 0), new Vector2(0, 421), new Vector2(696, 0) };
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
    IEnumerator MoveFunc()
    {
        MoveTime = 5;
        while (MoveTime-- > 0)
        {
            yield return new WaitForSeconds(1); //Ожидание секунда  
        }
        ChangeMove();
    }
    [Command(requiresAuthority = false)]
    public void ChangeMove()
    {
        StopAllCoroutines();

        if (turnPlayerId+1 > FindObjectOfType<NetworkManagerCard>().numPlayers)
            GiveTurn(1);
        else GiveTurn(turnPlayerId + 1);

        StartCoroutine(MoveFunc());

    }
    [Server]
    void GiveTurn(uint id)
    {
        turnPlayerId = id;
    }
/*    void Turn(int oldValue, int newValue)
    {
        turnPlayerId = newValue;
    }*/
    void GiveHandCards(SyncList<CardAttributes> pack, GameObject player) // Количество карт в руке
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(pack, player);
    }

    void GiveCardToHand(SyncList<CardAttributes> pack, GameObject player) // Выдача карты в руку
    {
        if (pack.Count == 0)
            return;

        List<CardAttributes> list = new List<CardAttributes> { pack[0] };

        if (Hands.Contains(new CardList(player.GetComponent<NetworkIdentity>().netId, new List<CardAttributes>())))
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
        /*if(hand.GetComponent<NetworkIdentity>().netId == 1)
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
        else if(hand.GetComponent<NetworkIdentity>().netId == 4)
        {
            Hand4.Add(pack[0]);
        }*/
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
