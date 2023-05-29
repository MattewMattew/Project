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
   public GameObject Type;
   Sprite LogoSprite;
   public enum TypeCard {DISPOSABLE_CARD, PERMANENT_CARD}
   public TypeCard InfoTypeCard;
   
   
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
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Barrel":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Barrel");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Beer":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Beer");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Carbine":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Carbine");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Diligence":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Diligence");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Duel":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Duel");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Dynamite":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Dynamite");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Gatling":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Gatling");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "General":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/General");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Indians":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Indians");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Jail":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Jail");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Missed":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Missed");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Mustang":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Mustang");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Panic":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Panic");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Remington":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Remington");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Saloon":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Saloon");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Scofield":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Scofield");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Volcanic":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Volcanic");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "WellsFargo":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/WellsFargo");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Winchester":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Winchester");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Women":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Women");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Roach":
         {
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Roach");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
      
          
              
      }
/*    Logo.sprite = card.Logo;
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
/*     Suit.sprite = card.Suit;
      Suit.preserveAspect = true*/;
      Dignity.text = card.Dignity;
   }
}
