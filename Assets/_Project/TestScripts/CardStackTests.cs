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
            Stack<Card> exampleCardStack = new Stack<Card>();
            for (uint i = 0; i < 13; i++)
            {
                exampleCardStack.Push(new Card(Suit.Spade, (Rank)i));
            }
            for (uint i = 0; i < 13; i++)
            {
                exampleCardStack.Push(new Card(Suit.Club, (Rank)i));
            }
            for (uint i = 0; i < 13; i++)
            {
                exampleCardStack.Push(new Card(Suit.Heart, (Rank)i));
            }
            for (uint i = 0; i < 13; i++)
            {
                exampleCardStack.Push(new Card(Suit.Diamond, (Rank)i));
            }
            Random.InitState(0);
            exampleCardStack = new Stack<Card>(exampleCardStack.OrderBy(c => Random.value));
            
            Random.InitState(0);
            var testingCardStack = new CardManager().GetCardStackAsArray();

            for (int i = 0; i < 52; i++)
            {
                Assert.That(() => testingCardStack[i] == exampleCardStack.ToArray()[i]);
            }
        }
    }
}