using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class GameManagerScript : MonoBehaviour // Колода
{
    public Transform SelfHand;
    public GameObject CardPref;
    // int Move, MoveTime = 30;
    // public TextMeshProUGUI MoveTimeTxt;
    // public Button EndMoveBtn;
    // public bool IsPlayerMove
    // {
    //     get
    //     {
    //     return Move % 2 == 0;
    //     } 
            
    // }

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
            if (player.GetComponent<NetworkIdentity>().netId == 4 && player.GetComponent<NetworkIdentity>().isLocalPlayer)
                GiveHandCards(gameObject.GetComponent<ServerManager>().Hand4);

        }
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
    public void DetectInventory (uint id, Transform inventory)
    {
        if (!inventory.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            if (id == 1) GiveInventoryCard(FindObjectOfType<ServerManager>().Inventory1, inventory);
            if (id == 2) GiveInventoryCard(FindObjectOfType<ServerManager>().Inventory2, inventory);
            if (id == 3) GiveInventoryCard(FindObjectOfType<ServerManager>().Inventory3, inventory);
            if (id == 4) GiveInventoryCard(FindObjectOfType<ServerManager>().Inventory4, inventory);
        }
    }

    void GiveInventoryCard(SyncList<CardAttributes> cardInventory, Transform inventory)
    {
        if (inventory.childCount < cardInventory.Count)
        {
            GameObject card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, inventory, false);
            card.GetComponent<CardInfoScripts>().ShowCardInfo(cardInventory[cardInventory.Count - 1]);
        }
    }

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
