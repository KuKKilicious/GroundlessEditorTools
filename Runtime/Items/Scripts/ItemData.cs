using System.Collections.Generic;
using UnityEngine;
// ReSharper disable PublicField

namespace Game.Base
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class ItemData : ScriptableObject
    {
        public int Id;

        public Texture Icon;

        public string Name;

        public List<ItemEffect> Effects; 
        
        public string Description;

        public List<EffectExplanation> EffectExplanations;

        public ItemCategory Category; 

        public ItemRarity Rarity; 

        public bool Active;

    }
}
