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
public class DropPlaceScript : MonoBehaviour,/* IDropHandler, IPointerEnterHandler, IPointerExitHandler */
                               IPointerClickHandler
{
    public FieldType Type;
    public bool PlaceCheck;
    CardScript[] card;

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
    // public void OnPointerEnter(PointerEventData eventData) // Когда зажимаем ЛКМ на карту
    // {
    //     if (eventData.pointerDrag == null || Type == FieldType.ENEMY_FIELD || Type == FieldType.PACK /*||Type == FieldType.ENEMY_HAND*/)
    //         return;

    //     CardScript card = eventData.pointerDrag.GetComponent<CardScript>();

    //     if (card)
    //         card.DefaultTempCardParent = transform;
    // }

    // public void OnPointerExit(PointerEventData eventData) // Когда отпускаем ЛКМ
    // {
    //     if (eventData.pointerDrag == null)
    //         return;

    //     CardScript card = eventData.pointerDrag.GetComponent<CardScript>();

    //     if (card && card.DefaultTempCardParent == transform)
    //         card.DefaultTempCardParent = card.DefaultParent;
    // }
}
