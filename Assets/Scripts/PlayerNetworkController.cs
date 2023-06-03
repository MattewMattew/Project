using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using static UnityEditor.Progress;
// using UnityEditor.Localization.Plugins.XLIFF.V20;

public class PlayerNetworkController : NetworkBehaviour
{
    public List<Material> Materials;
    Image materialHP;

    // Start is called before the first frame update
    void Start()
    {
        var materialComponents = GetComponentsInChildren<Image>();
        foreach (var item in materialComponents)
        {
            if(item.gameObject.tag == "HpBar")
            {
                item.material = Materials[(int)netId - 1];
                item.material.SetFloat("_RemovedS", 9f - 4f);
                materialHP = item;
            }
        }
    }
    [ClientRpc]
    public void HealthUpdateClientRpc(uint id, int health)
    {
        if(netId == id)
            materialHP.material.SetFloat("_RemovedS", 9f - health);
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
    public void CmdGiveCardToDiscard(CardAttributes card/*, uint id, uint target*/)
    {
        // StartCoroutine(FindObjectOfType<ServerManager>().DeleteCard(card, id));
        FindObjectOfType<ServerManager>().GiveCardToDiscard(card/*, id, target*/);
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

    [Command(requiresAuthority = false)]
    public void CmdDefense()
    {
        FindObjectOfType<ServerManager>().GiveTurn(FindObjectOfType<ServerManager>().turnPlayerId, false);
    }

    [Command(requiresAuthority =false)]
    public void CmdAttack(string action)
    {
        if(action == "Bang")
        {
            FindObjectOfType<ServerManager>().AttackAction(this);
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
}
