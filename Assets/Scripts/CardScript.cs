using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour, IPointerClickHandler 
{
    public GameObject TempCard;
    public GameManagerScript GameManager;

    void Awake() 
    {
        GameManager = FindObjectOfType<GameManagerScript>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var players = FindObjectsOfType<PlayerNetworkController>();
        bool turnedPlayer = false;
        foreach (var player in players)
        {
            if (player.netId == FindObjectOfType<ServerManager>().turnPlayerId && player.isLocalPlayer)
            {
                turnedPlayer = true;
                break;
            }
        }
        if(transform.parent.tag == "Hand" && turnedPlayer)
        {
            switch (GetComponent<CardInfoScripts>().Name.text)
            {
                case "Bang":
                {
                    UseCardOnEnemy();
                    break;
                }
                case "Barrel":
                {
                    AddCardInventory();
                    break;
                }
                case "Beer":
                {
                    UseCard();
                    break;
                }
                case "Carbine":
                {
                    AddCardInventory();
                    break;
                }
                case "Diligence":
                {
                    UseCard();
                    break;
                }
                case "Duel":
                {
                    UseCardOnEnemy();
                    break;
                }
                case "Dynamite":
                {
                    AddCardInventory();
                    break;
                }
                case "Gatling":
                {
                    UseCard();
                    break;
                }
                case "General":
                {
                    UseCard();
                    break;
                }
                case "Indians":
                {
                    UseCard();
                    break;
                }
                case "Jail":
                {
                    UseCardOnEnemy();
                    break;
                }
                case "Missed":
                {

                    break;
                }
                case "Mustang":
                {
                    AddCardInventory();
                    break;
                }
                case "Panic":
                {
                    UseCardOnEnemy();
                    break;
                }
                case "Remington":
                {
                    AddCardInventory();
                    break;
                }
                case "Saloon":
                {
                    UseCard();
                    break;
                }
                case "Scofield":
                {
                    AddCardInventory();
                    break;
                }
                case "Volcanic":
                {
                    AddCardInventory();
                    break;
                }
                case "WellsFargo":
                {
                    UseCard();
                    break;
                }
                case "Winchester":
                {
                    AddCardInventory();
                    break;
                }
                case "Women":
                {
                    UseCardOnEnemy();
                    break;
                }
                case "Roach":
                {
                    AddCardInventory();
                    break;
                }
            } 
        }
    }

    void AddCardInventory()
    {
        var players = FindObjectsOfType<PlayerNetworkController>();
        foreach (var player in players)
        {
            if (player.isLocalPlayer)
            {
                player.CmdUpdateInventory(GetComponent<CardInfoScripts>().SelfCard, player ,player.transform);
                Destroy(gameObject);
            }
        }
    }

    void UseCard()
    {
        FindObjectOfType<PlayerNetworkController>().CmdGiveCardToDiscard(GetComponent<CardInfoScripts>().SelfCard);
        Destroy(gameObject);
    }

    void UseCardOnEnemy()
    {
        CardScript[] cardsHand = GameObject.Find("SelfHand").GetComponentsInChildren<CardScript>();

        foreach (var item in cardsHand)
        {
            if (item.TempCard != null)
            {
                item.TempCard = null;
                item.transform.localScale = new Vector2(1f, 1f);
            }

            else if (item.transform == gameObject.transform)
            {
                item.TempCard = gameObject;
                transform.localScale = new Vector2(1.2f, 1.2f);
            }
        }
    }

    public void EndTurn()
    {
        TempCard = null;
        transform.localScale = new Vector2(1f, 1f);
    }
}
