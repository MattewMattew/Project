using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card 
{
    public string Name, Dignity/*, TextInfo*/;
    public Sprite Logo, Suit;
    public Card(string name, /*string textInfo*/ string logoPath, string dignity, string suitPath)
    {
        // name - имя
        // logoPath - картинка
        // dignity - достоинство масти
        // suitPath - масть
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        // TextInfo = textInfo;
        Suit = Resources.Load<Sprite>(suitPath);
        Dignity = dignity;
    }
}

public struct CardAttributes
{
    public string Name, Dignity, Suit;
    public CardAttributes(string name, string dignity, string suit)
    {
        Name = name;
        Dignity = dignity;
        Suit = suit;
    }
}

public static class CardDesk
{
    public static List<CardAttributes> AllServerCards = new List<CardAttributes>();
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}



// Diamonds - буби
// Spades - пики
// Hearts - черви
// Clubs - крести
public class CardManagerScript : MonoBehaviour // Список карт и свойства
{
    public void Awake() 
    {
        CardManager.AllCards.Add(new Card("Bang", "Sprites/Cards/Bang", "2", "Sprites/Suits/Diamonds"));

        CardManager.AllCards.Add(new Card("Barrel", "Sprites/Cards/Barrel", "Q", "Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Beer", "Sprites/Cards/Beer", "6", "Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Carbin", "Sprites/Cards/Carbin", "A", "Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("Diligence", "Sprites/Cards/Diligence", "9", "Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Duel", "Sprites/Cards/Duel", "8", "Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("Dynamite", "Sprites/Cards/Dynamite", "2", "Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Gatling", "Sprites/Cards/Gatling", "10", "Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("General", "Sprites/Cards/General", "9", "Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Indians", "Sprites/Cards/Indians", "K", "Sprites/Suits/Diamonds"));

        CardManager.AllCards.Add(new Card("Jail", "Sprites/Cards/Jail", "10", "Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Missed", "Sprites/Cards/Missed", "2", "Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Mustang", "Sprites/Cards/Mustang", "8", "Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Panic", "Sprites/Cards/Panic", "8", "Sprites/Suits/Diamonds"));

        CardManager.AllCards.Add(new Card("Remington", "Sprites/Cards/Remington", "K", "Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("Saloon", "Sprites/Cards/Saloon", "5", "Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Scofield", "Sprites/Cards/Scofield", "J", "Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("Volcanic", "Sprites/Cards/Volcanic", "10", "Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("WellsFargo", "Sprites/Cards/WellsFargo", "3", "Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Winchester", "Sprites/Cards/Winchester", "8", "Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Women", "Sprites/Cards/Women", "9", "Sprites/Suits/Diamonds"));

        CardManager.AllCards.Add(new Card("Roach", "Sprites/Cards/Roach", "A", "Sprites/Suits/Spades"));




        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));

        CardDesk.AllServerCards.Add(new CardAttributes("Barrel", "Q", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Beer", "6", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Carbin", "A", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("Diligence", "9", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Duel", "8", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("Dynamite", "2", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Gatling", "10", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("General", "9", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Indians", "K", "Diamonds"));

        CardDesk.AllServerCards.Add(new CardAttributes("Jail", "10", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Mustang", "8", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Panic", "8", "Diamonds"));

        CardDesk.AllServerCards.Add(new CardAttributes("Remington","K", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("Saloon", "5", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Scofield", "J", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("Volcanic", "10", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("WellsFargo", "3", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Winchester", "8", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Women", "9", "Diamonds"));

        CardDesk.AllServerCards.Add(new CardAttributes("Roach", "A", "Spades"));
    }
}
