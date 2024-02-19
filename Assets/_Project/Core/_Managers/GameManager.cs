using System;
using System.Collections;
using System.Collections.Generic;
using Game.Cards;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Victory
{
    public class GameManager : MonoBehaviour
    {
        private CardManager _cardManager;

        [SerializeField] private VictoryManager victoryManager;
        
        private void Awake()
        {
            _cardManager = new CardManager();
        }

        //todo replace with replace+reshuffle
        private void Reset()
        {
            _cardManager = new CardManager();
        }

        void BeginRound()
        {
            
        }

        void EndRound()
        {
            
        }

        void NewRound()
        {
            
        }
    }

}