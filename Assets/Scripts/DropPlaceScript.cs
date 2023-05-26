using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    // ENEMY_HAND,
    ENEMY_FIELD,
    PACK

}
public class DropPlaceScript : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,/* IDropHandler,  */
                               IPointerClickHandler
{
    public FieldType Type;
    public bool PlaceCheck;
    CardScript[] card;
    private Vector2 pos1;
    private Vector2 pos2;

    public void OnPointerClick(PointerEventData eventData)
    {
        card = FindObjectsOfType<CardScript>();
        foreach (var item in card)
        {
            if(item.TempCard != null) 
            {
                item.transform.SetParent(gameObject.transform);
                item.TempCard = null;
                item.transform.localScale = new Vector2(1f, 1f);
            }
        }
    }



    // public void OnDrop(PointerEventData eventData) // Когда кладем карту в поле
    // {
    //     if (Type != FieldType.SELF_FIELD)
    //         return;
    //     CardScript card = eventData.pointerDrag.GetComponent<CardScript>();

    //     if(card)
    //     {
    //         card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScripts>());
    //         card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoScripts>());
    //     }
    // }
    void Awake(){
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

    // public void OnPointerExit(PointerEventData eventData) // Когда отпускаем ЛКМ
    // {
    //     if (eventData.pointerDrag == null)
    //         return;

    //     CardScript card = eventData.pointerDrag.GetComponent<CardScript>();

    //     if (card && card.DefaultTempCardParent == transform)
    //         card.DefaultTempCardParent = card.DefaultParent;
    // }
}
