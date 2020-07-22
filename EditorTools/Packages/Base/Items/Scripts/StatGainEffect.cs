using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable PublicField
namespace Game.Base
{

    [System.Serializable]
    [CreateAssetMenu()] //TODO: Fix CreateAssetMenu() not taking any arguments
    public class StatGainEffect : ItemEffect
    {
        public bool IsMultiple;
        public float Value;
    }
}
