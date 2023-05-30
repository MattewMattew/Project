using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using System;

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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            foreach (var hand in FindObjectOfType<ServerManager>().Hands)
            {
                if (hand.Id == player.GetComponent<NetworkIdentity>().netId && player.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    GiveHandCards(hand.Cards);
                }
            }
        }
    }

    void GiveHandCards (List<CardAttributes> hand)
    {
        foreach (var item in hand)
        {
            GameObject cardGo = Instantiate(CardPref, SelfHand, false);
            cardGo.GetComponent<CardInfoScripts>().ShowCardInfo(item);
        }
    }
    public void DetectInventory (PlayerNetworkController playerController, Transform inventory, CardAttributes card)
    {
        print($"{playerController.isLocalPlayer} in GameManager");
        List<CardAttributes> list = new List<CardAttributes>() { card };
        if (!playerController.isLocalPlayer)
        {
            foreach (var inv in FindObjectOfType<ServerManager>().Inventorys)
            {
                if (inv.Id == playerController.netId)
                {
                    GiveInventoryCard(list, playerController);
                }
            }
        }
    }

    void GiveInventoryCard(List<CardAttributes> cardInventory, PlayerNetworkController playerController)    
    {
        print($"Give to {playerController.netId} inventory");
        GameObject card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, playerController.GetComponentInChildren<DropPlaceScript>().transform, false);
        card.GetComponent<CardInfoScripts>().ShowCardInfo(cardInventory[cardInventory.Count - 1]);
    }

}
