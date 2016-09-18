using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Gamelogic.Map;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Gamelogic.Agents
{
    class Pathfinder : MonoBehaviour
    {
        private HexCoordinates CurrentHexCoordinates;
        private HexCoordinates TargetHexCoordinates;

        void Awake()
        {
        }

        [UsedImplicitly]
        void Update()
        {
            //if (CurrentHexCoordinates == TargetHexCoordinates)
            //{
                
            //}
        }
    }
}
