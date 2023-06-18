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
        if(transform.tag == "Player")
        {
            foreach (var card in GameObject.FindGameObjectsWithTag("Card"))
            {
                if (!GetComponent<PlayerNetworkController>().isLocalPlayer)
                {
                    if (card.GetComponent<CardScript>().TempCard != null)
                    {
                        if(card.GetComponent<CardInfoScripts>().Name == "Jail")
                        {
                            foreach (var inv in GameObject.FindGameObjectsWithTag("Field"))
                            {
                                if(inv.GetComponentInParent<PlayerNetworkController>().netId == GetComponent<PlayerNetworkController>().netId)
                                {
                                    card.transform.SetParent(inv.transform);
                                    card.GetComponent<CardScript>().TempCard = null;
                                    card.transform.localScale = new Vector2(1f, 1f);
                                    GetComponent<PlayerNetworkController>().CmdUpdateInventory(card.GetComponent<CardInfoScripts>().SelfCard, 
                                                                            GetComponent<PlayerNetworkController>(), transform);
                                    FindObjectOfType<PlayerNetworkController>().CmdRemoveCardFromHand(FindObjectOfType<ServerManager>().turnPlayerId,
                                                                                card.GetComponent<CardInfoScripts>().SelfCard);
                                    Destroy(card);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (card.GetComponent<CardInfoScripts>().SelfCard.Name == "Bang") 
                            {
                                print($"Range{GetComponent<PlayerNetworkController>().Range} || ID {GetComponent<PlayerNetworkController>().netId}");
                                foreach (var inv in GameObject.FindGameObjectsWithTag("Field"))
                                {
                                    if (inv.GetComponentInParent<PlayerNetworkController>().isLocalPlayer)
                                    {
                                        foreach (var cardInv in inv.GetComponentsInChildren<CardInfoScripts>())
                                        {
                                            if (cardInv.InfoTypeCard == CardInfoScripts.TypeCard.WEAPON_CARD)
                                                if (GetComponent<PlayerNetworkController>().Range <= cardInv.WeaponRange)
                                                {
                                                    DeleteCard(card);
                                                    GetComponent<PlayerNetworkController>().CmdAttack(card.GetComponent<CardInfoScripts>().SelfCard.Name);
                                                    return;
                                                }
                                                
                                        }
                                    }
                                }
                                if (GetComponent<PlayerNetworkController>().Range <= 1)
                                {
                                    GetComponent<PlayerNetworkController>().CmdAttack(card.GetComponent<CardInfoScripts>().SelfCard.Name);
                                    DeleteCard(card);
                                    return;
                                }
                            } 
                            if (card.GetComponent<CardInfoScripts>().SelfCard.Name == "Duel")
                            {
                                GetComponent<PlayerNetworkController>().CmdDuel(FindObjectOfType<ServerManager>().turnPlayerId, 
                                                                        GetComponent<PlayerNetworkController>().netId);
                                DeleteCard(card);
                                return;
                            }

                            if(card.GetComponent<CardInfoScripts>().SelfCard.Name == "Women" || card.GetComponent<CardInfoScripts>().SelfCard.Name == "Panic")
                            foreach (var hand in FindObjectOfType<ServerManager>().Hands)
                            {
                                if (hand.Id == GetComponent<PlayerNetworkController>().netId)
                                {
                                    if (hand.Cards.Count > 0)
                                    {
                                        if(card.GetComponent<CardInfoScripts>().SelfCard.Name == "Women")
                                        {
                                            GetComponentInParent<PlayerNetworkController>().CmdWomenPanicAction(GetComponentInParent<PlayerNetworkController>().netId, 
                                                card.GetComponent<CardInfoScripts>().SelfCard);
                                            DeleteCard(card);
                                            GetComponent<PlayerNetworkController>().CmdRandomRemoveCardFromHand(GetComponent<PlayerNetworkController>(),
                                                                                            card.GetComponent<CardInfoScripts>().SelfCard);
                                            return;
                                        }
                                        if (card.GetComponent<CardInfoScripts>().SelfCard.Name == "Panic")
                                        {
                                             GetComponentInParent<PlayerNetworkController>().CmdWomenPanicAction(GetComponentInParent<PlayerNetworkController>().netId,
                                                card.GetComponent<CardInfoScripts>().SelfCard);
                                             GetComponent<PlayerNetworkController>().CmdRandomRemoveCardFromHand(GetComponent<PlayerNetworkController>(),
                                                                                            card.GetComponent<CardInfoScripts>().SelfCard);
                                            DeleteCard(card);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        card.GetComponent<CardScript>().TempCard = null;
                                        card.transform.localScale = new Vector2(1f, 1f);
                                        return;
                                    }
                                }
                            }
                             
                            
                        }
                    }   
                }
            }
        }
    }

    void DeleteCard(GameObject card)
    {
        card.GetComponent<CardScript>().TempCard = null;
        FindObjectOfType<PlayerNetworkController>().CmdRemoveCardFromHand(FindObjectOfType<ServerManager>().turnPlayerId, 
                                                card.GetComponent<CardInfoScripts>().SelfCard);
        FindObjectOfType<PlayerNetworkController>().CmdGiveCardToDiscard(card.GetComponent<CardInfoScripts>().SelfCard);
        Destroy(card);
    }

    void Awake()
    {
        pos1 = transform.localPosition;
        pos2 = new Vector2(0,transform.localPosition.y+92f);
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
