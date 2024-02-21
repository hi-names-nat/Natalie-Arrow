/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/24
 * 
 * What: VictoryManager.cs
 * 
 * Function: Class contained in victorymanager that manages all
 * victoryconditions.
 *
 ***********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Cards;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Victory
{
    
    [Serializable]
    public class VictoryManager
    {
        [FormerlySerializedAs("VictoryConditions")] [SerializeField] [Tooltip("All possible victory conditions")] 
        private List<VictoryCondition> victoryConditions;
        
        /// <summary>
        /// Checks if a victory condition has been satisfied   
        /// </summary>
        /// <param name="hand">the hand to check against</param>
        /// <returns>the VictoryCondition that was satisfied, or null if none were satisfied</returns>
        public VictoryCondition FindIfWon(List<Card> hand)
        {
            return victoryConditions.FirstOrDefault(victoryCondition => victoryCondition.IsSatisfied(hand));
        }
    }
}