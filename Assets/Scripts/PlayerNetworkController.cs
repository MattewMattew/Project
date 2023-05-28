using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using static UnityEditor.Progress;

public class PlayerNetworkController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    // Update is called once per frame
    public void setPlayerPosition(Vector2 pos)
    {
        transform.SetParent(GameObject.Find("Background").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = pos;
    }
    public void UpdateInventory(SyncList<CardAttributes> cards)
    {
        foreach (var item in cards)
        {
            GameObject card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, transform, false);
            card.GetComponent<CardInfoScripts>().ShowCardInfo(item);
        }
    }
    private void OnTransformChildrenChanged()
    {
        List<CardAttributes> childCards = new List<CardAttributes>();
        foreach (Transform child in transform)
        {
            childCards.Add(child.GetComponent<CardInfoScripts>().SelfCard);
            print($"{child.GetComponent<CardInfoScripts>().SelfCard.Name} name of cards in id {GetComponent<NetworkIdentity>().netId}");
        }
        foreach (CardAttributes card in childCards)
        {
            if(GetComponent<NetworkIdentity>().netId == 1)
            {
                FindObjectOfType<ServerManager>().Inventory1.Add(card);
            }
            if(GetComponent<NetworkIdentity>().netId == 2)
            {
                FindObjectOfType<ServerManager>().Inventory2.Add(card);
            }
            if(GetComponent<NetworkIdentity>().netId == 3)
            {
                FindObjectOfType<ServerManager>().Inventory3.Add(card);
            }
        }
/*        GameObject[] players = GameObject.FindGameObjectsWithTag("Field");
        foreach (var item in players)
        {
            if(item.GetComponent<NetworkIdentity>().netId == 1 && !item.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                item.GetComponent<PlayerNetworkController>().UpdateInventory(FindObjectOfType<ServerManager>().Inventory1);
            }
            if(item.GetComponent<NetworkIdentity>().netId == 2 && !item.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                item.GetComponent<PlayerNetworkController>().UpdateInventory(FindObjectOfType<ServerManager>().Inventory2);
            }
            if(item.GetComponent<NetworkIdentity>().netId == 3 && !item.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                item.GetComponent<PlayerNetworkController>().UpdateInventory(FindObjectOfType<ServerManager>().Inventory3);
            }
        }*/
    }
}
