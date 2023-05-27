using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScripts : MonoBehaviour
{
    public CardAttributes SelfCard;
    public TextMeshProUGUI Name, Dignity;
    public Image Logo, Suit;
    Sprite LogoSprite;

    public void HideCardInfo (CardAttributes card)
    {
        SelfCard = card;
        
        Name.text = "";
        Logo.sprite = null;
        Suit.sprite = null;
        Dignity.text = "";
    }
    public void ShowCardInfo (CardAttributes card)
    {
        SelfCard = card;

        Name.text = card.Name;
        switch (card.Name)
        {
            case "Bang":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Bang");
               break;
            }
            case "Barrel":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Barrel");
               break;
            }
            case "Beer":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Beer");
               break;
            }
            case "Carbine":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Carbine");
               break;
            }
            case "Diligence":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Diligence");
               break;
            }
            case "Duel":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Duel");
               break;
            }
            case "Dynamite":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Dynamite");
               break;
            }
            case "Gatling":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Gatling");
               break;
            }
            case "General":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/General");
               break;
            }
            case "Indians":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Indians");
               break;
            }
            case "Jail":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Jail");
               break;
            }
            case "Missed":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Missed");
               break;
            }
            case "Mustang":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Mustang");
               break;
            }
            case "Panic":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Panic");
               break;
            }
            case "Remington":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Remington");
               break;
            }
            case "Saloon":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Saloon");
               break;
            }
            case "Scofield":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Scofield");
               break;
            }
            case "Volcanic":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Volcanic");
               break;
            }
            case "WellsFargo":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/WellsFargo");
               break;
            }
            case "Winchester":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Winchester");
               break;
            }
            case "Women":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Women");
               break;
            }
            case "Roach":
            {
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Roach");
               break;
            }


        
            
                

        }
/*      Logo.sprite = card.Logo;
        Logo.preserveAspect = true;*/
        
        switch (card.Suit)
        {
            case "Clubs":
            {
               Suit.sprite = Resources.Load<Sprite>("Sprites/Suits/Clubs");
               break;
            }
            case "Hearts":
            {
               Suit.sprite = Resources.Load<Sprite>("Sprites/Suits/Hearts");
               break;
            }
            case "Diamonds":
            {
               Suit.sprite = Resources.Load<Sprite>("Sprites/Suits/Diamonds");
               break;
            }
            case "Spades":
            {
               Suit.sprite = Resources.Load<Sprite>("Sprites/Suits/Spades");
               break;
            }
        }

/*       Suit.sprite = card.Suit;
        Suit.preserveAspect = true*/;
        Dignity.text = card.Dignity;
    }

    private void Start()
    {
        // ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}
