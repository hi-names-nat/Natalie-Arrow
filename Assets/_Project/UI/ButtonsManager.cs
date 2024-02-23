/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/24
 * 
 * What: ButtonsManager.cs
 * 
 * Function: Manage the UI buttons at the bottom of the screen, Acting as a quick way to turn button groups
 * or individuals on or off.
 *
 ***********************************************************/

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public enum UIButtons
    {
        BetUp,
        BetDown,
        Deal,
        BlackJackDeal
    }
    public class ButtonsManager : MonoBehaviour
    {

        [SerializeField] [Tooltip("The button corresponding to the Bet up action")]
        private Button betUpButton;

        [SerializeField] [Tooltip("The button corresponding to the Bet down action")]
        private Button betDownButton;

        [SerializeField] [Tooltip("The button corresponding to the Deal action")]
        private Button dealButton;
        
        [Header("Blackjack Only")]
        [SerializeField] [Tooltip("The button corresponding to the Stay action. Only used in BlackJack")]
        private Button blackJackDeal;

        /// <summary>
        /// Disables the interactability for the bet up/down buttons
        /// </summary>
        public void DisableBetButtons()
        {
            betUpButton.interactable = false;
            betDownButton.interactable = false;
        }

        /// <summary>
        /// Enables the interactability for the bet up/down buttons
        /// </summary>
        public void EnableBetButtons()
        {
            betUpButton.interactable = true;
            betDownButton.interactable = true;
        }

        
        /// <summary>
        /// Sets the deal button to be interactable, based on the passed bool
        /// </summary>
        /// <param name="setEnabled">if the deal button should be clickable</param>
        public void SetDealClickable(bool setEnabled)
        {
            dealButton.interactable = setEnabled;
        }
        
        /// <summary>
        /// Sets the text for the specified button
        /// </summary>
        /// <param name="text">The text to set</param>
        /// <param name="button">Which button to change the text of</param>
        /// <exception cref="ArgumentOutOfRangeException">If the UIButtons enum is out of range, this is thrown.</exception>
        public void SetButtonText(string text, UIButtons button)
        {
            Button buttonComponent = button switch
            {
                UIButtons.BetUp => betUpButton,
                UIButtons.BetDown => betDownButton,
                UIButtons.Deal => dealButton,
                UIButtons.BlackJackDeal => blackJackDeal,
                _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
            };

            buttonComponent.GetComponentInChildren<TMP_Text>().text = text;
        }

        /// <summary>
        /// Enables the blackjack-only deal button
        /// </summary>
        /// <param name="setEnabled">If they should be enabled</param>
        public void SetBlackJackDealEnabled(bool setEnabled)
        {
            blackJackDeal.gameObject.SetActive(setEnabled);
        }
    }
}