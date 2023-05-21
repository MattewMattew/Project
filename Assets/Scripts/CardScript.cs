using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler // Мувмент карты
{
    Camera MainCamera;
    Vector2 offset;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCard;
    public GameManagerScript GameManager;
    public bool IsDraggable; // Наша рука или нет
    void Awake() 
    {
        MainCamera = Camera.main;
        TempCard = GameObject.Find("TempCard");
        GameManager = FindObjectOfType<GameManagerScript>();
    }
    
    public void OnBeginDrag(PointerEventData eventData) //Выполняется единожды при перетягивании объекта // Инициализация
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = DefaultTempCardParent = transform.parent;

        IsDraggable = DefaultParent.GetComponent<DropPlaceScript>().Type == FieldType.SELF_HAND &&
                      GameManager.IsPlayerMove;

        if (!IsDraggable)
            return;

        TempCard.transform.SetParent(DefaultParent);
        TempCard.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) // Выполняется каждый кадр при перетягивании объекта
    {
        if (!IsDraggable)
            return;
        
        Vector2 newPos = MainCamera.ScreenToWorldPoint(eventData.position); // Позиция курсора
        transform.position = newPos + offset; // Фикс взятия карты за её угол

        if (TempCard.transform.parent != DefaultTempCardParent)
            TempCard.transform.SetParent(DefaultTempCardParent);

        Checkposition();
    }

    public void OnEndDrag(PointerEventData eventData) // Выполняется единожды когда отпускается объект
    {
        if (!IsDraggable)
            return;
        
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCard.transform.GetSiblingIndex());
        TempCard.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCard.transform.localPosition = new Vector2(1800,0);
    }

    void Checkposition() // Проверка позиции взятой карты относительно содержимого руки, во избежании багов.
    {
        int newIndex = DefaultTempCardParent.childCount;

        for(int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if (transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;
                
                if(TempCard.transform.GetSiblingIndex() < newIndex)
                    newIndex--;

                break;
            }
        }

        TempCard.transform.SetSiblingIndex(newIndex);
    }

}
