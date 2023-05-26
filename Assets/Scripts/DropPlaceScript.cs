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

public class DropPlaceScript : NetworkBehaviour, IPointerEnterHandler,
                               IPointerExitHandler, IPointerClickHandler
{
    SyncList<GameObject> cards = new SyncList<GameObject>();
    public FieldType Type;
    GameObject[] card;
    private Vector2 pos1;
    private Vector2 pos2;
    public List<GameObject> TransformVars;

    [Server]
    public void CmdChildrenAdded(GameObject item)
    {
        Debug.Log(item);
        cards.Add(item);
    }
    [Command]
    public void ChildrenAdded(GameObject item)
    {
        CmdChildrenAdded(item);
    }

    public override void OnStartClient()
    {
        
        base.OnStartClient();

        cards.Callback += SyncTransformVars; //вместо hook, для SyncList используем подписку на Callback

        TransformVars = new List<GameObject>(cards.Count); //так как Callback действует только на изменение массива,  
        for (int i = 0; i < cards.Count; i++) //а у нас на момент подключения уже могут быть какие-то данные в массиве, нам нужно эти данные внести в локальный массив
        {
            TransformVars.Add(cards[i]);
        }
    }

    void SyncTransformVars(SyncList<GameObject>.Operation op, int index, GameObject oldItem, GameObject newItem)
    {
        switch (op)
        {
            case SyncList<GameObject>.Operation.OP_ADD:
                {
                    TransformVars.Add(newItem);
                    break;
                }
            case SyncList<GameObject>.Operation.OP_CLEAR:
                {

                    break;
                }
            case SyncList<GameObject>.Operation.OP_INSERT:
                {

                    break;
                }   
            case SyncList<GameObject>.Operation.OP_REMOVEAT:
                {

                    break;
                }
            case SyncList<GameObject>.Operation.OP_SET:
                {

                    break;
                }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        card = GameObject.FindGameObjectsWithTag("Card");
        foreach (var item in card)
        {
            if(item.GetComponent<CardScript>().TempCard != null) 
            {
                item.transform.SetParent(gameObject.transform);
                item.GetComponent<CardScript>().TempCard = null;
                item.transform.localScale = new Vector2(1f, 1f);

                item.GetComponent<CardScript>().GameManager.PlayerHandCards.Remove(item.GetComponent<CardInfoScripts>());
                item.GetComponent<CardScript>().GameManager.PlayerFieldCards.Add(item.GetComponent<CardInfoScripts>());
                if (isServer)
                    ChildrenAdded(item);
                else
                    CmdChildrenAdded(item);
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
}
