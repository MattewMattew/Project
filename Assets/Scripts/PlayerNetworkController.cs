using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
// using static UnityEditor.Progress;
// using UnityEditor.Localization.Plugins.XLIFF.V20;

public class PlayerNetworkController : NetworkBehaviour
{
    public List<Material> Materials;
    Image materialHP;
    public int Range;

    public int maxHealth = 0;

    public ServerManager.Roles Role;
    void Start()
    {
        
    }
    [ClientRpc]
    public void GiveRole(uint id, ServerManager.Roles role) 
    {
        if(netId == id) Role = role;
        // print($"{netId} player have {role} role");
        GiveHealthInit();
    }
    void GiveHealthInit()
    {
        var materialComponents = GetComponentsInChildren<Image>();
        foreach (var item in materialComponents)
        {
            if (item.gameObject.tag == "HpBar")
            {
                item.material = Materials[(int)netId - 1];
                if (Role == ServerManager.Roles.CAPTAIN)
                {
                    item.material.SetFloat("_RemovedS", 9f - 5f);
                    maxHealth = 5;
                }
                else
                {
                    item.material.SetFloat("_RemovedS", 9f - 4f);
                    maxHealth = 4;
                }
                materialHP = item;
            }
        }
    }
    [ClientRpc]
    public void DeathActionClientRpc(uint id)
    {
        if(netId == id)
        {
            Destroy(gameObject);
        }
    }

    [ClientRpc]
    public void HealthUpdateClientRpc(uint id, int health)
    {
        if(netId == id)
            materialHP.material.SetFloat("_RemovedS", 9f - health);
    }

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
    public void RemoveCardFromInventoryClientRpc(uint id, CardAttributes card)
    {
        FindObjectOfType<GameManagerScript>().RemoveCardFromInventory(id, card);
    }

    [ClientRpc]
    public void UpdateInvClientRpc(PlayerNetworkController playerController, CardAttributes card, Transform playerInventory)
    {
        FindObjectOfType<GameManagerScript>().DetectInventory(playerController, playerInventory, card);
    }
    [ClientRpc]
    public void UpdateCountCardsClientRpc(int countCards, uint id)
    {
        FindObjectOfType<GameManagerScript>().UpdateCountCards(countCards, id);
    }

    [ClientRpc]
    public void UpdateDiscardClientRpc(CardAttributes card)
    {
        FindObjectOfType<GameManagerScript>().IdentifyCardInDiscard(card);
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

    [ClientRpc]
    public void RemoveCardFromHandClientRpc(CardAttributes card)
    {
        if (isLocalPlayer)
            FindObjectOfType<GameManagerScript>().RemoveCardFromHand(card);
    }

    [ClientRpc]
    public void GiveHandCardsClientRpc(uint id, CardAttributes card)
    {
        print($"{id} {card.Name}");
        FindObjectOfType<GameManagerScript>().GiveHandCards(id, card);
    }

    [ClientRpc]
    public void ButtonActivationClientRpc(uint id)
    {   
        FindObjectOfType<GameManagerScript>().ButtonActivation(id);

    }







     public void setPlayerPosition(int range ,Vector2 pos)
    {
        transform.SetParent(GameObject.Find("Players").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = pos;
        Range = range;
        foreach (var item in GameObject.FindGameObjectsWithTag("Range"))
        {
            if (item.GetComponentInParent<PlayerNetworkController>().netId == netId)
                item.GetComponent<TextMeshProUGUI>().text = range.ToString();
        }
    }








    [Command(requiresAuthority = false)]
    public void CmdPanicAction(CardAttributes card, uint id)
    {
        FindObjectOfType<ServerManager>().PanicAction(card, id);
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

    [Command(requiresAuthority = false)]
    public void CmdRemoveCardFromInventory(CardAttributes card, uint id)
    {
        FindObjectOfType<ServerManager>().RemoveCardFromInventory(card, id);
    }

    [Command(requiresAuthority = false)]
    public void CmdDefense()
    {
        FindObjectOfType<ServerManager>().GiveTurn(FindObjectOfType<ServerManager>().turnPlayerId, false);
    }
    [Command(requiresAuthority = false)]
    public void CmdDuel(uint idAttacking, uint idDefenser)
    {
        FindObjectOfType<ServerManager>().DuelAction(idAttacking, idDefenser);
    }
    [Command(requiresAuthority =false)]
    public void CmdAttack(string card)
    {
        FindObjectOfType<ServerManager>().AttackAction(this.netId, card);
    }

    [Command(requiresAuthority = false)]
    public void CmdRegenerationHealth(uint id, CardAttributes card)
    {
        FindObjectOfType<ServerManager>().RegenerationHealth(id, card);
    }

    [Command(requiresAuthority = false)]
    public void CmdGiveHandCards(uint id, int cardsCount)
    {
        FindObjectOfType<ServerManager>().GiveHandCards(FindObjectOfType<ServerManager>().PackCards, id, cardsCount);
    }

    [Command(requiresAuthority =false)]
    public void CmdRemoveCardFromHand(uint id, CardAttributes card)
    {
        FindObjectOfType<ServerManager>().RemoveCardFromHand(card, id);
    }
    [Command(requiresAuthority =false)]
    public void CmdRandomRemoveCardFromHand(PlayerNetworkController player, CardAttributes card)
    {
        FindObjectOfType<ServerManager>().RandomRemoveCardFromHand(player, card);
    }

    [Command(requiresAuthority = false)]
    public void CmdMassiveAttackAction(string card)
    {
        StartCoroutine(FindObjectOfType<ServerManager>().MassiveAttackAction(card));
    }
}