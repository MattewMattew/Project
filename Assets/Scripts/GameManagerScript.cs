using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class GameManagerScript : MonoBehaviour // Колода
{
    public bool EndTurn;
    public Transform SelfHand, EnemyHand;
    public GameObject CardPref;
    int Move, MoveTime = 30;
    public TextMeshProUGUI MoveTimeTxt;
    public Button EndMoveBtn;
    public List<Vector2> spawnPoint = new List<Vector2>() { new Vector2(0, -237), new Vector2(-696, -77), new Vector2(0, 421)};
    public List<CardInfoScripts> PlayerHandCards = new List<CardInfoScripts>(),
                                 PlayerFieldCards = new List<CardInfoScripts>(),
                                 EnemyHandCards = new List<CardInfoScripts>(),
                                 EnemyFieldCards = new List<CardInfoScripts>();

    public bool IsPlayerMove
    {
        get
        {
        return Move % 2 == 0;
        } 
            
    }
    void Update()
    {

    }
    void Awake()
    {

    }
    void Start()
    {
        
    }
    
/*    void GiveHandCards(List<Card> pack, Transform hand) // Количество карт в руке
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(pack, hand);
    }

    void GiveCardToHand(List<Card> pack, Transform hand) // Выдача карты в руку
    {
        if (pack.Count == 0)
            return;

        Card card = CardVars[0];

        GameObject cardGo = Instantiate(CardPref, hand, false);
        NetworkServer.Spawn(cardGo);

        if (hand == EnemyHand)
        {
            cardGo.GetComponent<CardInfoScripts>().HideCardInfo(card);
            EnemyHandCards.Add(cardGo.GetComponent<CardInfoScripts>());
        }
        else
        {
            cardGo.GetComponent<CardInfoScripts>().ShowCardInfo(card);
            PlayerHandCards.Add(cardGo.GetComponent<CardInfoScripts>());
        }
        
        CardVars.RemoveAt(0);     

    }

    IEnumerator MoveFunc ()
    {
        MoveTime = 1;
        *//*MoveTimeTxt.text = MoveTime.ToString();*//*

        while (MoveTime-- > 0)
        {
            *//*MoveTimeTxt.text = MoveTime.ToString();*//*
            yield return new WaitForSeconds(1); //Ожидание секунда  
        }
        *//*ChangeMove();*//*
    }

    public void ChangeMove() 
    {
        StopAllCoroutines();
        Move++;

        EndMoveBtn.interactable = IsPlayerMove;

        if (IsPlayerMove)
            GiveNewCards();
        
        StartCoroutine(MoveFunc());
    }

    void GiveNewCards()
    {
        int i=0;
        while(i++ < 2)
            GiveCardToHand(CardVars, SelfHand);
    }*/

}
