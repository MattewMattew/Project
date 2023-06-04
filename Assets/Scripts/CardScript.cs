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
                                UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                                turnPlayer.CmdDefense();

                            }
                            else UseCardOnEnemy(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
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
                        UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                        break;
                    }
                    case "Carbine":
                    {
                        AddCardInventory();
                        break;
                    }
                    case "Diligence":
                    {
                        turnPlayer.CmdGiveHandCards(turnPlayer.netId, 2);
                        UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                        break;
                    }
                    case "Duel":
                    {
                        UseCardOnEnemy(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                        break;
                    }
                    case "Dynamite":
                    {
                        AddCardInventory();
                        break;
                    }
                    case "Gatling":
                    {
                        UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                        break;
                    }
                    case "General":
                    {
                        UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                        break;
                    }
                    case "Indians":
                    {
                        UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                        break;
                    }
                    case "Jail":
                    {
                        UseCardOnEnemy(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                        break;
                    }
                    case "Missed":
                        {
                            if (FindObjectOfType<ServerManager>().turnModificator == "Attack")
                            {
                                UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
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
                        UseCardOnEnemy(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
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
                        UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
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
                        turnPlayer.CmdGiveHandCards(turnPlayer.netId, 3);
                        UseCard(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
                        break;
                    }
                    case "Winchester":
                    {
                        AddCardInventory();
                        break;
                    }
                    case "Women":
                    {
                        UseCardOnEnemy(turnPlayer.netId, GetComponent<CardInfoScripts>().SelfCard);
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
                if (GetComponent<CardInfoScripts>().InfoTypeCard == CardInfoScripts.TypeCard.WEAPON_CARD)
                {
                    GameObject[] Inventory = GameObject.FindGameObjectsWithTag("Field");
                    foreach (var inv in Inventory)
                    {
                        if(inv.GetComponentInParent<PlayerNetworkController>().isLocalPlayer)
                            foreach (var item in inv.GetComponentsInChildren<CardInfoScripts>())
                            {
                                if (item.InfoTypeCard == CardInfoScripts.TypeCard.WEAPON_CARD)
                                {
                                    print("Done");
                                    player.CmdRemoveCardFromInventory(item.SelfCard, player.netId);
                                    Destroy(item.gameObject);
                                }
                            }
                    }
                }
                player.CmdUpdateInventory(GetComponent<CardInfoScripts>().SelfCard, player ,player.transform);
                Destroy(gameObject);
            }
        }
    }

    void UseCard(uint id, CardAttributes card)
    {
        FindObjectOfType<PlayerNetworkController>().CmdRemoveCardFromHand(id, card);
        FindObjectOfType<PlayerNetworkController>().CmdGiveCardToDiscard(GetComponent<CardInfoScripts>().SelfCard);
        Destroy(gameObject);
    }

    void UseCardOnEnemy(uint id, CardAttributes card)
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
