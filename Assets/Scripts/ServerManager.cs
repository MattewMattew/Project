using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    public readonly SyncList<CardAttributes> PackCards = new SyncList<CardAttributes>();
    public GameCard CurrentGame;
    public List<Card> CardVars;
    public readonly SyncList<CardAttributes> Hand1 = new SyncList<CardAttributes>();
    public readonly SyncList<CardAttributes> Hand2 = new SyncList<CardAttributes>();
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
    void SyncPackList(SyncList<CardAttributes>.Operation op, int index, Card oldItem, Card newItem)
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
    }
    void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        if (isServer)
        {
            CmdCardAdded();
            GameObject[] players = GameObject.FindGameObjectsWithTag("Field");
            foreach (var player in players)
            {
                print(player.GetComponent<NetworkIdentity>().netId);
                GiveHandCards(PackCards, player);
            }
        }
        foreach (var item in PackCards)
        {
            print(item.Name);
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

        /*        GameObject cardGo = Instantiate(CardPref, hand, false);
                NetworkServer.Spawn(cardGo);*/

        /*        if (hand == EnemyHand)
                {
                    cardGo.GetComponent<CardInfoScripts>().HideCardInfo(card);
                    EnemyHandCards.Add(cardGo.GetComponent<CardInfoScripts>());
                }
                else
                {
                    cardGo.GetComponent<CardInfoScripts>().ShowCardInfo(card);
                    PlayerHandCards.Add(cardGo.GetComponent<CardInfoScripts>());
                }*/
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
