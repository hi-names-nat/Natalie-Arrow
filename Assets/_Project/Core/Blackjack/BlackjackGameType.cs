/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/23
 *
 * What: BaseGameType.cs
 *
 * Function: The gametype that drives the blackjack game mode.
 * Does not use the victory manager, instead all behavior is
 * written natively.
 *
 ***********************************************************/
using System;
using _Project.Core._Managers;
using Game.Cards;
using Game.UI;
using Game.Victory;
using UnityEngine;

namespace Game.Blackjack
{
    internal enum State
    {
        Entry,
        Player,
        End,
        Reset,
    }
    
    [CreateAssetMenu(fileName = "New BlackJack Gametype", menuName = "Game/GameType/BlackJack", order = 0)]
    public class BlackjackGameType : BaseGameType
    {
        [SerializeField] [Tooltip("the score that the player & dealers want to hit")] 
        private int goalScore = 21;

        [SerializeField] [Tooltip("The payout when the player wins, multiplied by their bet down.")] 
        private int payout;

        [SerializeField] [Tooltip("The payout when the player hits a blackjack. Not multiplied.")] 
        private int jackpotPayout;
        
        
        /// <summary>
        /// The current state of the game. Enters on Entry, obviously.
        /// </summary>
        private State _state = State.Entry;
        
        public override void ContinueGameLoop(CardManager cardManager, Hand playerHand, Hand dealerHand, VictoryManager _, BetManager betManager,
            ButtonsManager buttonsManager)
        {
            switch (_state)
            {
                case State.Entry:
                    InitialDeal(cardManager, playerHand, dealerHand, betManager, buttonsManager);
                    _state = State.Player;
                    break;
                case State.Player:
                    DealerCompute(cardManager, dealerHand, buttonsManager);
                    _state = State.End;
                    break;
                case State.End:
                    EndGame(playerHand, dealerHand, betManager);
                    _state = State.Reset;
                    break;
                case State.Reset:
                    NewRound(cardManager, playerHand, dealerHand, buttonsManager, betManager);
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
        /// <param name="playerHand">The player's hand to add the dealt cards to.</param>
        /// <param name="dealerHand">The dealer's hand to add dealt cards to.</param>
        /// <param name="betManager">The bet manager, to lock in a bet</param>
        /// <param name="buttonsManager">The buttons manager, to disable changing the bet after it's set.</param>
        private void InitialDeal(
            CardManager cardManager, Hand playerHand, Hand dealerHand, BetManager betManager, ButtonsManager buttonsManager)
        {
            var cards = cardManager.GetCards(4);
            playerHand.AddCards(cards.GetRange(0,2), false);
            dealerHand.AddCardHidden(cards[3], false);
            dealerHand.AddCard(cards[2], false);
            betManager.PlaceBet();
            buttonsManager.DisableBetButtons();
            
            //Enable buttons for hit and stay
            buttonsManager.SetBlackJackDealEnabled(true);
            
            buttonsManager.SetButtonText("Stay", UIButtons.Deal);
        }
        
        /// <summary>
        /// Plays the Dealer
        /// </summary>
        /// <param name="cardManager">The card manager to replace/ redeal cards from</param>
        /// <param name="dealerHand">The hand to remove / add new cards to</param>
        private void DealerCompute(
            CardManager cardManager, Hand dealerHand, ButtonsManager buttonsManager)
        {
            buttonsManager.SetBlackJackDealEnabled(false);
            dealerHand.UnhideHand();
            while (BlackJackImplementations.GetHandValue(dealerHand.Cards, goalScore, true) <= 16)
            {
                dealerHand.AddCard(cardManager.GetCard());
            }
        }

        /// <summary>
        /// Does everything related to finding the victory condition and paying out
        /// </summary>
        /// <param name="playerHand">The player's hand</param>
        /// <param name="dealerHand">The dealer's hand</param>
        /// <param name="betManager">The bet manager, to display both end game text and update the bank</param>
        private void EndGame(
            Hand playerHand, Hand dealerHand, BetManager betManager)
        {
            var dealerValue = BlackJackImplementations.GetHandValue(dealerHand.Cards, goalScore, true);
            var playerValue = BlackJackImplementations.GetHandValue(playerHand.Cards, goalScore, false);
            
            if (goalScore - playerValue < 0 || dealerValue == goalScore)
            {
                betManager.ShowEndMessage("Dealer wins");
                return;
            }
            
            if (playerValue == goalScore)
            {
                betManager.ShowEndMessage("Player Wins:  Jackpot");
                betManager.UpdateBank(jackpotPayout, true);
                return;
            }

            if (goalScore-dealerValue < goalScore && goalScore-dealerValue > 0)
            {
                betManager.ShowEndMessage("Dealer wins");
                return;
            }
            
            betManager.ShowEndMessage("Player Wins");
            betManager.UpdateBank(payout, false);
        }

        /// <summary>
        /// Does everything related to starting a new round.
        /// </summary>
        /// <param name="cardManager">The card manager</param>
        /// <param name="playerHand">The player's hand</param>
        /// <param name="dealerHand">The dealer's hand</param>
        /// <param name="buttonsManager">The player's UI button manager</param>
        /// <param name="betManager">The betmanager</param>
        private void NewRound(
            CardManager cardManager, Hand playerHand, Hand dealerHand, ButtonsManager buttonsManager, BetManager betManager)
        {
            //Return all cards in the players' hand.
            cardManager.ReturnCards(playerHand.Cards);
            cardManager.ReturnCards(dealerHand.Cards);
            
            //Clears and resets the hands
            playerHand.Reset();
            dealerHand.Reset();
            
            //Reshuffles the deck
            cardManager.ShuffleDeck();
            buttonsManager.EnableBetButtons();
            
            //Sets if the deal button is enabled if the player has enough money remaining to place the bet
            buttonsManager.SetDealClickable(betManager.CanPlaceBet());
            
            //Hide the End of round message.
            betManager.HideEndMessage();
            
            buttonsManager.SetButtonText("Deal", UIButtons.Deal);
            buttonsManager.SetBlackJackDealEnabled(false);
        }
    }
}