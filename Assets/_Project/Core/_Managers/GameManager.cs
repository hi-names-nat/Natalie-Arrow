/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/24
 * 
 * What: GameManager.cs
 * 
 * Function: Acts as the forward between the gametype and world itself.
 * Manages all managers and deals with gametype-agnostic things such as
 * betting.
 *
 ***********************************************************/

using _Project.Core._Managers;
using Game.Cards;
using Game.UI;
using Game.UI.Cards;
using Game.Victory;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game State")]
        [SerializeField] [Tooltip("The gametype to play")]
        private BaseGameType gameType;
        [SerializeField] [Tooltip("The victoryManager, Manages victory types and the such")]
        private VictoryManager victoryManager;
        [SerializeField] [Tooltip("The player's hand, responsible for managing the player's hand and associated UI")] 
        private Hand playerHand;
        [SerializeField] [Tooltip("The dealer's hand, responsible for managing the dealer's hand and associated UI. Only used in Blackjack.")]
        private Hand dealerHand;
        
        [Header("Events")] 
        [SerializeField] [Tooltip("Called when the game is hard reset. Use for UI elements.")]
        private UnityEvent onReset;
        [SerializeField] [Tooltip("Called when the game is first setup. Use for UI elements.")] 
        private UnityEvent onSceneEnter;
        
        /// <summary>
        /// The cardmanager, responsible for managing the deck of cards
        /// </summary>
        private CardManager _cardManager;
        /// <summary>
        /// The bet manager, responsible for managing the player's bets and payouts.
        /// </summary>
        private BetManager _betManager;
        /// <summary>
        /// The buttonsmanager, responsible for managing the player's non-card interactable elements
        /// </summary>
        private ButtonsManager _buttonsManager;
        /// <summary>
        /// The bettable, responsible for managing the bet table.
        /// </summary>
        private BetTable _betTable;

        /// <summary>
        /// Function called by Button Manager to increase bet
        /// </summary>
        public void IncreaseBet()
        {
            _betManager.IncreaseBet();
            
            _buttonsManager.SetDealClickable(_betManager.CanPlaceBet());
            if (_betTable) _betTable.ScrollPanel(false);

        }
        
        /// <summary>
        /// Function called by button manager to decrease bet
        /// </summary>
        public void DecreaseBet()
        {
            _betManager.DecreaseBet();
            
            _buttonsManager.SetDealClickable(_betManager.CanPlaceBet());
            if (_betTable) _betTable.ScrollPanel(true);
        }
        
        /// <summary>
        /// Function called by the button manager to continue the game.
        /// </summary>
        public void DealButtonPressed()
        {
            gameType.ContinueGameLoop(_cardManager, playerHand, dealerHand, victoryManager, _betManager, _buttonsManager);
        }

        /// <summary>
        /// Deals a single card to the player or dealer's hand. Used by the blackjack deal button.
        /// </summary>
        /// <param name="toDealer">should this card be dealt to the dealer? </param>
        public void DealCard(bool toDealer = false)
        {
            if (playerHand.Cards.Count == gameType.MaxPlayerHand) return;
            if (toDealer) dealerHand.AddCard(_cardManager.GetCard());
            else playerHand.AddCard(_cardManager.GetCard());
        }
        
        private void Reset()
        {
            _cardManager = new CardManager();
            onReset.Invoke();
        }

        private void Start()
        {
            onSceneEnter.Invoke();
            
            _betManager.SetStartingBank(gameType.startingBank);
        }
        
        private void Awake()
        {
            _cardManager = new CardManager();
            _betManager = GetComponentInChildren<BetManager>();
            _buttonsManager = GetComponentInChildren<ButtonsManager>();
            _betTable = GetComponentInChildren<BetTable>();
        }
    }
}