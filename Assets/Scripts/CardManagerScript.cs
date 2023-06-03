using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card 
{
    public string Name, Dignity/*, TextInfo*/;
    public Sprite Logo, Suit;
    public Card(string name, /*string textInfo*/ string logoPath, string dignity, string suitPath)
    {
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
public class CardManagerScript : MonoBehaviour // Список карт и свойства
{
    public void Awake() 
    {
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Barrel", "Q", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Beer", "6", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Carbine", "A", "Clubs"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Diligence", "9", "Spades"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Duel", "8", "Clubs"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Dynamite", "2", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Gatling", "10", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("General", "9", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Indians", "K", "Diamonds"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Jail", "10", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Mustang", "8", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Panic", "8", "Diamonds"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Remington","K", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("Saloon", "5", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Scofield", "J", "Clubs"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Volcanic", "10", "Clubs"));

        // CardDesk.AllServerCards.Add(new CardAttributes("WellsFargo", "3", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Winchester", "8", "Spades"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Women", "9", "Diamonds"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Roach", "A", "Spades"));
    }
}
