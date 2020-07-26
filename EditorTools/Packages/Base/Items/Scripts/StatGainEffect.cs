using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable PublicField
namespace Game.Base
{

    [System.Serializable]
    public class StatGainEffect : ItemEffect
    {
        public StatType Stat;
        public bool IsMultiple;
        public float Value;
    }
}
