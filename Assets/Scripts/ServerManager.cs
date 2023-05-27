using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    public readonly SyncList<CardAttributes> PackCards = new SyncList<CardAttributes>();
    public GameCard CurrentGame;
    public List<Card> CardVars;


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
        }
        foreach (var item in PackCards)
        {
            print(item.Name);
        }
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
