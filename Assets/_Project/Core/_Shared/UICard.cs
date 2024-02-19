/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: #DATE#
 * 
 * What: UICard.cs
 * 
 * Function: ...
 *
 * TODO: ...
 *
 * Say thank you on the way out!
 * 
 ***********************************************************/

using System;
using Game.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Cards
{
    [RequireComponent(typeof(Button))]
    public class UICard : MonoBehaviour
    {
        [SerializeField] public Card cardDefinition;
        
        private HoldIndicator _holdIndicator;
        private Button _button;
        public bool Held { get; private set; } = false;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        protected void OnEnable()
        {
            _button.onClick.AddListener(OnPressed);
            if (_holdIndicator != null) _holdIndicator.gameObject.SetActive(Held);
        }

        protected void OnDisable()
        {
            _button.onClick.RemoveListener(OnPressed);

        }

        public void EnableInteract()
        {
            _button.interactable = true;
        }

        public void DisableInteract()
        {
            _button.interactable = false;
        }

        private void OnPressed()
        {
            Held = !Held;
            if (_holdIndicator != null) _holdIndicator.SetHeld(Held);
        }
    }
}