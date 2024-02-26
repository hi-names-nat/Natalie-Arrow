/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/23
 * 
 * What: BaseGameType.cs
 * 
 * Function: The base class of the game type object, responsible for
 * managing the game loop.
 *
 * 
 ***********************************************************/

using System;
using _Project.Core._Managers;
using Game.Cards;
using Game.UI;
using Game.Victory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public abstract class BaseGameType : ScriptableObject
    {
        /// <summary>
        /// The game's starting bank.
        /// </summary>
        [SerializeField] [Tooltip("The game's starting bank")]
        public float startingBank = 100;

        [SerializeField] [Tooltip("The player's maximum hand size")]
        public int maxPlayerHand = 13;

        /// <summary>
        /// Called when the game continues
        /// </summary>
        /// <param name="cardManager">the Cardmanager in the scene</param>
        /// <param name="playerHand">the Hand in the scene</param>
        /// <param name="dealerHand"></param>
        /// <param name="victoryManager">the Victorymanager in the scene</param>
        /// <param name="betManager">the BetManager in the scene</param>
        /// <param name="buttonsManager">the ButtonsManager in the scene</param>
        /// <exception cref="NotImplementedException">Called on the base class. due to base not being intended to be
        /// implemented.</exception>
        public virtual void ContinueGameLoop(
            CardManager cardManager, Hand playerHand, Hand dealerHand, 
            VictoryManager victoryManager, BetManager betManager, ButtonsManager buttonsManager)
        {
        }

        public virtual void Reset()
        { }
    }
}