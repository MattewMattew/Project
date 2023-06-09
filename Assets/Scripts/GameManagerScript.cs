using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using System;

public class GameManagerScript : MonoBehaviour // Колода
{
    private GameObject Anim;
    public Transform SelfHand, DiscardTransform;
    public GameObject CardPref;
    public TextMeshProUGUI CountCards;
    public GameObject CardDiscard;

    void Awake()
    {
        
    }
    void Update()
    {
        CountCards.text = (FindObjectOfType<ServerManager>().PackCards.Count).ToString();
        GameObject.Find("DisCount").GetComponent<TextMeshProUGUI>().text = (FindObjectOfType<ServerManager>().Discard.Count).ToString();
    }
    void Start()
    {
        Anim = GameObject.FindWithTag("Anim");
        Anim.GetComponent<Animator>().Play("Opacity");
    }

    public void ButtonActivation(uint id)
    {
        foreach (var player in FindObjectsOfType<PlayerNetworkController>())
        {
            if (player.isLocalPlayer)
            {
                if (player.netId == id) FindObjectOfType<Button>().enabled = true;
                else FindObjectOfType<Button>().enabled = false;
            }
        }
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
    public void RemoveCardFromHand(CardAttributes card)
    {
        foreach (var item in FindObjectsOfType<CardInfoScripts>())
        {
            if(item.transform.parent.tag == "Hand")
            {
                if(item.SelfCard.Name == card.Name && item.SelfCard.Suit == card.Suit && item.SelfCard.Dignity == card.Dignity)
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }
    void GiveInventoryCard(List<CardAttributes> cardInventory, PlayerNetworkController playerController)    
    {
        GameObject card = null;
        foreach (var inv in GameObject.FindGameObjectsWithTag("Field"))
        {
            if (inv.GetComponentInParent<PlayerNetworkController>().netId == playerController.netId)
            {
                card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, inv.transform, false);
                card.GetComponent<CardInfoScripts>().ShowCardInfo(cardInventory[cardInventory.Count - 1]);
                break;
            }
        }
    }

    public void IdentifyCardInDiscard(CardAttributes card)
    {
        if (CardDiscard == null) 
            CardDiscard = Instantiate(CardPref, DiscardTransform, false);
        CardDiscard.GetComponent<CardInfoScripts>().ShowCardInfo(card);
    }
    public void UpdateCountCards(int cardCount, uint id)
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("CardCount"))
        {
            if (player.GetComponentInParent<PlayerNetworkController>().netId == id)
                player.GetComponent<TextMeshProUGUI>().text = cardCount.ToString();
        }
    }


}
