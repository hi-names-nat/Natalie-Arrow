/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/15/23
 * 
 * What: CardStackTests.cs
 * 
 * Function: Contains tests for the standard poker victory types.
 *
 * 
 ***********************************************************/
using System;
using System.Collections.Generic;
using Game.Poker;
using Game.Poker.Victory;
using NUnit.Framework;

namespace Game.Cards
{
    public class TestPokerVictories
    {
        Func<List<Card>, PokerSettings, bool> TestedMethod;

        private readonly PokerSettings noDeuces = new PokerSettings { wildDeuces = false };
        private PokerSettings deuces = new PokerSettings { wildDeuces = true };

        [Test]
        public void TestRoyalFlush()
        {
            TestedMethod = StandardPokerVictoryImplementations.RoyalFlush;
            
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Heart, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Five),
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));

            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Heart, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Five),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));

            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
        }
        
        [Test]
        public void TestStraight()
        {

            TestedMethod = StandardPokerVictoryImplementations.Straight;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Six)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            //Edge case, Aces Low
            testHand = new List<Card>
            {
                new(Suit.Spade, Rank.Ace),
                new(Suit.Spade, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Five),
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            //Edge Case, Aces High
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            //Failure Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Seven),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            //Deuces test
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Seven),
                new(Suit.Diamond, Rank.King),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            //Deuces test
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Five),
                new(Suit.Diamond, Rank.Six),
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Six),
                new(Suit.Diamond, Rank.Seven),
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.King),
                new(Suit.Heart, Rank.Three),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Jack),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Club, Rank.Eight),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Club, Rank.Four),
                new(Suit.Spade, Rank.Four),
                new(Suit.Club, Rank.Two),
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Club, Rank.Five),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Club, Rank.Four),
                new(Suit.Spade, Rank.Ace),
                new(Suit.Club, Rank.Two),
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
        }
        
        [Test]
        public void TestFlush()
        {
            TestedMethod = StandardPokerVictoryImplementations.Flush;
            
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Six)
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            testHand = new List<Card>
            {
                new(Suit.Spade, Rank.Ace),
                new(Suit.Spade, Rank.Two),
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Five),
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ten),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Seven),
                new(Suit.Diamond, Rank.King),
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
        }

        [Test]
        public void TestTwoPair()
        {
            TestedMethod = StandardPokerVictoryImplementations.TwoPair;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Five)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Queen),
                new(Suit.Heart, Rank.Queen)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King ),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Queen),
                new(Suit.Heart, Rank.Jack)
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            //Deuces not implemented, not used in wild twos.
        }

        [Test]
        public void TestThreeKind()
        {
            TestedMethod = StandardPokerVictoryImplementations.ThreeKind;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Heart, Rank.King),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            // Deuces
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Ten),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Spade, Rank.Ace),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Queen),
                new(Suit.Heart, Rank.Nine)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
        }

        [Test]
        public void TestFourKind()
        {
            TestedMethod = StandardPokerVictoryImplementations.FourKind;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Club, Rank.Two),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Heart, Rank.Four),
                new(Suit.Diamond, Rank.Two),        
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Club, Rank.Two),
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Heart, Rank.King),
                new(Suit.Club, Rank.King),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));

            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Club, Rank.Two),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Heart, Rank.Four),
                new(Suit.Diamond, Rank.Two),        
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Club, Rank.Two),
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Heart, Rank.Two),
                new(Suit.Club, Rank.Two),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Ace),
                new(Suit.Heart, Rank.Ace),
                new(Suit.Spade, Rank.Ace),
                new(Suit.Heart, Rank.Ace)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
        }

        [Test]
        public void TestFullHouse()
        {
            TestedMethod = StandardPokerVictoryImplementations.FullHouse;
            
            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Five)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.King),
                new(Suit.Heart, Rank.King),
                new(Suit.Spade, Rank.Queen),
                new(Suit.Heart, Rank.Queen)
            };
            Assert.True(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.False(TestedMethod.Invoke(testHand, noDeuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Five)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.King),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Queen)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Five),
                new(Suit.Heart, Rank.Four)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
        }
    }
}