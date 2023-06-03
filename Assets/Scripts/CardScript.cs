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
        // bool turnedPlayer = false;
        PlayerNetworkController turnPlayer = new PlayerNetworkController();
        foreach (var player in players)
        {
            if (player.netId == FindObjectOfType<ServerManager>().turnPlayerId && player.isLocalPlayer)
            {
                turnPlayer = player;
                break;
            }
            if (player.netId == FindObjectOfType<ServerManager>().attackedPlayerId && player.isLocalPlayer)
            {
                turnPlayer = player;
                // turnedPlayer = true;
                break;
            }

        }
//         if (FindObjectOfType<ServerManager>().turnModificator == "Attack")
//         {
//             if (transform.parent.tag == "Hand" && turnPlayer)
//             {
//                 switch (GetComponent<CardInfoScripts>().Name.text)
//                 {
// /*                    case "Bang":
//                         {
//                             UseCardOnEnemy();
//                             break;
//                         }*/
//                     case "Missed":
//                         {
//                             UseCard();
//                             turnPlayer.CmdDefense();
//                             break;
//                         }
//                     default:
//                         {
//                             print("You has been attacked");
//                             break;
//                         }
//                 }
//             }
//         }
        // else
        {
            if(transform.parent.tag == "Hand" && turnPlayer)
            {
                switch (GetComponent<CardInfoScripts>().Name.text)
                {
                    case "Bang":
                        {
                            if (FindObjectOfType<ServerManager>().turnModificator == "Attack")
                            {
                                UseCard();
                                turnPlayer.CmdDefense();

                            }
                            else UseCardOnEnemy();
                            break;
                        }
                    case "Barrel":
                    {
                        AddCardInventory();
                        break;
                    }
                    case "Beer":
                    {
                        turnPlayer.CmdRegenerationHealth(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
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
                        turnPlayer.CmdGiveHandCards(FindObjectOfType<ServerManager>().PackCards, turnPlayer.netId, 2);
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
                            if (FindObjectOfType<ServerManager>().turnModificator == "Attack")
                            {
                                UseCard();
                                turnPlayer.CmdDefense();
                                
                            }
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
                        turnPlayer.CmdRegenerationHealth(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
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
                        turnPlayer.CmdGiveHandCards(FindObjectOfType<ServerManager>().PackCards, turnPlayer.netId, 3);
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
    }

    void AddCardInventory()
    {
        var players = FindObjectsOfType<PlayerNetworkController>();
        foreach (var player in players)
        {
            if (player.isLocalPlayer)
            {
                // if (GetComponent<CardInfoScripts>().SelfCard.Name == "Winchester" || GetComponent<CardInfoScripts>().SelfCard.Name == "Volcanic" || 
                //     GetComponent<CardInfoScripts>().SelfCard.Name == "Scofield" ||GetComponent<CardInfoScripts>().SelfCard.Name == "Remington" ||
                //     GetComponent<CardInfoScripts>().SelfCard.Name == "Carbine")
                // {
                //     GameObject[] CardsInventory = player.GetComponentsInChildren<GameObject>();
                //     foreach (var card in CardsInventory)
                //     {
                        
                //     }
                // }
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
