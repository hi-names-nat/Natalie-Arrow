using System;
using System.Collections;
using System.Collections.Generic;
using Game.Cards;
using Game.Victory;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private CardManager _cardManager;

        [SerializeField] private VictoryManager victoryManager;

        [SerializeField] private UnityEvent onReset;
        [SerializeField] private UnityEvent onBegin;
        [SerializeField] private UnityEvent onSceneEnter;
        [SerializeField] private UnityEvent onEnd;

        private void Awake()
        {
            _cardManager = new CardManager();
        }

        //todo replace with replace+reshuffle
        private void Reset()
        {
            _cardManager = new CardManager();
            onReset.Invoke();
        }

        private void Start()
        {
            onSceneEnter.Invoke();
        }

        void BeginRound()
        {
            onBegin.Invoke();
        }

        void EndRound()
        {
            onEnd.Invoke();
        }

        void NewRound()
        {
            
        }
    }

}