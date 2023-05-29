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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Field");
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
            print($"Card given {item.Name}");
            GameObject cardGo = Instantiate(CardPref, SelfHand, false);
            cardGo.GetComponent<CardInfoScripts>().ShowCardInfo(item);
        }
    }
    public void DetectInventory (uint id, Transform inventory, CardAttributes card)
    {
        List<CardAttributes> list = new List<CardAttributes>() { card };
        if (!inventory.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            foreach (var inv in FindObjectOfType<ServerManager>().Inventorys)
            {
                print($"Game manager get {id} id");
                if (inv.Id == id)
                {
                    print($"Player {id} get {inv.Cards.Count} cards");
                    GiveInventoryCard(list, inventory);
                }
            }
        }
    }

    void GiveInventoryCard(List<CardAttributes> cardInventory, Transform inventory)
    {
        if (inventory.childCount < cardInventory.Count)
        {
            GameObject card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, inventory, false);
            card.GetComponent<CardInfoScripts>().ShowCardInfo(cardInventory[cardInventory.Count - 1]);
        }
    }

}
