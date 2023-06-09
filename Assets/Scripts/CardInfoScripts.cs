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
   public GameObject Type, Info;
   public int WeaponRange;
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
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(БЭНГ!)");
            
            break;
         }
         case "Missed":
         {
            // Name.text = "Промах";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Missed");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Промах)");
            break;
         }
         case "Beer":
         {
            // Name.text = "Пиво";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Beer");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Пиво)");
            break;
         }
         case "Panic":
         {
            // Name.text = "Воровство";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Panic");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Паника)");
            break;
         }
         case "Gatling":
         {
            // Name.text = "Турель";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Gatling");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Гатлинг)");
            break;
         }
         case "WellsFargo":
         {
            // Name.text = "Контробанда";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/WellsFargo");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Уэлс Фарго)");
            break;
         }
         case "Diligence":
         {
            // Name.text = "Контейнер";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Diligence");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Дилижанс)");
            break;
         }
         case "General":
         {
            // Name.text = "Снабжение";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/General");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Уэлс Фарго)");
            break;
         }
         case "Duel":
         {
            // Name.text = "Дуэль";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Duel");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            break;
         }
         case "Saloon":
         {
            // Name.text = "Бар";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Saloon");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Салун)");
            break;
         }
         case "Women":
         {
            // Name.text = "Устрашение";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Women");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Плутовка Кэт)");
            break;
         }
         case "Indians":
         {
            // Name.text = "Заключенные";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Indians");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Гатлинг)");
            break;
         }

/* _________________________________________________ PERMANENT CARD ________________________________________________________________ */

         case "Barrel":
         {
            if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Box") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "Укрытие";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Barrel");
               
               InfoTypeCard = TypeCard.PERMANENT_CARD;}
            break;
         }
         case "Dynamite":
         {
            if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Bomb") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "Граната";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Dynamite");
               
               InfoTypeCard = TypeCard.PERMANENT_CARD;}
            break;
         }
         case "Jail":
         {  
            if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Jail") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "Карцер";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Jail");
               
               InfoTypeCard = TypeCard.PERMANENT_CARD;}
            break;
         }
         case "Mustang":
         {
             if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Jetpack") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "Джетпак";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Mustang");
               
               InfoTypeCard = TypeCard.PERMANENT_CARD;}
            break;
         }
         case "Roach":
         {
             if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Scope") ;
               Info.SetActive(false);
            }
            else
            {
               // Name.text = "Прицел";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Roach");
               InfoTypeCard = TypeCard.PERMANENT_CARD;
            }
            break;
         }

/* _________________________________________________ WEAPON CARD ___________________________________________________________________ */

      case "Carbine":
         {
            InfoTypeCard = TypeCard.WEAPON_CARD;
            WeaponRange = 4;
            if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Gun") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "Blast-1";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Carbine");
               Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Карабин)");
            }
            break;
         }
         case "Remington":
         {
            InfoTypeCard = TypeCard.WEAPON_CARD;
            WeaponRange = 3;
            if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Gun") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "SL-9";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Remington");
               Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Ремингтон)");
            }
            break;
         }
         case "Scofield":
         {
            InfoTypeCard = TypeCard.WEAPON_CARD;
            WeaponRange = 2;
            if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Gun") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "GL-16";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Scofield");
               Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Скофилд)");
            }
            break;
         }
         case "Volcanic":
         {
            InfoTypeCard = TypeCard.WEAPON_CARD;
            WeaponRange = 1;
            if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Gun") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "PP-90G1";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Volcanic");
            }
            break;
         }
         case "Winchester":
         {
            InfoTypeCard = TypeCard.WEAPON_CARD;
            WeaponRange = 5;
            if (transform.parent.tag =="Field")
            {  
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/CardIcons/Gun") ;
               Info.SetActive(false);
            }
            else 
            {
               // Name.text = "SSJ-69";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Winchester");
               Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Винчестер)");
            }
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
