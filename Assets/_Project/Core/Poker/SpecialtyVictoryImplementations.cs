using System.Collections.Generic;
using Game.Cards;

namespace Game.Victory
{
    public static class SpecialtyVictoryImplementations
    {
        /// <summary>
        /// Checks if the hand given contains a card that is a jack or better (jack, Queen, King)
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool JacksOrBetter(List<Card> cards)
        {
            return cards.Exists(card => (int)card.Rank >= 10);
        }
    }
}