/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/21/24
 * 
 * What: BetTable.cs
 * 
 * Function: Manage the UI table at the top of the screen.
 * Responsible for color changes.
 *
 ***********************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Cards
{
    /// <summary>
    /// Handles the bet table in the poker gametypes.
    /// </summary>
    public class BetTable : MonoBehaviour
    {
        
        [SerializeField] [Tooltip("The numeric panels to manage the colors of") ]
        private List<Image> panels;
        [SerializeField] [Tooltip("The color to show when the row is selected")] 
        private Color selectedColor = new Color(1,0,0,1);
        [SerializeField] [Tooltip("The color to show when the row is not selected")] private Color unselectedColor = new Color(1,1,1,1);
        
        /// <summary>
        /// The current index in panels of the selected panel.
        /// </summary>
        private int _selectedPanelIndex;

        /// <summary>
        /// Moves the selected panel (the part that is red) either up the bet table or down
        /// </summary>
        /// <param name="lowering">Should the bet table be lowered? if not, it's raised.</param>
        public void ScrollPanel(bool lowering)
        {
            if (lowering)
            {
                if (panels[_selectedPanelIndex] == panels[0]) return;

                panels[_selectedPanelIndex].color = unselectedColor;
                _selectedPanelIndex--;
                panels[_selectedPanelIndex].color = selectedColor;
            }
            else
            {
                if (panels[_selectedPanelIndex] == panels[4]) return;

                panels[_selectedPanelIndex].color = unselectedColor;
                _selectedPanelIndex++;
                panels[_selectedPanelIndex].color = selectedColor;
            }
        }
        
        private void Awake()
        {
            panels[0].color = selectedColor;
            _selectedPanelIndex = 0;
        }
    }
}