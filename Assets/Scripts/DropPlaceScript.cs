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
                        FindObjectOfType<PlayerNetworkController>().CmdRemoveCardFromHand(FindObjectOfType<ServerManager>().turnPlayerId,
                                            card.GetComponent<CardInfoScripts>().SelfCard);
                        Destroy(card);
                    }
                    else
                    {
                        FindObjectOfType<PlayerNetworkController>().CmdGiveCardToDiscard(card.GetComponent<CardInfoScripts>().SelfCard);
                        if (card.GetComponent<CardInfoScripts>().SelfCard.Name == "Bang")
                            GetComponentInParent<PlayerNetworkController>().CmdAttack(card.GetComponent<CardInfoScripts>().SelfCard.Name);
                        if (card.GetComponent<CardInfoScripts>().SelfCard.Name == "Duel")
                            GetComponentInParent<PlayerNetworkController>().CmdDuel(FindObjectOfType<ServerManager>().turnPlayerId, 
                                                                             GetComponentInParent<PlayerNetworkController>().netId);
                        card.GetComponent<CardScript>().TempCard = null;
                        FindObjectOfType<PlayerNetworkController>().CmdRemoveCardFromHand(FindObjectOfType<ServerManager>().turnPlayerId, 
                                                                    card.GetComponent<CardInfoScripts>().SelfCard);
                        Destroy(card);
                    }
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
