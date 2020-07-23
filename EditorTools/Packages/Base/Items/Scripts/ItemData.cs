using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Base
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class ItemData : ScriptableObject
    {
        public Texture Icon;

        public string Name;

        public List<ItemEffect> Effects; //TODO: Use Effect Data

        public string Description;

        public ItemCategory Category; //TODO: Use category Enum 

        public ItemRarity Rarity; //TODO: Use rarity Enum 
    }
}
