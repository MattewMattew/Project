using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    // ENYME_HAND,
    ENYME_FIELD,
    PACK

    //fhgfghfhhftrbghghh
}
public class DropPlaceScript : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public FieldType Type;
    public void OnDrop(PointerEventData eventData)
    {
        if (Type != FieldType.SELF_FIELD)
            return;
        CardScript card = eventData.pointerDrag.GetComponent<CardScript>();

        if(card)
            card.DefaultParent = transform;
    }
    //djhklfjljgpoijpjpfg
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || Type == FieldType.ENYME_FIELD || Type == FieldType.PACK /*||Type == FieldType.ENYME_HAND*/)
            return;

        CardScript card = eventData.pointerDrag.GetComponent<CardScript>();

        if (card)
            card.DefaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        CardScript card = eventData.pointerDrag.GetComponent<CardScript>();

        if (card && card.DefaultTempCardParent == transform)
            card.DefaultTempCardParent = card.DefaultParent;
    }
}
