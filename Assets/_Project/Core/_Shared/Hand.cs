/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/15/23
 * 
 * What: Hand.cs
 * 
 * Function: Acts as a hand of cards using a List<Card>
 *
 * TODO: ...
 *
 * Say thank you on the way out!
 * 
 ***********************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Cards;
using Game.UI.Cards;

public class Hand : MonoBehaviour
{
    public List<Card> Cards { get; private set; }
    private List<UICard> _uiCards;

    private void Awake()
    {
        _uiCards = GetComponentsInChildren<UICard>().ToList();
    }
    
    public void AddCards(List<Card> cards)
    {
        Cards.AddRange(cards);

        foreach (var card in cards)
        {
            _uiCards.First(uiCard => uiCard.cardDefinition == card).gameObject.SetActive(true);
        }
    }
    
    public void AddCard(Card card)
    {
        Cards.Add(card);
        _uiCards.First(uiCard => uiCard.cardDefinition == card).gameObject.SetActive(true);
    }
    
    public void Reset()
    {
        foreach (var card in _uiCards.Where(card => card.gameObject.activeSelf))
        {
            card.gameObject.SetActive(false);
        }
    }
}
