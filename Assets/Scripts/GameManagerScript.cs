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

    }
    void Start()
    {

    }

    public void GiveHandCards (uint id, CardAttributes card)
    {
        foreach (var item in FindObjectsOfType<PlayerNetworkController>())
        {
            if(item.netId == id && item.isLocalPlayer)
            {
                GameObject cardGo = Instantiate(CardPref, SelfHand, false);
                cardGo.GetComponent<CardInfoScripts>().ShowCardInfo(card);  
            }
        }
    }
    public void DetectInventory (PlayerNetworkController playerController, Transform inventory, CardAttributes card)
    {
        List<CardAttributes> list = new List<CardAttributes>() { card };

            foreach (var inv in FindObjectOfType<ServerManager>().Inventorys)
            {
                if (inv.Id == playerController.netId)
                {
                    GiveInventoryCard(list, playerController);
                }
            }
    }

    public void RemoveCardFromInventory(uint id, CardAttributes card)
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Field"))
        {
            if (item.GetComponentInParent<PlayerNetworkController>().netId == id)
            {
                foreach (Transform chield in item.transform)
                {
                    if (chield.GetComponent<CardInfoScripts>().SelfCard.Name == card.Name)
                        Destroy(chield.gameObject);
                }
            }
        }
    }

    void GiveInventoryCard(List<CardAttributes> cardInventory, PlayerNetworkController playerController)    
    {
        print("Spawn Inv");
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
