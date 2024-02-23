/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/24
 * 
 * What: VictoryCondition.cs
 * 
 * Function: A scriptableobject that contains a singular victory condition,
 * including what kind of victory it is, payout, and if it's a jackpot
 *
 ***********************************************************/

using System;
using System.Collections.Generic;
using Game.Cards;
using UnityEngine;

namespace Game.Victory
{
    public enum VictoryType
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
    }
    
    [CreateAssetMenu(fileName = "New Victory Condition", menuName = "Game/Victory Condition", order = 0)]
    public class VictoryCondition : ScriptableObject
    {
        //The         
        [SerializeField] [Tooltip("The type of victory")]
        public VictoryType victoryType;
        [SerializeField] [Tooltip("THe name to display on the victory message.")]
        public string victoryName;
        
        [SerializeField] [Tooltip("If set, when betmanager is set on max jackpot is paid, ignoring the bet multiplier")]
        public bool isJackpot;
        [SerializeField] [Tooltip("The jackpot payout to pay out")]
        public int jackpotPayout;
        
        [SerializeField] [Tooltip("The standard payout when this victorytype is satisfied")]
        public int payout;
        
        
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

        /// <summary>
        /// Forwarder function using the dictionary checking if this victory condition has been satisfied
        /// </summary>
        /// <param name="cards">the hand to check</param>
        /// <returns>If the hand satisfied the victory check</returns>
        public virtual bool IsSatisfied(List<Card> cards)
        {
            return VictoryTable[victoryType].Invoke(cards);
        }
    }
}