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

using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ButtonsManager : MonoBehaviour
    {

        [SerializeField] [Tooltip("The button corresponding to the Bet up action")]
        private Button betUpButton;

        [SerializeField] [Tooltip("The button corresponding to the Bet down action")]
        private Button betDownButton;

        [SerializeField] [Tooltip("The button corresponding to the Deal action")]
        private Button dealButton;

        public void DisableBetButtons()
        {
            betUpButton.interactable = false;
            betDownButton.interactable = false;
        }

        public void EnableBetButtons()
        {
            betUpButton.interactable = true;
            betDownButton.interactable = true;
        }

        public void SetDealEnabled(bool setEnabled)
        {
            dealButton.interactable = setEnabled;
        }
    }
}