/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/23/23
 * 
 * What: SpecialityVictoryImplementations.cs
 * 
 * Function: Contains all the victory state checks that are for specific gamemodes. Jacks/Deuces.
 * All static as it's called by the scriptableobject.
 *
 * 
 ***********************************************************/
using System.Collections.Generic;
using System.Linq;
using Game.Cards;

namespace Game.Poker.Victory
{
    public static class SpecialtyVictoryImplementations
    {
        /// <summary>
        /// Checks if the hand given contains a card that is a jack or better (jack, Queen, King)
        /// </summary>
        /// <param name="hand">The hand to check</param>
        /// <returns>If the hand contains jacks or better</returns>
        public static bool JacksOrBetter(List<Card> hand, PokerSettings settings)
        {
            if (hand == null) return false;
            
            return hand.Exists(card => (int)card.Rank >= 10);
        }

        /// <summary>
        /// Checks if the hand given contains four twos (deuces)
        /// </summary>
        /// <param name="hand">The hand to check</param>
        /// <param name="settings">The gamesettings</param>
        /// <returns>If the hand contains four twos</returns>
        public static bool FourDeuces(List<Card> hand, PokerSettings settings)
        {
            if (hand == null || !settings.wildDeuces) return false;
            
            return hand.Count(card => card.Rank == Rank.Two) == 4;
        }

        /// <summary>
        /// Does the hand contain five of a kind (4 of a  kind & deuce, etc)
        /// </summary>
        /// <param name="hand">The hand to check</param>
        /// <param name="settings">The gamesettings</param>
        /// <returns>The above conditional</returns>
        public static bool FiveOfAKind(List<Card> hand, PokerSettings settings)
        {
            if (hand == null || !settings.wildDeuces) return false;
            
            if (!hand.Exists(card => card.Rank == Rank.Two)) return false;
            var cards = new List<Card>(hand);
            cards.RemoveAll(card => card.Rank == Rank.Two);

            return cards.All(card => card.Rank == cards[0].Rank);
        }

        /// <summary>
        /// Does the hand given contain a royal flush, using deuces as substitutes 
        /// </summary>
        /// <param name="hand">The hand to check</param>
        /// <param name="settings">The gamesettings</param>
        /// <returns>The above conditions</returns>
        public static bool RoyalFlushWithDeuces(List<Card> hand, PokerSettings settings)
        {
            if (hand == null) return false;
            if (!StandardPokerVictoryImplementations.Flush(hand, settings)) return false;
            
            var cards = new List<Card>(hand);
            var deuceCount = cards.RemoveAll(card => card.Rank == Rank.Two);
            
            var orderedCards = cards.Select(card => card.Rank).Distinct().ToList();
            orderedCards.Sort();
            
            
            if (orderedCards.Exists(rank => (int)rank < 8))
            {
                return false;
            }
            

            return orderedCards.Count + deuceCount == 5;
        }
    }
}