using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Core._Managers;
using Game.Cards;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
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