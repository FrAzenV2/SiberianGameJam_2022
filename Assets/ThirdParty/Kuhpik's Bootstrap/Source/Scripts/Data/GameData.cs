using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Components;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        #region InputData

        public Vector2 CurrentMovementInput { get; set; }
        public bool RunNextFrame { get; set; }
        public bool InteractNextFrame { get; set; }

        #endregion
        
        public PlayerEnitityComponent PlayerEntity { get; set; }
    }
}