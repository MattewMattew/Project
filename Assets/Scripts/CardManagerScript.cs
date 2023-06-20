using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject menu;
    private Button exitButton;
    public void Awake() 
    {
        /*                                                       80 cards                                                                    */

        /* _________________________________________________ DISPOSABLE CARD _______________________________________________________________ */

        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "3", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "4", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "5", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "6", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "7", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "8", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "9", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "10", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "J", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "Q", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "K", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "A", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "Q", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "K", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "A", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "2", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "3", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "4", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "5", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "6", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "7", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "8", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "9", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Bang", "A", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "2", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "3", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "4", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "5", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "6", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "7", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "8", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "10", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "J", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "Q", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "K", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Missed", "A", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("Beer", "6", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Beer", "7", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Beer", "8", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Beer", "9", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Beer", "10", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Beer", "J", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Panic", "J", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Panic", "Q", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Panic", "A", "Hearts"));
        CardDesk.AllServerCards.Add(new CardAttributes("Panic", "8", "Diamonds"));

        CardDesk.AllServerCards.Add(new CardAttributes("Gatling", "10", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("WellsFargo", "3", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Diligence", "9", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Diligence", "Q", "Spades"));

        // CardDesk.AllServerCards.Add(new CardAttributes("General", "9", "Clubs"));
        // CardDesk.AllServerCards.Add(new CardAttributes("General", "Q", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Duel", "8", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Duel", "J", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Duel", "Q", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Saloon", "5", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Women", "9", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Women", "10", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Women", "J", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Women", "K", "Hearts"));

        CardDesk.AllServerCards.Add(new CardAttributes("Indians", "K", "Diamonds"));
        CardDesk.AllServerCards.Add(new CardAttributes("Indians", "A", "Diamonds"));


        /* _________________________________________________ PERMANENT CARD ________________________________________________________________ */

        CardDesk.AllServerCards.Add(new CardAttributes("Barrel", "Q", "Spades"));
        CardDesk.AllServerCards.Add(new CardAttributes("Barrel", "K", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Dynamite", "2", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Jail", "10", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Jail", "J", "Spades"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Jail", "4", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Mustang", "8", "Hearts"));
        // CardDesk.AllServerCards.Add(new CardAttributes("Mustang", "9", "Hearts"));

        // CardDesk.AllServerCards.Add(new CardAttributes("Roach", "A", "Spades"));


        /* _________________________________________________ WEAPON CARD ___________________________________________________________________ */

        CardDesk.AllServerCards.Add(new CardAttributes("Carbine", "A", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("Remington","K", "Clubs"));

        CardDesk.AllServerCards.Add(new CardAttributes("Scofield", "J", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Scofield", "Q", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Scofield", "K", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Volcanic", "10", "Clubs"));
        CardDesk.AllServerCards.Add(new CardAttributes("Volcanic", "10", "Spades"));

        CardDesk.AllServerCards.Add(new CardAttributes("Winchester", "8", "Spades"));

        exitButton = GameObject.Find("Exit").GetComponent<Button>();
        exitButton.onClick.AddListener(Exit);
    }
    void Exit()
    {
        print("sd");
        if (menu.active == false && !FindObjectOfType<ServerManager>())
        {
            SceneManager.LoadScene(0);
        }
    }
/*    private void Update()
    {
        if(!FindObjectOfType<ServerManager>())
        {
            print("done");
            mainMenu.SetActive(true);
        }
        else if(mainMenu.active == true)
        {
            mainMenu.SetActive(false);
        }
    }*/
}
