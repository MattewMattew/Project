using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> Pack;
    
    public Game()
    {
        Pack = GivePack();
    }
    List<Card> GivePack()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 22; i++)
        {
            int card = Random.Range(0, CardManager.AllCards.Count);
            list.Add(CardManager.AllCards[card]);
            CardManager.AllCards.RemoveAt(card);
        }
        return list;
    }
    
}

public class GameManagerScript : MonoBehaviour // Колода
{
    public Game CurrentGame;
    public Transform SelfHand, EnemyHand;
    public GameObject CardPref;
    int Move, MoveTime = 30;
    public TextMeshProUGUI MoveTimeTxt;
    public Button EndMoveBtn;

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

    void Start()
    {
        CurrentGame = new Game();

        GiveHandCards(CurrentGame.Pack, SelfHand);
        GiveHandCards(CurrentGame.Pack, EnemyHand);
        
        StartCoroutine(MoveFunc());
    }
    
    void GiveHandCards(List<Card> pack, Transform hand) // Количество карт в руке
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(pack, hand);
    }

    void GiveCardToHand(List<Card> pack, Transform hand) // Выдача карты в руку
    {
        if (pack.Count == 0)
            return;

        Card card = pack[0];

        GameObject cardGo = Instantiate(CardPref, hand, false);

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
        
        pack.RemoveAt(0);     

    }

    IEnumerator MoveFunc ()
    {
        MoveTime = 30;
        MoveTimeTxt.text = MoveTime.ToString();

        while (MoveTime-- > 0)
        {
            MoveTimeTxt.text = MoveTime.ToString();
            yield return new WaitForSeconds(1); //Ожидание секунда  
        }
        ChangeMove();
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
            GiveCardToHand(CurrentGame.Pack, SelfHand);
    }

}
