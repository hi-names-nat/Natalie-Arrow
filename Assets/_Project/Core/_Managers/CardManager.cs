using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public enum Suit: int
{
    Spade=0,
    Club=1,
    Heart=2,
    Diamond=3,
}

public struct Card
{
    public Suit Suit;
    public uint Rank;

    public Card(Suit suit, uint rank)
    {
        Suit = suit;
        Rank = rank;
    }
}

public class CardManager
{
    private Stack<Card> _cardStack;
    
    public CardManager()
    {
        
        _cardStack = new Stack<Card>();
        foreach (var suit in (Suit[])Enum.GetValues(typeof(Suit)))
        {
            for (uint i = 0; i < 13; i++)
            {
                _cardStack.Push(new Card(suit, i));
            }
        }

        //todo, expensive space complexity, could replace.
        _cardStack = new Stack<Card>(_cardStack.OrderBy(c => UnityEngine.Random.value));
    }

    public Card GetCard()
    {
        return _cardStack.Pop();
    }
}
