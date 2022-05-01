using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Components;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        #region InputData

        public Vector3 CurrentMovementInput { get; set; }
        public bool RunNextFrame { get; set; }
        public bool InteractNextFrame { get; set; }

        #endregion

        #region CurrentMovementData

        public float CurrentSpeed { get;  set; }
        public float CurrentSpeedPercentage { get; set; }
        public Vector3 CurrentForward { get; set; }
        public Vector3 PreviousForward { get; set; }
        public Quaternion CurrentRotation { get;  set; }
        public Vector3 CurrentDeltaRotationEulers { get;  set; }
        
        
        #endregion

        public List<ItemEntityComponent> StackList = new List<ItemEntityComponent>();
        public PlayerEntityComponent PlayerEntity { get; set; }
        
    }
}