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
    GameObject[] card;
    private Vector2 pos1;
    private Vector2 pos2;

    public void OnPointerClick(PointerEventData eventData)
    {
        var players = FindObjectsOfType<PlayerNetworkController>();
        PlayerNetworkController turnedPlayer = new PlayerNetworkController();
        foreach (var player in players)
        {
            if (player.netId == FindObjectOfType<ServerManager>().turnPlayerId) turnedPlayer = player;
        }
        print($"{turnedPlayer.isLocalPlayer} is localplayer is turned");
        card = GameObject.FindGameObjectsWithTag("Card");
        if (FindObjectOfType<ServerManager>().turnPlayerId == GetComponentInParent<NetworkIdentity>().netId && GetComponentInParent<NetworkIdentity>().isLocalPlayer)
        {
            foreach (var item in card)
            {
                if (item.GetComponent<CardScript>().TempCard != null && item.GetComponent<CardInfoScripts>().InfoTypeCard == CardInfoScripts.TypeCard.PERMANENT_CARD)
                {
                    item.transform.SetParent(transform);
                    item.GetComponent<CardScript>().TempCard = null;
                    item.transform.localScale = new Vector2(1f, 1f);
                }
                
            }
        }
        else if(turnedPlayer.isLocalPlayer)
        {

            foreach (var item in card)
            {
                if (item.GetComponent<CardScript>().TempCard != null && item.GetComponent<CardInfoScripts>().InfoTypeCard == CardInfoScripts.TypeCard.DISPOSABLE_CARD)
                {
                    print($"{turnedPlayer.netId} DropPlace");
                    FindObjectOfType<PlayerNetworkController>().CmdGiveCardToDiscard(item.GetComponent<CardInfoScripts>().SelfCard, turnedPlayer.netId);
                    Destroy(item);
                    item.GetComponent<CardScript>().TempCard = null;
                }
            }
        }
    }


    void Awake()
    {
/*        if (!isLocalPlayer)
        {
            Type = FieldType.ENEMY_FIELD;
        }*/
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

    public void OnPointerEnter(PointerEventData eventData) // Когда зажимаем ЛКМ на карту
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
    private void OnTransformChildrenChanged()
    {
        if (GetComponentInParent<NetworkIdentity>().isLocalPlayer && GetComponentInParent<NetworkIdentity>().isClient)
        {
            List<CardAttributes> childCards = new List<CardAttributes>();
            foreach (Transform child in transform)
            {
                childCards.Add(child.GetComponent<CardInfoScripts>().SelfCard);
            }
            GetComponentInParent<PlayerNetworkController>().CmdUpdateInventory(childCards[childCards.Count - 1], GetComponentInParent<PlayerNetworkController>(), transform);
        }

    }
}
