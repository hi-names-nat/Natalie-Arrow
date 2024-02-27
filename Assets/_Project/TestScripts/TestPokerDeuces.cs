/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/15/23
 * 
 * What: TestPokerDeuces.cs
 * 
 * Function: Contains tests for the Wild Twos poker gametype
 *
 * 
 ***********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Game.Poker;
using Game.Poker.Victory;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Game.Cards
{
    public class TestPokerDeuces
    {
        Func<List<Card>, PokerSettings, bool> TestedMethod;

        private PokerSettings deuces = new PokerSettings { wildDeuces = true };
        
        [Test]
        public void TestDeuceRoyalFlush()
        {
            TestedMethod = SpecialtyVictoryImplementations.RoyalFlushWithDeuces;

            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));

            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Diamond, Rank.Two)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.King),
                new(Suit.Diamond, Rank.King),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Diamond, Rank.Ace)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Ace),
                new(Suit.Spade, Rank.Two),
                new(Suit.Diamond, Rank.Jack),
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Diamond, Rank.King)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
        }
        
        [Test]
        public void TestFiveOfAKind()
        {
            TestedMethod = SpecialtyVictoryImplementations.FiveOfAKind;

            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Jack),
                new(Suit.Heart, Rank.Jack),
                new(Suit.Spade, Rank.Jack),
                new(Suit.Heart, Rank.Jack)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));

            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Ace),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Five),
                new(Suit.Spade, Rank.Four),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Spade, Rank.Queen),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Club, Rank.Two),
                new(Suit.Diamond, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Diamond, Rank.Four),
                new(Suit.Spade, Rank.Six)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
        }
        
        [Test]
        public void TestFourDeuces()
        {
            TestedMethod = SpecialtyVictoryImplementations.FourDeuces;

            // Standard Case
            var testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.King)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));

            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Spade, Rank.Ace),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Three),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two)
            };
            Assert.True(TestedMethod.Invoke(testHand, deuces));
            
            // Standard Case
            testHand = new List<Card>
            {
                new(Suit.Diamond, Rank.Queen),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Three),
                new(Suit.Spade, Rank.Two),
                new(Suit.Heart, Rank.Two)
            };
            Assert.False(TestedMethod.Invoke(testHand, deuces));
        }
    }
}