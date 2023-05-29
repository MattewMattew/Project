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
    // ENEMY_HAND,
    ENEMY_FIELD,
    PACK

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
        if (FindObjectOfType<ServerManager>().turnPlayerId == transform.GetComponent<NetworkIdentity>().netId && transform.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            card = GameObject.FindGameObjectsWithTag("Card");
            foreach (var item in card)
            {
                if (item.GetComponent<CardScript>().TempCard != null /*&& item.GetComponent<CardInfoScripts>().InfoTypeCard == CardInfoScripts.TypeCard.PERMANENT_CARD*/)
                {
                    item.transform.SetParent(gameObject.transform);
                    item.GetComponent<CardScript>().TempCard = null;
                    item.transform.localScale = new Vector2(1f, 1f);
                }
            }
        }
        else if(FindObjectOfType<ServerManager>().turnPlayerId == transform.GetComponent<NetworkIdentity>().netId)
        {
            card = GameObject.FindGameObjectsWithTag("Card");
            foreach (var item in card)
            {
                if (item.GetComponent<CardScript>().TempCard != null && item.GetComponent<CardInfoScripts>().InfoTypeCard == CardInfoScripts.TypeCard.DISPOSABLE_CARD)
                {
                    Destroy(item); 
                    item.GetComponent<CardScript>().TempCard = null;
                }
            }
            
        }
        // print($"Now be turn is {FindObjectOfType<ServerManager>().turnPlayerId} player!");
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
}
