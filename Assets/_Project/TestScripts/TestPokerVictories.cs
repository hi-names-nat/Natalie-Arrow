using System;
using System.Collections.Generic;
using Game.Victory;
using NUnit.Framework;

namespace Game.Cards
{
    public class TestPokerVictories
    {
        Func<List<Card>, bool> TestedMethod = StandardPokerVictoryImplementations.RoyalFlush;

        
        [Test]
        public void TestRoyalFlush()
        {
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(TestedMethod.Invoke(testHand));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Heart, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Five),
            };
            Assert.False(TestedMethod.Invoke(testHand));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand));

            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand));
        }
        
        [Test]
        public void TestStraight()
        {

            Func<List<Card>, bool> testedMethod = StandardPokerVictoryImplementations.Straight;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Six)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            //Edge case, Aces Low
            testHand = new List<Card>
            {
                new(Suit.Spade, Rank.Ace),
                new(Suit.Spade, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Five),
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            //Edge Case, Aces High
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            //Failure Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Seven),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(testedMethod.Invoke(testHand));
        }
        
        [Test]
        public void TestFlush()
        {
            Func<List<Card>, bool> testedMethod = StandardPokerVictoryImplementations.Flush;
            
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Six)
            };
            Assert.False(testedMethod.Invoke(testHand));
            
            testHand = new List<Card>
            {
                new(Suit.Spade, Rank.Ace),
                new(Suit.Spade, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Five),
            };
            Assert.False(testedMethod.Invoke(testHand));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Seven),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(testedMethod.Invoke(testHand));
        }

        [Test]
        public void TestTwoPair()
        {
            Func<List<Card>, bool> testedMethod = StandardPokerVictoryImplementations.TwoPair;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Five)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Queen),
                new(Suit.Heart, Rank.Queen)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King ),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Queen),
                new(Suit.Heart, Rank.Jack)
            };
            Assert.False(testedMethod.Invoke(testHand));
        }

        [Test]
        public void TestThreeKind()
        {
            Func<List<Card>, bool> testedMethod = StandardPokerVictoryImplementations.ThreeKind;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Heart, Rank.King),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(testedMethod.Invoke(testHand));
        }

        [Test]
        public void TestFourKind()
        {
            Func<List<Card>, bool> testedMethod = StandardPokerVictoryImplementations.FourKind;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Club, Rank.Two),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Heart, Rank.Four),
                new(Suit.Diamond, Rank.Two),        
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Club, Rank.Two),
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Heart, Rank.King),
                new(Suit.Club, Rank.King),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(testedMethod.Invoke(testHand));

        }

        [Test]
        public void TestFullHouse()
        {
            Func<List<Card>, bool> testedMethod = StandardPokerVictoryImplementations.FullHouse;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Five)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Heart, Rank.King),
                new(Suit.Spade, Rank.Queen),
                new(Suit.Heart, Rank.Queen)
            };
            Assert.True(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(testedMethod.Invoke(testHand));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(testedMethod.Invoke(testHand));
        }
    }
}