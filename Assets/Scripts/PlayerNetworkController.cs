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
    private GameObject AnimIndians;
    private Coroutine coroutine;
    private GameObject Anim;
    public List<Material> Materials;
    Image materialHP;
    public int Range;
    public GameObject RoleInf;
    public Image RoleInfo;

    public int maxHealth = 0;

    public ServerManager.Roles Role;
    void Start()
    {  
        Anim = GetComponentInChildren<Animator>().gameObject;
        
    }
    public void SetName()
    {
        if (isLocalPlayer)
        {
            foreach (var item in GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (item.tag == "UserName")
                {
                    item.text = FindObjectOfType<ButtonsNetworkManager>().DisplayName;
                    CmdSendNameToServer(FindObjectOfType<ButtonsNetworkManager>().DisplayName);
                    break;
                }

            }

        }
    }
    [Command(requiresAuthority =false)]
    public void CmdSendNameToServer(string name)
    {
        FindObjectOfType<ServerManager>().SendNameToClient(netId, name);
    }
    [ClientRpc]
    public void SetNameClientRpc(uint id, string name)
    {
        if(netId == id)
        {
            foreach (var item in GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (item.tag == "UserName")
                {
                    item.text = name;
                    break;
                }

            }
        }
    }

    [ClientRpc]
    public void GiveRole(uint id, ServerManager.Roles role) 
    {
        if(netId == id) Role = role;
        // print($"{netId} player have {role} role");
        GiveHealthInit();
        if(role == ServerManager.Roles.CAPTAIN)
        {
            RoleInfo.gameObject.SetActive(true);
            RoleInfo.sprite=Resources.Load<Sprite>("Sprites/Role/Роль(Капитан)");
        }
        else{
            RoleInfo.gameObject.SetActive(false);
        }
        if (isLocalPlayer && netId == id)
        {
            if (role == ServerManager.Roles.SINDICATE)
            {
                RoleInfo.gameObject.SetActive(true); 
                RoleInfo.sprite=Resources.Load<Sprite>("Sprites/Role/Роль(Бандит)");   
            }
            else if (role == ServerManager.Roles.HELPER)
            {
                RoleInfo.gameObject.SetActive(true);
                RoleInfo.sprite=Resources.Load<Sprite>("Sprites/Role/Роль(Помощник)");
            }
            else if (role == ServerManager.Roles.RENEGADE)
            {
                RoleInfo.gameObject.SetActive(true);
                RoleInfo.sprite=Resources.Load<Sprite>("Sprites/Role/Роль(Ренегат)");
            }
        }
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
        AnimIndians = GameObject.Find("AnimIndians");
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

    [ClientRpc] 
    public void AnimAction(CardAttributes card)
    {
        if(FindObjectOfType<ServerManager>().turnModificator != "Discarding") 
        {
            AnimIndians.GetComponent<Animator>().SetBool("Gatling", false);
            AnimIndians.GetComponent<Animator>().SetBool("Indians", false);
            AnimIndians.GetComponent<Animator>().SetBool("Saloon", false);
            Anim.GetComponent<Animator>().SetBool("Bang", false);
            Anim.GetComponent<Animator>().SetBool("Duel", false);
            Anim.GetComponent<Animator>().SetBool("Diligence", false);
            Anim.GetComponent<Animator>().SetBool("WellsFargo", false);
            Anim.GetComponent<Animator>().SetBool("Jail", false);
            Anim.GetComponent<Animator>().SetBool("Panic", false);
            Anim.GetComponent<Animator>().SetBool("Women", false);
            Anim.GetComponent<Animator>().SetBool("Beer", false);
            Anim.GetComponent<Animator>().SetBool("Missed", false);
            Anim.GetComponent<Animator>().SetBool("Barrel", false);
            Anim.GetComponent<Animator>().SetBool("Roach", false);
            Anim.GetComponent<Animator>().SetBool("Dynamite", false);
            Anim.GetComponent<Animator>().SetBool("Mustang", false);
            Anim.GetComponent<Animator>().SetBool("Remington", false);
            Anim.GetComponent<Animator>().SetBool("Winchester", false);
            Anim.GetComponent<Animator>().SetBool("Carbine", false);
            Anim.GetComponent<Animator>().SetBool("Volcanic", false);
            Anim.GetComponent<Animator>().SetBool("Scofield", false);
            if (coroutine != null) StopCoroutine(coroutine);
            print ($"{card.Name} card");
            if (netId == FindObjectOfType<ServerManager>().turnPlayerId || netId == FindObjectOfType<ServerManager>().attackedPlayerId && card.Name != "Saloon")
            {
                if(card.Name=="Saloon" || card.Name=="Indians" || card.Name== "Gatling")
                {
                    AnimIndians.GetComponent<Animator>().SetBool(card.Name, true);
                    coroutine = StartCoroutine(StartAnim(card.Name, AnimIndians.GetComponent<Animator>()));
                }
                else 
                {
                   if(netId == FindObjectOfType<ServerManager>().attackedPlayerId && FindObjectOfType<ServerManager>().turnModificator == "Duel" || 
                   FindObjectOfType<ServerManager>().turnModificator == "Gatling" || FindObjectOfType<ServerManager>().turnModificator == "Indians")
                   {

                    Anim.GetComponent<Animator>().SetBool(card.Name, true);
                    coroutine = StartCoroutine(StartAnim(card.Name, Anim.GetComponent<Animator>()));

                   } 
                   if(netId == FindObjectOfType<ServerManager>().attackedPlayerId)
                   {
                    Anim.GetComponent<Animator>().SetBool(card.Name, true);
                    coroutine = StartCoroutine(StartAnim(card.Name, Anim.GetComponent<Animator>()));
                   }
                }

            }
            if(card.Name == "Saloon")
            {
                print ("DontBeer");
                Anim.GetComponent<Animator>().SetBool("Beer", true);
                coroutine = StartCoroutine(StartAnim("Beer", Anim.GetComponent<Animator>()));


            }
        }

    }
    IEnumerator StartAnim(string cardName, Animator animator)
    {
        int Animtime = 3;
        while(Animtime > 0)
        {
            Animtime--;
            yield return new WaitForSeconds(1);
        }
        if (animator.GetComponent<Animator>().GetBool(cardName))
        {
            //Anim.SetActive(false);
            animator.GetComponent<Animator>().SetBool(cardName, false);
        }

    }   
}