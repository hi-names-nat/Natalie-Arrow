using System.Collections.Generic;
using Game.Cards;
using UnityEngine;

namespace _Project.Core._Managers
{
    [CreateAssetMenu(fileName = "New Victory Condition", menuName = "Game/Victory Condition", order = 0)]
    public class BaseVictoryCondition : ScriptableObject
    {
        [SerializeField]
        private int payout;

        public virtual bool IsSatisfied(List<Card> cards)
        {
            return false;
        }
    }
}