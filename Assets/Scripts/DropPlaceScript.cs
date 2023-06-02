using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    PACK,
    DISCARD

}

public class DropPlaceScript : MonoBehaviour, IPointerEnterHandler,
                               IPointerExitHandler, IPointerClickHandler
{
    public FieldType Type;
    private Vector2 pos1;
    private Vector2 pos2;

    public void OnPointerClick(PointerEventData eventData)
    {
/*        var players = FindObjectsOfType<PlayerNetworkController>();
        PlayerNetworkController turnedPlayer = new PlayerNetworkController();
        foreach (var player in players)
        {
            if (player.netId == FindObjectOfType<ServerManager>().turnPlayerId) turnedPlayer = player;
        }*/
        // print($"{turnedPlayer.isLocalPlayer} is localplayer is turned");
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (var card in cards)
        {
            if (!GetComponentInParent<NetworkIdentity>().isLocalPlayer)
            {
                if (card.GetComponent<CardScript>().TempCard != null)
                {
                    if(card.GetComponent<CardInfoScripts>().Name.text == "Jail")
                    {
                        card.transform.SetParent(transform);
                        card.GetComponent<CardScript>().TempCard = null;
                        card.transform.localScale = new Vector2(1f, 1f);
                        GetComponentInParent<PlayerNetworkController>().CmdUpdateInventory(card.GetComponent<CardInfoScripts>().SelfCard, 
                                                                            GetComponentInParent<PlayerNetworkController>(), transform);
                        Destroy(card);
                    }
                    else
                    {
                        FindObjectOfType<PlayerNetworkController>().CmdGiveCardToDiscard(card.GetComponent<CardInfoScripts>().SelfCard);
                            // FindObjectOfType<ServerManager>().turnPlayerId, 
                            // GetComponentInParent<NetworkIdentity>().netId);
                        Destroy(card);
                        card.GetComponent<CardScript>().TempCard = null;
                        
                    }
                    // FindObjectOfType<PlayerNetworkController>().CmdRemoveCard(item.GetComponent<CardInfoScripts>().SelfCard, turnedPlayer.netId);
                }   
            }
        }
    }

    void Awake()
    {
        pos1 = transform.localPosition;
        pos2 = new Vector2(0,transform.localPosition.y+99f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {   
        if (Type == FieldType.SELF_HAND)
        {
            StopAllCoroutines();
            StartCoroutine(TransfordSelfHand(transform.localPosition, pos1, 0.2f));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Type == FieldType.SELF_HAND)
        {
            StopAllCoroutines();
            StartCoroutine(TransfordSelfHand(transform.localPosition, pos2, 0.2f));
        }
    }

    private  IEnumerator TransfordSelfHand(Vector2 posstart,Vector2 posend, float time){
        float t = 0;
        do
        {
            transform.localPosition = Vector2.Lerp(posstart,posend,t/time);
            t +=Time.deltaTime;
            yield return null;
        } while (t<=time);

    }
}
