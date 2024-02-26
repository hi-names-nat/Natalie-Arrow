/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/24
 * 
 * What: PokerGameType.cs
 * 
 * Function: Responsible for upkeep and management of the Poker Gametype.
 * All game upkeep is looped through here.
 *
 * 
 ***********************************************************/

using System;
using _Project.Core._Managers;
using Game.Cards;
using Game.UI;
using Game.Victory;
using UnityEngine;

namespace Game.Poker
{
    /// <summary>
    /// An enum representing gamestate.
    /// </summary>
    internal enum State
    {
        Entry,
        Redeal,
        End,
        Reset
    }

    [Serializable]
    public struct PokerSettings
    {
        public bool wildDeuces;
    }
    
    [CreateAssetMenu(fileName = "New Poker Gametype", menuName = "Game/GameType/Poker", order = 0)]
    public class PokerGameType : BaseGameType
    {
        /// <summary>
        /// The max hand size of the game. Default is 5.
        /// </summary>
        private static readonly uint HandSize = 5;
        
        /// <summary>
        /// The current state of the game. Enters on Entry, obviously.
        /// </summary>
        private State _state = State.Entry;

        [SerializeField] private PokerSettings fineSettings;

        /// <summary>
        /// The main state manager. Enables/Disables components and functionality as the game progresses. 
        /// </summary>
        /// <param name="cardManager">the Cardmanager, passed from GameManager</param>
        /// <param name="handManager">the Player's Hand, passed from GameManager</param>
        /// <param name="_">Dealer's hand. Unused here.</param>
        /// <param name="victoryManager">the victory manager, passed from GameManager</param>
        /// <param name="betManager">the bet manager, passed from GameManager</param>
        /// <param name="buttonsManager">the button manager, passed from GameManager</param>
        /// <exception cref="ArgumentOutOfRangeException">If the state somehow becomes something that is
        /// undefined, this is called.</exception>
        public override void ContinueGameLoop(
            CardManager cardManager, Hand handManager, Hand _, 
            VictoryManager victoryManager, BetManager betManager, ButtonsManager buttonsManager)
        {
            switch (_state)
            {
                case State.Entry:
                    InitialDeal(cardManager, handManager, betManager, buttonsManager, victoryManager);
                    _state = State.Redeal;
                    break;
                case State.Redeal:
                    ReDeal(cardManager, handManager, victoryManager, betManager);
                    _state = State.End;
                    break;
                case State.End:
                    EndGame(handManager, victoryManager, betManager);
                    _state = State.Reset;
                    break;
                case State.Reset:
                    NewRound(cardManager, handManager, buttonsManager, betManager);
                    _state = State.Entry;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Does everything related to initially dealing and enabling holding on the cards.
        /// </summary>
        /// <param name="cardManager">The given Cardmanager to get the cards from.</param>
        /// <param name="handManager">The hand to add the dealt cards to.</param>
        /// <param name="betManager">The bet manager, to lock in a bet</param>
        /// <param name="buttonsManager">The buttons manager, to disable changing the bet after it's set.</param>
        /// <param name="victoryManager">The victory manager, to do card state victory checking.</param>
        private void InitialDeal(
            CardManager cardManager, Hand handManager, BetManager betManager, ButtonsManager buttonsManager, VictoryManager victoryManager)
        {
            var cards = cardManager.GetCards(HandSize);
            handManager.AddCards(cards);
            betManager.PlaceBet();
            buttonsManager.DisableBetButtons();
            
            //Enable card holding
            handManager.EnableUiCardInteraction();

            //Display the current state of the hand, if it would win.
            var currentstate = victoryManager.FindIfWon(handManager.Cards, fineSettings);
            if (currentstate != null) betManager.UpdateHandState(currentstate.victoryName);
        }

        /// <summary>
        /// Does everything for Redealing the cards after selecting holds.
        /// </summary>
        /// <param name="cardManager">The card manager to replace/ redeal cards from</param>
        /// <param name="handManager">The hand to remove / add new cards to</param>
        /// <param name="victoryManager">The victory manager, to check if the player's hand is a winning hand</param>
        /// <param name="betManager">The bet manager, to update the hand state HUD in the case that there's a winning hand</param>
        private void ReDeal(
            CardManager cardManager, Hand handManager, VictoryManager victoryManager, BetManager betManager)
        {
            var cards = handManager.RemoveUnheldCards();
            //Replace cards back into the cardManager
            cardManager.ReturnCards(cards);
            //Add newly dealt cards
            handManager.AddCards(cardManager.GetCards((uint)cards.Count));
            
            //Disable card holding
            handManager.DisableUiCardInteraction();
                        
            //Display the current state of the hand, if it would win, or hide if none.
            var currentstate = victoryManager.FindIfWon(handManager.Cards, fineSettings);
            if (currentstate != null) betManager.UpdateHandState(currentstate.victoryName);
            else betManager.HideHandState();

        }

        /// <summary>
        /// Does everything related to finding the victory condition and paying out
        /// </summary>
        /// <param name="handManager">The player's hand</param>
        /// <param name="victoryManager">The victorymanager to get the victory conditions from</param>
        /// <param name="betManager">The bet manager, to display both end game text and update the bank</param>
        private void EndGame(
            Hand handManager, VictoryManager victoryManager, BetManager betManager)
        {
            //Get if the player has a winning hand. Returns null if they do not.
            var victoryCondition = victoryManager.FindIfWon(handManager.Cards, fineSettings);
            if (victoryCondition != null)
            {
                //Different behaviors for if it was a jackpot (max value, max bet) or not.
                if (victoryCondition.isJackpot) betManager.UpdateBank(victoryCondition.jackpotPayout, true);
                else betManager.UpdateBank(victoryCondition.payout);
                
                betManager.ShowEndMessage(victoryCondition.victoryName);
            }
            else
            {
                //Show that the player did not win
                betManager.ShowEndMessage("No Win");
            }
        }

        /// <summary>
        /// Does everything related to starting a new round.
        /// </summary>
        /// <param name="cardManager">The card manager</param>
        /// <param name="handManager">The Player's hand</param>
        /// <param name="buttonsManager">The player's UI button manager</param>
        /// <param name="betManager">The betmanager</param>
        private static void NewRound(
            CardManager cardManager, Hand handManager, ButtonsManager buttonsManager, BetManager betManager)
        {
            //Return all cards in the players' hand.
            cardManager.ReturnCards(handManager.Cards);
            
            //Clears and resets the handManager
            handManager.Reset();
            
            //Reshuffles the deck
            cardManager.ShuffleDeck();
            buttonsManager.EnableBetButtons();
            
            //Sets if the deal button is enabled if the player has enough money remaining to place the bet
            buttonsManager.SetDealClickable(betManager.CanPlaceBet());
            
            //Hide the End of round message.
            betManager.HideEndMessage();
            
            //Hide hand state, as no hand.
            betManager.HideHandState();
        }

        public override void Reset()
        {
            _state = State.Entry;
        }
    }
}