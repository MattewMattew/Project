using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScripts : MonoBehaviour
{
   public CardAttributes SelfCard;
   public TextMeshProUGUI Name, Dignity;
   public Image Logo, Suit,Icon;
   public GameObject Type;
   Sprite LogoSprite;
   public enum TypeCard {DISPOSABLE_CARD, PERMANENT_CARD, WEAPON_CARD}
   public TypeCard InfoTypeCard;
   
   //Test
   public void HideCardInfo (CardAttributes card)
   {
      SelfCard = card;
      
      Name.text = "";
      Logo.sprite = null;
      Suit.sprite = null;
      Dignity.text = "";
      Icon.sprite = null;
      
   }
   public void ShowCardInfo (CardAttributes card)
   {
      SelfCard = card;
      Name.text = card.Name;
      Dignity.text = card.Dignity;
      switch (card.Name)
      {
      /*                                                       22 cards                                                                    */

      /* _________________________________________________ DISPOSABLE CARD _______________________________________________________________ */
         case "Bang":
         {
            // Name.text = "Выстрел";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Bang");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(БЭНГ!)");
            break;
         }
         case "Missed":
         {
            // Name.text = "Промах";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Missed");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Промах)");
            break;
         }
         case "Beer":
         {
            // Name.text = "Пиво";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Beer");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Пиво)");
            break;
         }
         case "Panic":
         {
            // Name.text = "Воровство";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Panic");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Паника)");
            break;
         }
         case "Gatling":
         {
            // Name.text = "Турель";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Gatling");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Гатлинг)");
            break;
         }
         case "WellsFargo":
         {
            // Name.text = "Контробанда";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/WellsFargo");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Уэлс Фарго)");
            break;
         }
         case "Diligence":
         {
            // Name.text = "Контейнер";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Diligence");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Дилижанс)");
            break;
         }
         case "General":
         {
            // Name.text = "Снабжение";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/General");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Уэлс Фарго)");
            break;
         }
         case "Duel":
         {
            // Name.text = "Дуэль";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Duel");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Saloon":
         {
            // Name.text = "Бар";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Saloon");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Салун)");
            break;
         }
         case "Women":
         {
            // Name.text = "Устрашение";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Women");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Плутовка Кэт)");
            break;
         }
         case "Indians":
         {
            // Name.text = "Заключенные";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Indians");
            Type.GetComponent<Image>().color = Color.red;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Гатлинг)");
            break;
         }

/* _________________________________________________ PERMANENT CARD ________________________________________________________________ */

         case "Barrel":
         {
            // Name.text = "Укрытие";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Barrel");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Dynamite":
         {
            // Name.text = "Граната";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Dynamite");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Jail":
         {
            // Name.text = "Карцер";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Jail");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Mustang":
         {
            // Name.text = "Джетпак";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Mustang");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }
         case "Roach":
         {
            // Name.text = "Прицел";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Roach");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.PERMANENT_CARD;
            break;
         }

/* _________________________________________________ WEAPON CARD ___________________________________________________________________ */

      case "Carbine":
         {
            // Name.text = "Blast-1";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Carbine");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.WEAPON_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Карабин)");
            break;
         }
         case "Remington":
         {
            // Name.text = "SL-9";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Remington");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.WEAPON_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Ремингтон)");
            break;
         }
         case "Scofield":
         {
            // Name.text = "GL-16";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Scofield");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.WEAPON_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Скофилд)");
            break;
         }
         case "Volcanic":
         {
            // Name.text = "PP-90G1";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Volcanic");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.WEAPON_CARD;
            break;
         }
         case "Winchester":
         {
            // Name.text = "SSJ-69";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Winchester");
            Type.GetComponent<Image>().color = Color.blue;
            InfoTypeCard = TypeCard.WEAPON_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Винчестер)");
            break;
         }          
      }

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
   }
}
