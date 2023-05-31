using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using System;

public class GameManagerScript : MonoBehaviour // Колода
{
    public Transform SelfHand, DiscardTransform;
    public GameObject CardPref;
    public TextMeshProUGUI CountCards;
    GameObject CardDiscard;
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
    void Update()
    {
        CountCards.text = (FindObjectOfType<ServerManager>().PackCards.Count).ToString();
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
            foreach (var hand in FindObjectOfType<ServerManager>().Hands)
                if (hand.Id == player.GetComponent<NetworkIdentity>().netId)
                    foreach (var textMesh in player.GetComponentsInChildren<TextMeshProUGUI>())
                        if (textMesh.tag != "Timer") textMesh.text = hand.Cards.Count.ToString();
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
        GameObject card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, playerController.GetComponentInChildren<DropPlaceScript>().transform, false);
        card.GetComponent<CardInfoScripts>().ShowCardInfo(cardInventory[cardInventory.Count - 1]);
    }

    public void IdentifyCardInDiscard(CardAttributes card)
    {
        if (CardDiscard == null) 
            CardDiscard = Instantiate(CardPref, DiscardTransform, false);
        CardDiscard.GetComponent<CardInfoScripts>().ShowCardInfo(card);
    }
}
