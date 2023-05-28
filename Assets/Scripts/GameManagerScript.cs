using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class GameManagerScript : MonoBehaviour // Колода
{
    public bool isPackReady;
    public Transform SelfHand;
    public GameObject CardPref;
    // int Move, MoveTime = 30;
    // public TextMeshProUGUI MoveTimeTxt;
    // public Button EndMoveBtn;
    ServerManager ServerCard;
    
    // public List<CardInfoScripts> PlayerHandCards = new List<CardInfoScripts>(),
    //                              PlayerFieldCards = new List<CardInfoScripts>();
    //                              EnemyHandCards = new List<CardInfoScripts>(),
    //                              EnemyFieldCards = new List<CardInfoScripts>();

    // public bool IsPlayerMove
    // {
    //     get
    //     {
    //     return Move % 2 == 0;
    //     } 
            
    // }


    void Update()
    {   

    }
    void Awake()
    {
        
    }
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Field");
        foreach (var player in players)
        {
            if (player.GetComponent<NetworkIdentity>().netId == 1 && player.GetComponent<NetworkIdentity>().isLocalPlayer)
                GiveHandCards(gameObject.GetComponent<ServerManager>().Hand1);
            if (player.GetComponent<NetworkIdentity>().netId == 2 && player.GetComponent<NetworkIdentity>().isLocalPlayer)
                GiveHandCards(gameObject.GetComponent<ServerManager>().Hand2);
            if (player.GetComponent<NetworkIdentity>().netId == 3 && player.GetComponent<NetworkIdentity>().isLocalPlayer)
                GiveHandCards(gameObject.GetComponent<ServerManager>().Hand3);

        }
        // GiveHandCards(ServerCard.PackCards, SelfHand);
    }

    void GiveHandCards (SyncList<CardAttributes> hand)
    {
        if(hand.Count == 4)
        {
            foreach (var item in hand)
            {
                GameObject cardGo = Instantiate(CardPref, SelfHand, false);
                cardGo.GetComponent<CardInfoScripts>().ShowCardInfo(item);
            }
        }
    }
    
    // void GiveHandCards(List<CardAttributes> pack, Transform hand) // Количество карт в руке
    // {
    //     int i = 0;
    //     while (i++ < 4)
    //         GiveCardToHand(pack, hand);
    // }

    // void GiveCardToHand(CardAttributes card, Transform hand) // Выдача карты в руку
    // {
    //     // if (pack.Count == 0)
    //     //     return;

    //     GameObject cardGo = Instantiate(CardPref, hand, false);

    //     cardGo.GetComponent<CardInfoScripts>().HideCardInfo(card);
    //     // PlayerHandCards.Add(cardGo.GetComponent<CardInfoScripts>());

    //     // if (hand == EnemyHand)
    //     // {
    //     //     cardGo.GetComponent<CardInfoScripts>().HideCardInfo(card);
    //     //     // EnemyHandCards.Add(cardGo.GetComponent<CardInfoScripts>());
    //     // }
    //     // else
    //     // {
    //     //     cardGo.GetComponent<CardInfoScripts>().ShowCardInfo(card);
    //     //     // PlayerHandCards.Add(cardGo.GetComponent<CardInfoScripts>());
    //     // }
        
    //     // pack.RemoveAt(0);     

    // }

    /*IEnumerator MoveFunc ()
    {
        MoveTime = 1;
        *//*MoveTimeTxt.text = MoveTime.ToString();*//*

        while (MoveTime-- > 0)
        {
            *//*MoveTimeTxt.text = MoveTime.ToString();*//*
            yield return new WaitForSeconds(1); //Ожидание секунда  
        }
        *//*ChangeMove();*//*
    }

    public void ChangeMove() 
    {
        StopAllCoroutines();
        Move++;

        EndMoveBtn.interactable = IsPlayerMove;

        if (IsPlayerMove)
            GiveNewCards();
        
        StartCoroutine(MoveFunc());
    }

    void GiveNewCards()
    {
        int i=0;
        while(i++ < 2)
            GiveCardToHand(CardVars, SelfHand);
    }*/

}
