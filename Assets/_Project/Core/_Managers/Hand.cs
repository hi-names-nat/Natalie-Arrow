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
using Game.UI.Cards;

namespace Game.Cards
{
    /// <summary>
    /// Acts as a hand of card. Used for both the player & dealer in blackjack.
    /// </summary>
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

        /// <summary>
        /// Adds the given list of cards to the hand, including UI
        /// </summary>
        /// <param name="cards">The cards to add</param>
        /// <param name="startClickable"></param>
        public void AddCards(List<Card> cards, bool startClickable = true)
        {
            Cards.AddRange(cards);

            foreach (var card in cards)
            {
                var uiCard = EnableCardVisual(card);
                if (!startClickable)  uiCard.DisableInteract();
            }
        }

        /// <summary>
        /// Adds a single card to the player's hand.
        /// </summary>
        /// <param name="card">The card to add</param>
        /// <param name="startClickable"></param>
        public void AddCard(Card card, bool startClickable = true)
        {
            Cards.Add(card);
            var uiCard = EnableCardVisual(card);
            if (!startClickable) uiCard.DisableInteract();
        }

        /// <summary>
        /// Adds a card to the hand, its face hidden
        /// </summary>
        /// <param name="card">The card to add</param>
        /// <param name="startClickable">If the UI Card should begin as being clickable (default: yes)</param>
        public void AddCardHidden(Card card, bool startClickable = true)
        {
            Cards.Add(card);
            var cardVisual = EnableCardVisual(card);
            cardVisual.SetCardHidden();
            if (!startClickable) cardVisual.DisableInteract();
        }
        
        /// <summary>
        /// Unhides a single card in the hand
        /// </summary>
        /// <param name="card">The card to unhide</param>
        public void UnhideCard(Card card)
        {
            var cardVisual = _uiCards.First(uiCard => uiCard.cardDefinition == card);
            cardVisual.SetCardShown();
        }

        /// <summary>
        /// Unhides all hidden cards in this hand.
        /// </summary>
        public void UnhideHand()
        {
            foreach (var uiCard in _uiCards.Where(uiCard => uiCard.Hidden))
            {
                uiCard.SetCardShown();
            }
        }

        /// <summary>
        /// Enables the UIcard from the 52 card list.
        /// </summary>
        /// <param name="card">The card to enable</param>
        private UICard EnableCardVisual(Card card)
        {
            var cardVisual = _uiCards.First(uiCard => uiCard.cardDefinition == card);
            _activeUICards.Add(cardVisual);
            cardVisual.gameObject.SetActive(true);
            cardVisual.transform.SetAsLastSibling();
            return cardVisual;
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
        
        private void Awake()
        {
            _uiCards = GetComponentsInChildren<UICard>(true).ToList();
            _activeUICards = new List<UICard>();
            Cards = new List<Card>();
        }
    }
}
