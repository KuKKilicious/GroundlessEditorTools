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

        public string Effects; //TODO: Use Effect Data

        public string Description;

        public string Category; //TODO: Use category Enum 

        public string Rarity; //TODO: Use rarity Enum 
    }
}
