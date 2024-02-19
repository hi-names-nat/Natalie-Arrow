using System;
using System.Collections.Generic;
using Game.Cards;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Victory
{
    enum VictoryType
    {
        RoyalFlush,
        StraightFlush,
        FourOfAKind,
        FullHouse,
        Flush,
        Straight,
        ThreeOfAKind,
        TwoPair,
        JacksOrBetter,
        Etc,
    }
    
    [CreateAssetMenu(fileName = "New Victory Condition", menuName = "Game/Victory Condition", order = 0)]
    public class VictoryCondition : ScriptableObject
    {
        [SerializeField]
        private int payout;
        [SerializeField] 
        private VictoryType victoryType;
        
        //Static lookup table for what each VictoryType method is attached to what enum.
        private static readonly Dictionary<VictoryType, Func<List<Card>, bool>> VictoryTable = new()
        {
            { VictoryType.RoyalFlush, StandardPokerVictoryImplementations.RoyalFlush },
            { VictoryType.StraightFlush, StandardPokerVictoryImplementations.StraightFlush },
            { VictoryType.FourOfAKind, StandardPokerVictoryImplementations.FourKind },
            { VictoryType.FullHouse, StandardPokerVictoryImplementations.FullHouse },
            { VictoryType.Flush, StandardPokerVictoryImplementations.Flush },
            { VictoryType.Straight, StandardPokerVictoryImplementations.Straight },
            { VictoryType.ThreeOfAKind, StandardPokerVictoryImplementations.ThreeKind },
            { VictoryType.TwoPair, StandardPokerVictoryImplementations.TwoPair },
            { VictoryType.JacksOrBetter, SpecialtyVictoryImplementations.JacksOrBetter}
        };

        public virtual bool IsSatisfied(List<Card> cards)
        {
            
            return VictoryTable[victoryType].Invoke(cards);
            
        }
    }
}