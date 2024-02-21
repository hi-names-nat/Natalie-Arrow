/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/15/23
 * 
 * What: Hand.cs
 * 
 * Function: Acts as a hand of cards using a List<Card>.
 * Manages the UIcards as well as the pure data cards.
 *
 *
 ***********************************************************/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Cards;
using Game.UI.Cards;

public class Hand : MonoBehaviour
{
    /// <summary>
    /// The data-only cards
    /// </summary>
    public List<Card> Cards { get; private set; }
    /// <summary>
    /// A list of all 52 UI Cards
    /// </summary>
    private List<UICard> _uiCards;
    /// <summary>
    /// The currently active UI cards.
    /// </summary>
    private List<UICard> _activeUICards;

    private void Awake()
    {
        _uiCards = GetComponentsInChildren<UICard>(true).ToList();
        _activeUICards = new List<UICard>();
        Cards = new List<Card>();
    }
    
    /// <summary>
    /// Adds the given list of cards to the hand, including UI
    /// </summary>
    /// <param name="cards">The cards to add</param>
    public void AddCards(List<Card> cards)
    {
        Cards.AddRange(cards);

        foreach (var card in cards)
        {
            EnableCardVisual(card);
        }
    }
    
    /// <summary>
    /// Adds a single card to the player's hand.
    /// </summary>
    /// <param name="card">The card to add</param>
    public void AddCard(Card card)
    {
        Cards.Add(card);
        EnableCardVisual(card);
    }

    /// <summary>
    /// Enables the UIcard from the 52 card list.
    /// </summary>
    /// <param name="card">The card to enable</param>
    private void EnableCardVisual(Card card)
    {
        var cardVisual = _uiCards.First(uiCard => uiCard.cardDefinition == card);
        _activeUICards.Add(cardVisual);
        cardVisual.gameObject.SetActive(true);
        cardVisual.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Disables the UICard from the active card list
    /// </summary>
    /// <param name="card">The card to disable</param>
    private void DisableCardVisual(Card card)
    {
        var cardVisual = _uiCards.First(uiCard => uiCard.cardDefinition == card);
        _activeUICards.Remove(cardVisual);
        cardVisual.gameObject.SetActive(false);
    }

    /// <summary>
    /// Function to remove cards that the player didn't hold
    /// </summary>
    /// <returns>The cards that were removed</returns>
    public List<Card> RemoveUnheldCards()
    {
        var uiCards = _activeUICards.Where(card => !card.Held).ToList();

        //Get the cards that weren't held
        var cards = Cards.Where(card => uiCards.Any(uiCard => uiCard.cardDefinition == card)).ToList();
        
        //Remove all unheld cards from the hand
        Cards.RemoveAll(card => cards.Contains(card));

        //Disable the unheld card's GUI
        cards.ForEach(DisableCardVisual);
        
        //Return the removed cards  
        return cards;
    }

    /// <summary>
    /// Disables interaction for each active UI card
    /// </summary>
    public void DisableUiCardInteraction()
    {
        _activeUICards.ForEach(card => card.DisableInteract());
    }

    /// <summary>
    /// Enables interaction for each active UI card
    /// </summary>
    public void EnableUiCardInteraction()
    {
        _activeUICards.ForEach(card => card.EnableInteract());
    }
    
    /// <summary>
    /// Resets the hand. clearing cards and UI cards.
    /// </summary>
    public void Reset()
    {
        _activeUICards.ForEach(card => card.gameObject.SetActive(false));
        _activeUICards.Clear();
        Cards.Clear();
    }
}
