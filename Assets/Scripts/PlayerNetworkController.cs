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
/*    void Update()
    {


    }*/


    [ClientRpc]
    public void UpdateInvClientRpc()
    {

        if (FindObjectOfType<ServerManager>() && !isLocalPlayer)
        {
            if (netId == 1)
            {
                print($"{transform.childCount} transform childCount");
                if (transform.childCount < FindObjectOfType<ServerManager>().Inventory1.Count)
                {
                    print($"id {netId} need update inventory");
                    GameObject card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, transform, false);
                    card.GetComponent<CardInfoScripts>().ShowCardInfo(FindObjectOfType<ServerManager>().Inventory1[FindObjectOfType<ServerManager>().Inventory1.Count - 1]);
                }
            }
            if (netId == 2)
            {
                print($"{transform.childCount} transform childCount");
                if (transform.childCount < FindObjectOfType<ServerManager>().Inventory2.Count)
                {
                    print($"id {netId} need update inventory");
                    GameObject card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, transform, false);
                    card.GetComponent<CardInfoScripts>().ShowCardInfo(FindObjectOfType<ServerManager>().Inventory2[FindObjectOfType<ServerManager>().Inventory2.Count - 1]);
                }
            }
            if (netId == 3)
            {
                print($"{transform.childCount} transform childCount");
                if (transform.childCount < FindObjectOfType<ServerManager>().Inventory3.Count)
                {
                    print($"id {netId} need update inventory");
                    GameObject card = Instantiate(FindObjectOfType<GameManagerScript>().CardPref, transform, false);
                    card.GetComponent<CardInfoScripts>().ShowCardInfo(FindObjectOfType<ServerManager>().Inventory3[FindObjectOfType<ServerManager>().Inventory3.Count - 1]);
                }
            }
        }
    }
    public void setPlayerPosition(Vector2 pos)
    {
        transform.SetParent(GameObject.Find("Background").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = pos;
    }
    [Command(requiresAuthority = false)]
    public void CmdUpdateInventory(CardAttributes card, uint id)
    {
        UpdateInventory(card, id);
    }
    [Server]
    public void UpdateInventory(CardAttributes card, uint id)
    {
        if(id == 1)
        {

            FindObjectOfType<ServerManager>().Inventory1.Add(card);
        }
        if(id == 2)
        {

            FindObjectOfType<ServerManager>().Inventory2.Add(card);
        }
        if(id == 3)
        {

            FindObjectOfType<ServerManager>().Inventory3.Add(card);
        }
        UpdateInvClientRpc();
    }
    private void OnTransformChildrenChanged()
    {
        if (isLocalPlayer)
        {
            List<CardAttributes> childCards = new List<CardAttributes>();
            foreach (Transform child in transform)
            {
                childCards.Add(child.GetComponent<CardInfoScripts>().SelfCard);
                print($"{child.GetComponent<CardInfoScripts>().SelfCard.Name} name of cards in id {GetComponent<NetworkIdentity>().netId}");
            }
            if(GetComponent<NetworkIdentity>().netId == 1)
            {
                CmdUpdateInventory(childCards[childCards.Count-1], netId);
            }
            if(GetComponent<NetworkIdentity>().netId == 2)
            {
                CmdUpdateInventory(childCards[childCards.Count - 1], netId);
            }
            if(GetComponent<NetworkIdentity>().netId == 3)
            {
                CmdUpdateInventory(childCards[childCards.Count - 1], netId);
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
