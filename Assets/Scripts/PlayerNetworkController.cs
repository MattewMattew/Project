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
    public void UpdateInvClientRpc(uint id, CardAttributes card)
    {
        print($"{id} player need update inventory!");
        FindObjectOfType<GameManagerScript>().DetectInventory(id, transform, card);
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
        FindObjectOfType<ServerManager>().UpdateInventory(card, id);
    }
    
    private void OnTransformChildrenChanged()
    {
        if (isLocalPlayer && isClient)
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
