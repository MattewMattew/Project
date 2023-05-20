using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> SelfHand, EnemyHand, Pack, SelfField, EnemyField;
    
    public Game()
    {
        Pack = GivePack();

        SelfHand = new List<Card>();
        EnemyHand = new List<Card>();

        SelfField = new List<Card>();
        EnemyField = new List<Card>();
        Debug.Log(Pack);
    }
    List<Card> GivePack()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 22; i++)
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
            Debug.Log(list);
        return list;
    }
    
}

public class GameManagerScript : MonoBehaviour // Колода
{
    public Game CurrentGame;
    public Transform SelfHand, EnemyHand;
    public GameObject CardPref;
    void Start()
    {
        CurrentGame = new Game();

        GiveHandCards(CurrentGame.Pack, SelfHand);
    }
    
    void GiveHandCards(List<Card> pack, Transform hand) // Количество карт в руке
    {
        int i = 0;
        while (i++ < 5)
            GiveCardToHand(pack, hand);
    }

    void GiveCardToHand(List<Card> pack, Transform hand) // Выдача карты в руку
    {
        if (pack.Count == 0)
            return;

        Card card = pack[0];

        GameObject cardGo = Instantiate(CardPref, hand, false);

        if (hand == EnemyHand)
            cardGo.GetComponent<CardInfoScripts>().HideCardInfo(card);
        else
            cardGo.GetComponent<CardInfoScripts>().ShowCardInfo(card);
        
        pack.RemoveAt(0);     

    }

}
