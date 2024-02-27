/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/15/23
 * 
 * What: CardStackTests.cs
 * 
 * Function: Contains tests for the card manager.
 *
 * 
 ***********************************************************/
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Game.Cards
{
    public class CardStackTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestStackGenerationAndShuffle()
        {
            Queue<Card> exampleCardStack = new Queue<Card>();
            for (uint i = 0; i < 13; i++)
            {
                exampleCardStack.Enqueue(new Card(Suit.Spade, (Rank)i));
            }
            for (uint i = 0; i < 13; i++)
            {
                exampleCardStack.Enqueue(new Card(Suit.Club, (Rank)i));
            }
            for (uint i = 0; i < 13; i++)
            {
                exampleCardStack.Enqueue(new Card(Suit.Heart, (Rank)i));
            }
            for (uint i = 0; i < 13; i++)
            {
                exampleCardStack.Enqueue(new Card(Suit.Diamond, (Rank)i));
            }
            Random.InitState(0);
            exampleCardStack = new Queue<Card>(exampleCardStack.OrderBy(c => Random.value));
            
            Random.InitState(0);
            var testingCardStack = new CardManager().GetCardStackAsArray();

            for (var i = 0; i < 52; i++)
            {
                Assert.That(() => testingCardStack[i] == exampleCardStack.ToArray()[i]);
            }
        }
    }
}