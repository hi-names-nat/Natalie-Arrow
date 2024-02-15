using System;
using System.Collections.Generic;
using _Project.Core._Managers;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class VictoryManager
    {

        [SerializeField] private List<BaseVictoryCondition> VictoryConditions;
    }
}