﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Base
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class ItemData : ScriptableObject
    {
        public Texture Icon;

        public string Name;

        public List<ItemEffect> Effects; 
        
        public string ShortDescription;
        
        public string FullDescription;

        public ItemCategory Category; 

        public ItemRarity Rarity; 

    }
}
