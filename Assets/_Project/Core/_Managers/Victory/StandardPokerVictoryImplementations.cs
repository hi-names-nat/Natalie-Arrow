/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/15/23
 * 
 * What: StandardPokerVictoryImplementations.cs
 * 
 * Function: Contains all the victory state checks that are in the standard poker game type.
 * All static as it's called by the scriptableobject.
 *
 * 
 ***********************************************************/

using System.Collections.Generic;
using System.Linq;
using Game.Cards;

namespace Game.Victory
{
    public static class StandardPokerVictoryImplementations
    {
        /// <summary>
        /// Checks if the hand given is a Royal Flush (Ten, Ace, Jack, Queen, King of same suit)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <returns>The above condition</returns>
        public static bool RoyalFlush(List<Card> cards)
        {
            if (!Flush(cards)) return false;
            
            var orderedCards = cards.Select(card => card.Rank).ToList();
            orderedCards.Sort();
            
            var royalFlush = new List<Rank>() { Rank.Ten, Rank.Ace, Rank.Jack, Rank.Queen, Rank.King };
            
            return orderedCards.SequenceEqual(royalFlush);
        }
        
        /// <summary>
        /// Checks if the hand given is a Straight Flush (Straight & Flush)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <returns>The above condition</returns>
        public static bool StraightFlush(List<Card> cards)
        {
            return Straight(cards) && Flush(cards);
        }
        
        /// <summary>
        /// Checks if the hand given is a Four of a Kind (4 of the same rank, different suit)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <returns>The above condition</returns>
        public static bool FourKind(List<Card> cards) {
            if (cards == null) return false;
            
            var orderedCards = cards.OrderBy(card => card.Rank).ToList();
            int idx = orderedCards[0].Rank != orderedCards[1].Rank ? 1 : 0;
            orderedCards = orderedCards.GetRange(idx, orderedCards.Count - 1);

            return orderedCards.All(c => c.Rank == orderedCards[0].Rank);
        }
        
        /// <summary>
        /// Checks if the hand given is a Full House (One pair of three, One pair of two)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <returns>The above condition</returns>
        public static bool FullHouse(List<Card> cards)
        {
            if (!ThreeKind(cards)) return false;
            var sortedCards = cards.OrderBy(card => card.Rank).ToList();
            return sortedCards[0].Rank == sortedCards[1].Rank &&
                   sortedCards[^1].Rank == sortedCards[^2].Rank;
        }
        
        /// <summary>
        /// Checks if the hand given is a Flush (All of the same suit)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <returns>The above condition</returns>
        public static bool Flush(List<Card> cards)
        {
            if (cards == null) return false;

            var suit = cards[0].Suit;
            return cards.GetRange(1, cards.Count-1).All(card => card.Suit == suit);
        }
        
        /// <summary>
        /// Checks if the hand given is a Straight (Rank in order)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <returns>The above condition</returns>
        public static bool Straight(List<Card> cards)
        {
            if (cards == null) return false;

            var orderedCards = cards.Select(card => card.Rank).ToList();
            orderedCards.Sort();

            if (orderedCards.Contains(Rank.Ace))
            {
                var acesLow = new List<Rank>() { Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Ace };
                var acesHigh = new List<Rank>() { Rank.Ten, Rank.Ace, Rank.Jack, Rank.Queen, Rank.King };

                return orderedCards.SequenceEqual(acesLow) || orderedCards.SequenceEqual(acesHigh);
            }

            for (var i = 0; i < orderedCards.Count-1; i++)
            {
                if (orderedCards[i] + 1 != orderedCards[i + 1]) return false;
            }

            return true;
        }
        
        /// <summary>
        /// Checks if the hand given is a Three of a kind (Three of the same rank)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <returns>The above condition</returns>
        public static bool ThreeKind(List<Card> cards) {
            if (cards == null) return false;
            
            var orderedCards = cards.OrderBy(card => card.Rank).ToArray();

            for (int i = 0; i < cards.Count-2; i++)
            {
                if (orderedCards[i].Rank == orderedCards[i + 1].Rank && orderedCards[i].Rank == orderedCards[i + 2].Rank) return true;
            }

            return false;
        }
        
        /// <summary>
        /// Checks if the hand given is a Two Pair (Two pairs of the same rank)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <returns>The above condition</returns>
        public static bool TwoPair(List<Card> cards)
        {
            if (cards == null) return false;

            var gotOnePair = false;

            var orderedCards = cards.OrderBy(card => card.Rank);
            for (int i = 0; i < cards.Count-1; i++)
            {
                if (cards[i].Rank != cards[i + 1].Rank) continue;
                
                if (gotOnePair) return true;
                
                gotOnePair = true;
            }

            return false;
        }
    }
}