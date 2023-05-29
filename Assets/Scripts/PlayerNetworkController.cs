using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

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
        FindObjectOfType<GameManagerScript>().DetectInventory(netId, transform);
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
        List<CardAttributes> list = new List<CardAttributes>{ card };

        if (FindObjectOfType<ServerManager>().Inventorys.Contains(new ServerManager.CardList(id, new List<CardAttributes>())))
        {
            foreach (var inventory in FindObjectOfType<ServerManager>().Inventorys)
            {
                if (inventory.Id == id)
                {
                    inventory.Cards.Add(card);
                }
            }            
        }
        else
        {
            FindObjectOfType<ServerManager>().Inventorys.Add(new ServerManager.CardList(id, list));
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
            CmdUpdateInventory(childCards[childCards.Count-1], netId);
        }

    }
}
