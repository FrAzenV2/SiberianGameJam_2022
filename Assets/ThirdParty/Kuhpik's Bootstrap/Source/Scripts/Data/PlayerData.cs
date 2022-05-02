using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;

namespace Kuhpik
{
    /// <summary>
    /// Used to store player's data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // [BoxGroup("level")] public int level;
        // [BoxGroup("currency")] public int money;

        public event Action MoneyChanged;
        public float Money
        {
            get => money;
            set
            {
                money = value;
                MoneyChanged?.Invoke();
            }
        }
        [SerializeField] private float money = 0;
    }
}