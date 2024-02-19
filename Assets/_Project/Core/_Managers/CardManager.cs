using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Cards
{
    public enum Suit: uint
    {
        Spade = 0,
        Club = 1,
        Heart = 2,
        Diamond = 3,
    }

    public enum Rank: uint
    {
          Two = 0,   Three = 1,
        Four = 2,  Five = 3,  Six = 4, 
        Seven = 5, Eight = 6, Nine = 7,
        Ten = 8, Ace = 9,  Jack = 10, Queen = 11,
        King = 12
    }
    
    /// <summary>
    /// Card object, representing a single playing card. Invalid if Rank is above 13.
    /// </summary>
    public struct Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        /// <summary>
        /// Generate a card from a suit and rank.
        /// </summary>
        /// <param name="suit">the suit the new card will be</param>
        /// <param name="rank">the rank the new card will be (from 0 to 12)</param>
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        /// <summary>
        /// Equality operator, are rank and suit equal?
        /// </summary>
        /// <param name="lhs">the left-hand side</param>
        /// <param name="rhs">the right-hand side</param>
        /// <returns>if the rank and suit of each card are equal</returns>
        public static bool operator ==(Card lhs, Card rhs)
        {
            return lhs.Rank == rhs.Rank && lhs.Suit == rhs.Suit;
        }
        
        /// <summary>
        /// Inequality operator, are rank or suit different?
        /// </summary>
        /// <param name="lhs">the left-hand side</param>
        /// <param name="rhs">the right-hand side</param>
        /// <returns>if either the rank or suit of each card are unequal</returns>
        public static bool operator !=(Card lhs, Card rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Override of GetHashCode for hashing stuff.
        /// </summary>
        /// <returns>the hashcode.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Suit, Rank);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public class CardManager
    { 
        private Stack<Card> _cardStack;

        /// <summary>
        /// Creates a new CardManager, containing a new, shuffled deck of cards.
        /// </summary>
        public CardManager()
        {

            _cardStack = new Stack<Card>();
            foreach (var suit in (Suit[])Enum.GetValues(typeof(Suit)))
            {
                foreach (var rank in (Rank[])Enum.GetValues(typeof(Rank)))
                {
                    _cardStack.Push(new Card(suit, rank));
                }
            }

            ShuffleDeck();
        }

        /// <summary>
        /// Removes a card from the deck, returning it. Will throw an exception if the stack is empty.
        /// </summary>
        /// <returns>The removed card</returns>
        public Card GetCard()
        {
            if (_cardStack.Count != 0) return _cardStack.Pop();
            else throw new ArgumentOutOfRangeException($"CardManager has run out of cards.");
        }

        /// <summary>
        /// Place a card back in the stack
        /// </summary>
        /// <param name="card">The Card to add back</param>
        /// <param name="allowCopies">If multiple copies of the same card type are allowed in the stack.</param>
        /// <returns></returns>
        public bool ReturnCard(Card card, bool allowCopies=false)
        {
            if (_cardStack.Contains(card) && !allowCopies) 
                return false;

            _cardStack.Push(card); 
            return true;
        }

        /// <summary>
        /// Shuffles the deck in-place, resulting in a randomized deck.
        /// </summary>
        /// <param name="shouldBeFullDeck">If the function should short-circuit if the deck isn't full (52 cards)</param>
        public void ShuffleDeck(bool shouldBeFullDeck=true)
        {
            if (shouldBeFullDeck && _cardStack.Count != 52) return;
            //todo, expensive space complexity, could replace.
            _cardStack = new Stack<Card>(_cardStack.OrderBy(_ => UnityEngine.Random.value));
        }

        /// <summary>
        /// Creates and returns a deep copy of the CardStack as an Array.
        /// Specifically for debug purposes. Do not use this in game code.
        /// </summary>
        /// <returns>Clone of CardStack as Card[]</returns>
        public Card[] GetCardStackAsArray()
        {
            return _cardStack.ToArray();
        }
    }

}