using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScripts : MonoBehaviour
{
   public CardAttributes SelfCard;
   public TextMeshProUGUI NameCard, Dignity ,InfoCard;
   public Image Logo, Suit,Icon;
   public GameObject Type, Info;
   public int WeaponRange;
   public string Name;
   Sprite LogoSprite;
   public enum TypeCard {DISPOSABLE_CARD, PERMANENT_CARD, WEAPON_CARD}
   public TypeCard InfoTypeCard;
   
   //Test
   // public void HideCardInfo (CardAttributes card)
   // {
   //    SelfCard = card;
      
   //    Name = "";
   //    NameCard.text = "";
   //    Logo.sprite = null;
   //    Suit.sprite = null;
   //    Dignity.text = "";
   //    Icon.sprite = null;
      
   // }
   public void ShowCardInfo (CardAttributes card)
   {
      SelfCard = card;
      Name = card.Name;
      Dignity.text = card.Dignity;
      switch (card.Name)
      {
      /*                                                       22 cards                                                                    */

      /* _________________________________________________ DISPOSABLE CARD _______________________________________________________________ */
         case "Bang":
         {  
            
            NameCard.text = "Выстрел";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Bang");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(БЭНГ!)");
            InfoCard.text = "Основной способ лишить соперника единиц здоровья.";
            
            break;
         }
         case "Missed":
         {
            NameCard.text = "Промах";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Missed");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Промах)");
            InfoCard.text = "Если против вас сыграна карта «Бэнг!», вы можететут же отменить попадание, сыграв карту «Мимо!»";
            break;
         }
         case "Beer":
         {
            NameCard.text = "Пиво";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Beer");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Пиво)");
            InfoCard.text = "Карта восстанавливает единицу здоровья";
            break;
         }
         case "Panic":
         {
            NameCard.text = "Воровство";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Panic");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Паника)");
            InfoCard.text="Заберите себе на руку карту у игрока на расстоянии 1 ";
            break;
         }
         case "Gatling":
         {
            NameCard.text = "Турель";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Gatling");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Гатлинг)");
            InfoCard.text="Каждый другой игрок, независимо от расстояния, получает одно попадание";
            break;
         }
         case "WellsFargo":
         {
            NameCard.text = "Контробанда";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/WellsFargo");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Уэлс Фарго)");
            InfoCard.text="Возьмите наруку 3 карты из колоды.";
            break;
         }
         case "Diligence":
         {
            NameCard.text = "Контейнер";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Diligence");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Дилижанс)");
            InfoCard.text="Возьмите на руку 2 карты из колоды";
            break;
         }
         case "General":
         {
            NameCard.text = "Снабжение";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/General");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Book");
            InfoCard.text="Раскройте из колоды столько карт, сколько игроков на данный момент осталось в игре.";
            break;
         }
         case "Duel":
         {
            NameCard.text = "Дуэль";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Duel");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            InfoCard.text="Вызовите любого игрока — глаза в глаза — на поединок, независимо от расстояния";
            break;
         }
         case "Saloon":
         {
            NameCard.text = "Бар";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Saloon");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Салун)");
            InfoCard.text="Каждый игрок восстанавливает по единице здоровья";
            break;
         }
         case "Women":
         {
            NameCard.text = "Устрашение";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Women");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Плутовка Кэт)");
            InfoCard.text="Заставьте любого игрока, независимо от расстояния, сбросить карту";
            break;
         }
         case "Indians":
         {
            NameCard.text = "Заключенные";
            Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Indians");
            Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKartaRED") ;
            InfoTypeCard = TypeCard.DISPOSABLE_CARD;
            Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Гатлинг)");
            InfoCard.text="Каждый другой игрок должен либо сбросить с руки карту «Бэнг!», бо потерять единицу здоровья";
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
               NameCard.text = "Укрытие";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Barrel");
               InfoCard.text="Всякий раз, когда в вас попадают, можете сделать проверку";
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
               NameCard.text = "Граната";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Dynamite");
               InfoCard.text="Игроки передают карту по кругу, пока «Динамит» не взорвётся";
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
               NameCard.text = "Карцер";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Jail");
               InfoCard.text="Сыграйте эту карту на любого игрока, независимо от расстояния(«Тюрьму» нельзя сыграть на шерифа!)";
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
               NameCard.text = "Джетпак";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Mustang");
               InfoCard.text="Пока эта карта у вас в игре, для всех остальных игроков расстояние до вас увеличивается на 1";
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
               NameCard.text = "Прицел";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Roach");
               InfoCard.text="Пока эта карта у вас в игре, для вас расстояние до всех остальных игроков уменьшается на 1";
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
               NameCard.text = "Blast-1";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Carbine");
               Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Карабин)");
               InfoCard.text="Число в прицеле, которое обозначает максимальную дистанцию атаки";
               
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
               NameCard.text = "SL-9";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Remington");
               Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Ремингтон)");
               InfoCard.text="Число в прицеле, которое обозначает максимальную дистанцию атаки";
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
               NameCard.text = "GL-16";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Scofield");
               Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Скофилд)");
               InfoCard.text="Число в прицеле, которое обозначает максимальную дистанцию атаки";
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
               NameCard.text = "PP-90G1";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Volcanic");
               InfoCard.text="Позволяет за один ход сыграть сколько угодно карт «Бэнг!»";
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
               NameCard.text = "SSJ-69";
               Info.SetActive(true);
               Type.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/NeonKarta") ;
               Logo.sprite = Resources.Load<Sprite>("Sprites/Cards/Winchester");
               Icon.sprite=Resources.Load<Sprite>("Sprites/Icon/Иконки(Винчестер)");
               InfoCard.text="Число в прицеле, которое обозначает максимальную дистанцию атаки";
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
