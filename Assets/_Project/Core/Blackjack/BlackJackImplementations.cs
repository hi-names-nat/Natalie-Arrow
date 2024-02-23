using System.Collections.Generic;
using System.Linq;
using Game.Cards;

namespace Game.Blackjack
{
    public static class BlackJackImplementations
    {
        // the score that the player & dealers want to hit
        public static int goalScore = 21;
        
        /// <summary>
        /// Get the value of the given hand.
        /// </summary>
        /// <param name="isDealer">If this hand is the dealer's (always treat ace as 11)</param>
        /// <returns></returns>
        public static int GetHandValue(IEnumerable<Card> cards, bool isDealer)
        {
            var orderedCards = cards.OrderBy(card => card.Rank == Rank.Ace).ThenBy(card => card.Rank);

            return orderedCards.Aggregate(0, (current, card) => current + (int)card.Rank switch
            {
                < 9 => (int)card.Rank + 2,
                >= 10 => 10,
                //always expecting ace to be last.
                9 when current > goalScore-11 && !isDealer => 1,
                9 => 11
            });// int
        }
    }
}