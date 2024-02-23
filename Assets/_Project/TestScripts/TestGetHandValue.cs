using System.Collections.Generic;
using Game.Blackjack;
using NUnit.Framework;

namespace Game.Cards
{
    public class TestGetHandValue
    {
        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [Test]
        public void TestGetHandValues()
        {
            var hand = new List<Card>();            
            //Blackjack. 10 (king) + Ace (11)
            hand.AddRange(new List<Card>()
            {
                new Card(Suit.Club, Rank.Ace),
                new Card(Suit.Club, Rank.King),
            });
            Assert.That(BlackJackImplementations.GetHandValue(hand, 21, false) == 21);
            
            hand.Clear();
            
            //13 for player (11 + 1 + 1) and 33 for dealer (11 + 11 + 11)   
            hand.AddRange(new List<Card>()
            {
                new Card(Suit.Club, Rank.Ace),
                new Card(Suit.Club, Rank.Ace),
                new Card(Suit.Club, Rank.Ace),
            });
            Assert.That(BlackJackImplementations.GetHandValue(hand, 21, false) == 13);
            Assert.That(BlackJackImplementations.GetHandValue(hand, 21, true) == 33);
            
            hand.Clear();
            
            hand.AddRange(new List<Card>()
            {
                new Card(Suit.Club, Rank.Ace),
                new Card(Suit.Club, Rank.King),
                new Card(Suit.Club, Rank.Two),
            });
            Assert.That(BlackJackImplementations.GetHandValue(hand, 21, false) == 13);
        }
    }
}