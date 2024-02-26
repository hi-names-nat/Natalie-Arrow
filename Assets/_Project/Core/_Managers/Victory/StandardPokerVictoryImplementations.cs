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

using System;
using System.Collections.Generic;
using System.Linq;
using Game.Cards;

namespace Game.Poker.Victory
{
    public static class StandardPokerVictoryImplementations
    {
        /// <summary>
        /// Checks if the hand given is a Royal Flush (Ten, Ace, Jack, Queen, King of same suit).
        /// disables deuces. this is the 'TRUE' royal flush.
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <param name="settings">Settings for the current poker state. Not used here.</param>
        /// <returns>The above condition</returns>
        public static bool RoyalFlush(List<Card> cards, PokerSettings settings)
        {
            settings.wildDeuces = false;
            if (!Flush(cards, settings)) return false;
            
            var orderedCards = cards.Select(card => card.Rank).ToList();
            orderedCards.Sort();
            
            var royalFlush = new List<Rank> { Rank.Ten, Rank.Ace, Rank.Jack, Rank.Queen, Rank.King };
            
            return orderedCards.SequenceEqual(royalFlush);
        }

        /// <summary>
        /// Checks if the hand given is a Straight Flush (Straight & Flush)
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <param name="settings"></param>
        /// <returns>The above condition</returns>
        public static bool StraightFlush(List<Card> cards, PokerSettings settings)
        {
            return Straight(cards, settings) && Flush(cards, settings);
        }

        /// <summary>
        /// Checks if the hand given is a Four of a Kind (4 of the same rank, different suit)
        /// </summary>
        /// <param name="hand">The given hand</param>
        /// <param name="settings">The poker gamesettings</param>
        /// <returns>The above condition</returns>
        public static bool FourKind(List<Card> hand, PokerSettings settings)
        {
            if (hand == null) return false;

            var cards = new List<Card>(hand);
            var deuceCount = settings.wildDeuces ? cards.RemoveAll(card => card.Rank == Rank.Two) : 0;
            if (deuceCount == 4) return true;
            
            var orderedCards = cards.OrderBy(card => card.Rank).ToList();
            var idx = orderedCards[0].Rank != orderedCards[1].Rank ? 1 : 0;
            orderedCards = orderedCards.GetRange(idx, orderedCards.Count - 1);

            return orderedCards.Count(c => c.Rank == orderedCards[0].Rank) + deuceCount == 4;
        }

        /// <summary>
        /// Checks if the hand given is a Full House (One triplet of three, One pair of two)
        /// </summary>
        /// <param name="hand">The given hand</param>
        /// <param name="settings">The poker gamesettings</param>
        /// <returns>The above condition</returns>
        public static bool FullHouse(List<Card> hand, PokerSettings settings)
        {
            if (hand == null) return false;
            
            var cards = new List<Card>(hand);
            var deuceCount = settings.wildDeuces ? cards.RemoveAll(card => card.Rank == Rank.Two) : 0;
            switch (deuceCount)
            {
                case >= 3:
                    return true;
                case 2 when (TwoKind(cards, settings)):
                case 1 when ThreeKind(cards, settings):
                    return true;
                case 1:
                    return TwoPair(cards, settings);
            }

            if (!ThreeKind(cards, settings)) return false;
            var sortedCards = cards.OrderBy(card => card.Rank).ToList();
            return sortedCards[0].Rank == sortedCards[1].Rank &&
                   sortedCards[^1].Rank == sortedCards[^2].Rank;
        }

        /// <summary>
        /// Checks if the hand given is a Flush (All of the same suit)
        /// </summary>
        /// <param name="hand">The given hand</param>
        /// <param name="settings">The poker gamesettings</param>
        /// <returns>The above condition</returns>
        public static bool Flush(List<Card> hand, PokerSettings settings)
        {
            if (hand == null) return false;
            
            var cards = new List<Card>(hand);
            
            var suit = cards[0].Suit;
            return cards.GetRange(1, cards.Count-1).All(card => card.Suit == suit);
            
            //I'm not sure if wild means both wild rank and suit. assuming not.
            // var deuceCount = settings.wildDeuces ? cards.RemoveAll(card => card.Rank == Rank.Two) : 0;
            // return cards.GetRange(1, cards.Count-1).Count(card => card.Suit == suit) + deuceCount == 4;
        }

        /// <summary>
        /// Checks if the hand given is a Straight (Rank in order)
        /// </summary>
        /// <param name="hand">The given hand</param>
        /// <param name="settings">The game settings</param>
        /// <returns>The above condition</returns>
        public static bool Straight(List<Card> hand, PokerSettings settings)
        {
            if (hand == null) return false;
            
            var cards = new List<Card>(hand);
            var deuceCount = settings.wildDeuces ? cards.RemoveAll(card => card.Rank == Rank.Two) : 0;

            var orderedCards = cards.Select(card => card.Rank).OrderBy(rank => rank).ToList();
            orderedCards.Sort();
            
            var acesLow = new List<Rank>() { Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Ace };
            var acesHigh = new List<Rank>() { Rank.Ten, Rank.Ace, Rank.Jack, Rank.Queen, Rank.King };

            if (acesHigh.Except(orderedCards).ToArray().Length <= deuceCount || 
                acesLow.Except(orderedCards).ToArray().Length <= deuceCount) return true;

            var count = orderedCards[0];
            for (var i = 1; i < orderedCards.Count; i++)
            {
                if (++count == orderedCards[i]) continue;

                if (orderedCards[i] == orderedCards[i - 1]) return false;
                
                if (deuceCount > 0)
                {
                    deuceCount -= (int)orderedCards[i] - (int)count;
                    if (deuceCount < 0) return false;
                    
                    count++;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>   
        /// Checks if the hand given is a Three of a kind (Three of the same rank)
        /// </summary>
        /// <param name="hand">The given hand</param>
        /// <param name="settings">The game settings</param>
        /// <returns>The above condition</returns>
        public static bool ThreeKind(List<Card> hand, PokerSettings settings) {
            if (hand == null) return false;
            
            var cards = new List<Card>(hand);
            var deuceCount = settings.wildDeuces ? cards.RemoveAll(card => card.Rank == Rank.Two) : 0;
            if (deuceCount >= 2 || (TwoKind(cards, settings) && deuceCount == 1)) return true;
            
            var orderedCards = cards.OrderBy(card => card.Rank).ToArray();

            for (int i = 0; i < cards.Count-2; i++)
            {
                if (orderedCards[i].Rank == orderedCards[i + 1].Rank && orderedCards[i].Rank == orderedCards[i + 2].Rank) return true;
            }

            return false;
        }

        private static bool TwoKind(List<Card> hand, PokerSettings settings)
        {
            if (hand == null) return false;
            
            var cards = new List<Card>(hand);
            var deuceCount = settings.wildDeuces ? cards.RemoveAll(card => card.Rank == Rank.Two) : 0;
            if (deuceCount != 0) return true;
            
            cards = cards.OrderBy(card => card.Rank).ToList();

            for (int i = 0; i < cards.Count-1; i++)
            {
                if (cards[i].Rank != cards[i + 1].Rank) continue;
                
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the hand given is a Two Pair (Two pairs of the same rank). Does not support wild deuces.
        /// </summary>
        /// <param name="cards">The given hand</param>
        /// <param name="settings">The fine settings for the gamemode</param>
        /// <returns>The above condition</returns>
        public static bool TwoPair(List<Card> cards, PokerSettings settings )
        {
            if (cards == null) return false;
            
            var gotOnePair = false;

            cards = cards.OrderBy(card => card.Rank).ToList();
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