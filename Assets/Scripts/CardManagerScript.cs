using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card 
{
    public string Name, Dignity/*, TextInfo*/;
    public Sprite Logo, Suit;
    public Card(string name, /*string textInfo,*/ string logoPath, string dignity, string suitPath)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        // TextInfo = textInfo;
        Suit = Resources.Load<Sprite>(suitPath);
        Dignity = dignity;
    }
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScript : MonoBehaviour
{
    public void Awake() 
    {
        CardManager.AllCards.Add(new Card("Bang","Sprites/Cards/Bang","2","Sprites/Suits/Diamonds"));

        CardManager.AllCards.Add(new Card("Barrel","Sprites/Cards/Barrel","Q","Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Beer","Sprites/Cards/Beer","6","Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Carbin","Sprites/Cards/Carbin","A","Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("Diligence","Sprites/Cards/Diligence","9","Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Duel","Sprites/Cards/Duel","8","Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("Dynamite","Sprites/Cards/Dynamite","2","Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Gatling","Sprites/Cards/Gatling","10","Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("General","Sprites/Cards/General","9","Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Indians","Sprites/Cards/Indians","K","Sprites/Suits/Diamond"));

        CardManager.AllCards.Add(new Card("Jail","Sprites/Cards/Jail","10","Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Missed","Sprites/Cards/Missed","2","Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Mustang","Sprites/Cards/Mustang","8","Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Panic","Sprites/Cards/Panic","8","Sprites/Suits/Diamond"));

        CardManager.AllCards.Add(new Card("Remington","Sprites/Cards/Remington","K","Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("Saloon","Sprites/Cards/Saloon","5","Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Scofield","Sprites/Cards/Scofield","J","Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("Volcanic","Sprites/Cards/Volcanic","10","Sprites/Suits/Clubs"));

        CardManager.AllCards.Add(new Card("WellsFargo","Sprites/Cards/WellsFargo","3","Sprites/Suits/Hearts"));

        CardManager.AllCards.Add(new Card("Winchester","Sprites/Cards/Winchester","8","Sprites/Suits/Spades"));

        CardManager.AllCards.Add(new Card("Women","Sprites/Cards/Women","9","Sprites/Suits/Diamond"));

        CardManager.AllCards.Add(new Card("Roach","Sprites/Cards/Roach","A","Sprites/Suits/Spades"));
    }
}
