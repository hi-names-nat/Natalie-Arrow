using System;
using System.Collections.Generic;
using System.Resources;
using Game.Cards;
using UnityEngine;

namespace Game.Victory
{
    [Serializable]
    public class VictoryManager
    {

        [SerializeField] private List<VictoryCondition> VictoryConditions;
        
        void FindIfWon()
        {

            foreach (var victoryCondition in VictoryConditions)
            {
                //Replace with Hand
                if (victoryCondition.IsSatisfied(new List<Card>()))
                {
                    //This is the best victory condition do the stuff
                }
            }
            
            //Then loop thru each VictoryCondition
        }
    }
}