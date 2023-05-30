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
    public void UpdateInvClientRpc(PlayerNetworkController playerController, CardAttributes card, Transform playerInventory)
    {
        print($"{playerController.netId} player need update inventory!");
        FindObjectOfType<GameManagerScript>().DetectInventory(playerController, playerInventory, card);
    }
    public void setPlayerPosition(Vector2 pos)
    {
        print(pos);
        transform.SetParent(GameObject.Find("Background").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = pos;
    }
    [Command(requiresAuthority = false)]
    public void CmdUpdateInventory(CardAttributes card, PlayerNetworkController playerController, Transform playerInventory)
    {
        FindObjectOfType<ServerManager>().UpdateInventory(card, playerController, playerInventory);
    }

    [Command(requiresAuthority = false)]
    public void CmdGiveCardToDiscard(CardAttributes card)
    {
        FindObjectOfType<ServerManager>().GiveCardToDiscard(card);
    }
}
