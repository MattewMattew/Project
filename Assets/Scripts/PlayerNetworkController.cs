using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using TMPro;
using System.Linq;
// using UnityEditor.Localization.Plugins.XLIFF.V20;

public class PlayerNetworkController : NetworkBehaviour
{
    public List<Material> Materials; 
    // Start is called before the first frame update
    void Start()
    {

    }
    /*    void Update()
        {


        }*/
    [ClientRpc]
    public void TimerUpdateClientRpc(int timer, uint id)
    {
        TextMeshProUGUI[] values = GetComponentsInChildren<TextMeshProUGUI>();
        if (netId == id)
        {
            foreach (var value in values)
            {
                if(value.tag == "Timer")
                {
                    // print($"ative {id} {value.GetComponent<TextMeshProUGUI>().gameObject.activeSelf}");
                    if (!value.GetComponent<TextMeshProUGUI>().enabled)
                        value.GetComponent<TextMeshProUGUI>().enabled = true;
                    value.GetComponent<TextMeshProUGUI>().text = timer.ToString();
                }
            }
        }
        else
        {
            foreach (var value in values)
            {
                if (value.tag == "Timer" && value.GetComponent<TextMeshProUGUI>().enabled)
                    value.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
        

       
    }
    [ClientRpc]
    public void UpdateInvClientRpc(PlayerNetworkController playerController, CardAttributes card, Transform playerInventory)
    {
        FindObjectOfType<GameManagerScript>().DetectInventory(playerController, playerInventory, card);
    }
    [ClientRpc]
    public void UpdateDiscardClientRpc(CardAttributes card)
    {
        FindObjectOfType<GameManagerScript>().IdentifyCardInDiscard(card);
    }
    // [ClientRpc]
    // public void UpdateCountCardPlayerClientRpc(CardAttributes cards)
    // {
    //     var players = GameObject.FindGameObjectsWithTag("Player");
    //     foreach (var player in players)
    //         foreach (var hand in FindObjectOfType<ServerManager>().Hands)
    //             if (hand.Id == player.GetComponent<NetworkIdentity>().netId)
    //                 foreach (var textMesh in player.GetComponentsInChildren<TextMeshProUGUI>())
    //                     if (textMesh.tag != "Timer") textMesh.text = hand.Cards.Count.ToString();
    // }


    public void setPlayerPosition(Vector2 pos)
    {
        transform.SetParent(GameObject.Find("Players").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = pos;
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateInventory(CardAttributes card, PlayerNetworkController playerController, Transform playerInventory)
    {
        FindObjectOfType<ServerManager>().UpdateInventory(card, playerController, playerInventory);
    }
    [Command(requiresAuthority = false)]
    public void CmdGiveCardToDiscard(CardAttributes card, uint id, uint target)
    {
        // StartCoroutine(FindObjectOfType<ServerManager>().DeleteCard(card, id));
        FindObjectOfType<ServerManager>().GiveCardToDiscard(card, id, target);
    }
    [ClientRpc]
    public void GetActionClientRpc(string action)
    {
        switch (action)
        {
            case "Bang":
                {

                    break;
                }
            case "Beer":
                {

                    break;
                }
        }
    }

    [ClientRpc]
    public void AttackClientRpc(string action)
    {
        if(action == "Bang")
        {

        }
    }


    [ClientRpc]
    public void EndTurnClientRpc()
    {
        CardScript[] check = FindObjectsOfType<CardScript>();
        foreach (var item in check)
        {
            item.EndTurn();
        }
    }
    // [Command(requiresAuthority = false)]
    // public void CmdRemoveCard(CardAttributes card, uint id)
    // {
    //     FindObjectOfType<ServerManager>().RemoveCard(card, id);   
    // }



}
