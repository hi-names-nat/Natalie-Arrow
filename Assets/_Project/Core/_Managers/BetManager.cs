/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/24
 * 
 * What: BetManager.cs
 * 
 * Function: Manages the bet state and the related UI. Includes
 * managing the Bet state, payouts, and the end game message.
 *
 ***********************************************************/
using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace _Project.Core._Managers
{
    public class BetManager: MonoBehaviour
    {
        /// <summary>
        /// The maximum allowable bet modifier
        /// </summary>
        private const int MaxBetModifier = 5;

        /// <summary>
        /// An action that is called when money is payed out, includes as a parameter how much was payed out.
        /// </summary>
        public event Action<int> OnPayout;

        
        /// <summary>
        /// The Text instance that is the initial bet, or bet modifier
        /// </summary>
        [SerializeField] private TMP_Text betMultiplierText;
        /// <summary>
        /// The text instance that is the bank text.
        /// </summary>
        [SerializeField] private TMP_Text bankText;
        
        /// <summary>
        /// The victory/defeat text instance
        /// </summary>
        [SerializeField] private TMP_Text endText;

        /// <summary>
        /// The bet multiplier, or initial bet
        /// </summary>
        private int BetMultiplier { get; set; } = 1;
        /// <summary>
        /// The player's bank
        /// </summary>
        private float Bank { get; set; }

        /// <summary>
        /// Sets the starting bank
        /// </summary>
        /// <param name="bank">The value to set the bank at</param>
        public void SetStartingBank(float bank)
        {
            Bank = bank;
            bankText.text = Bank.ToString("C");
        }
        
        /// <summary>
        /// Sets the starting bet lower
        /// </summary>
        public void DecreaseBet()
        {
            if (BetMultiplier == 1) return;
            
            BetMultiplier -= 1;
            betMultiplierText.text = BetMultiplier.ToString(CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// Increases the starting bet
        /// </summary>
        public void IncreaseBet()
        {
            if (BetMultiplier == MaxBetModifier) return;
            
            BetMultiplier += 1;
            betMultiplierText.text = BetMultiplier.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// If the player is currently able to bet at this price.
        /// </summary>
        /// <returns></returns>
        public bool CanPlaceBet()
        {
            return BetMultiplier <= Bank;
        }

        /// <summary>
        /// Removes the money from the current selected bet level from the bank
        /// </summary>
        /// <returns>If the bank contained enough money</returns>
        public bool PlaceBet()
        {
            if (BetMultiplier > Bank) return false;
            Bank -= BetMultiplier;
            bankText.text = Bank.ToString("C");
            return true;
        }

        /// <summary>
        /// Adds money to the bank
        /// </summary>
        /// <param name="payout">the payout to add to the bank</param>
        /// <param name="isJackpot">if true, ignores the bet modifier in favor of directly applying the given payout</param>
        public void UpdateBank(int payout, bool isJackpot = false)
        {
            var finalPayout = isJackpot && BetMultiplier == MaxBetModifier ? payout : payout * BetMultiplier; 
            
            Bank += finalPayout;
            OnPayout?.Invoke(finalPayout);
            
            bankText.text = Bank.ToString("C");
        }

        /// <summary>
        /// Displays the end-of round message 
        /// </summary>
        /// <param name="message">The message to display</param>
        public void ShowEndMessage(string message)
        {
            endText.text = message;
            endText.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the end-of round message 
        /// </summary>
        public void HideEndMessage()
        {
            endText.gameObject.SetActive(false);
        }
    }
}