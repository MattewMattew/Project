using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScript : NetworkBehaviour, IPointerClickHandler 
{
    public GameObject TempCard;
    public GameManagerScript GameManager;

    void Awake() 
    {
        GameManager = FindObjectOfType<GameManagerScript>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(transform.parent.tag == "Field")
            return;

        CardScript[] check = FindObjectsOfType<CardScript>();

        foreach (var item in check)
        {
            if(item.TempCard != null)
            {
                item.TempCard = null;
                item.transform.localScale = new Vector2(1f, 1f);
            }

            else if(item.transform == gameObject.transform)
            {
                item.TempCard = gameObject;
                transform.localScale = new Vector2(1.2f, 1.2f);
            }

        }
    }
}
