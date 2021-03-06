using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Components;
using Common.Scripts.Enums;

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

        #region StackData

        public readonly Dictionary<ItemType, int> StackItems = new Dictionary<ItemType, int>();

        public readonly List<ItemEntityComponent> StackList = new List<ItemEntityComponent>();
        public int CurrentStackMax => DefaultStackMax + AdditionalStacks;
        public int DefaultStackMax { get; set; }
        public int AdditionalStacks { get; set; }

        #endregion
        
        public bool IsDirty { get; set; }

        public List<ChaserEntityComponent> ActiveChasers { get; set; } = new List<ChaserEntityComponent>();

        public PlayerEntityComponent PlayerEntity { get; set; }
        
    }
}