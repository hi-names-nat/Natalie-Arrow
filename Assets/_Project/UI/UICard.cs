/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/24
 * 
 * What: UICard.cs
 * 
 * Function: Manages the UI face of the cards. Responsible for
 * Managing held behavior and managing if they're being displayed or not.
 *
 ***********************************************************/

using System.Net.Mime;
using Game.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Cards
{
    [RequireComponent(typeof(Button))]
    public class UICard : MonoBehaviour
    {
        [SerializeField] [Tooltip("The card this UI Card represents")]
        public Card cardDefinition;

        [SerializeField] [Tooltip("The \"back\" of the UI Card")]
        private Sprite cardBack;
        
        /// <summary>
        /// The holdindicator object, a UI element enabled when the player decides to hold the card
        /// </summary>
        private HoldIndicator _holdIndicator;
        /// <summary>
        /// The button object, used to decide if the card is being held or not
        /// </summary>
        private Button _button;

        /// <summary>
        /// The face side of the card
        /// </summary>
        private Sprite cardFace;
        
        /// <summary>
        /// The UI image that shows the card sprite.
        /// </summary>
        private Image cardImage;
        
        /// <summary>
        /// If the UI card is currently held
        /// </summary>
        public bool Held { get; private set; } = false;

        /// <summary>
        /// If the UI card is currently hidden (face-down)
        /// </summary>
        public bool Hidden { get; private set; } = false;

        /// <summary>
        /// Enable the clickable behavior on the card
        /// </summary>
        public void EnableInteract()
        {
            _button.interactable = true;
        }

        /// <summary>
        /// disable the clickable behavior on the card
        /// </summary>
        public void DisableInteract()
        {
            _button.interactable = false;
            Held = false;
            if (_holdIndicator != null) _holdIndicator.gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets the card to be hidden (showing the back)
        /// </summary>
        public void SetCardHidden()
        {
            Hidden = true;
            cardImage.sprite = cardBack;
        }

        /// <summary>
        /// Sets the card visible (showing the face)
        /// </summary>
        public void SetCardShown()
        {
            Hidden = false;
            cardImage.sprite = cardFace;
        }

        /// <summary>
        /// Behavior when the UICard is clicked, simply switch the held bool and heldindicator's rendering status.
        /// </summary>
        private void OnPressed()
        {
            Held = !Held;
            if (_holdIndicator != null) _holdIndicator.SetHeld(Held);
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _holdIndicator = GetComponentInChildren<HoldIndicator>(true);
            cardImage = GetComponentInChildren<Image>();
            cardFace = cardImage.sprite;
        }

        protected void OnEnable()
        {
            _button.onClick.AddListener(OnPressed);
            EnableInteract();
        }

        protected void OnDisable()
        {
            _button.onClick.RemoveListener(OnPressed);
            DisableInteract();
        }
    }
}