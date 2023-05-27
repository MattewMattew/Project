using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScripts : MonoBehaviour
{
    public Card SelfCard;
    public TextMeshProUGUI Name, Dignity;
    public Image Logo, Suit;

    public void HideCardInfo (Card card)
    {
        SelfCard = card;
        
        Name.text = "";
        Logo.sprite = null;
        Suit.sprite = null;
        Dignity.text = "";
    }
    public void ShowCardInfo (Card card)
    {
        SelfCard = card;

        Name.text = card.Name;
/*        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Suit.sprite = card.Suit;
        Suit.preserveAspect = true*/;
        Dignity.text = card.Dignity;
    }

    private void Start()
    {
        // ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}
