using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;

using Game.Base;
using Game.Base.Utilities;
using System.Linq;
using System.ComponentModel;

namespace Game.Editor
{

    public enum SortType
    {
        Name, Category, Rarity
    }
    public class ItemCreationWindow : OdinEditorWindow
    {
        private SortType currentSortType;
        private bool isSorted;


        [MenuItem("Game/ItemCreation")]
        private static void OpenWindow()
        {
            GetWindow<ItemCreationWindow>().Show();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (EditorUtility.DisplayDialog("Save", "Do you want to save?", "Yes", "No"))
            {
                SaveAll();
            }
        }
        [PropertyOrder(-10), HorizontalGroup("Top", 0.4f, MinWidth = 100, MaxWidth = 1000, LabelWidth = 100)]
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton, Expanded = true)]
        public void Search(string searchTerm)
        {

        }


        [HorizontalGroup("Top", 0.4f, MarginLeft = 0.1f, MinWidth = 150, MaxWidth = 1000, LabelWidth = 150)]
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton, Expanded = true)]
        public void AddNewItem(string itemName)
        {
            if (itemName == null || itemName.Length < 4)
            {
                Debug.LogWarning("Enter an item name that is longer than 3 characters before trying adding an item");
                return;
            }

            //Create SO
            ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
            newItem.Name = itemName;




            if (!AssetUtil.SaveAsset(newItem)) { return; }; //return if unsuccessful
            //Create ViewData

            itemTable.Add(new ItemTableViewData(itemName));
        }



        //TODO: replace with settings icon
        [HorizontalGroup("Top", 0.1f, MarginLeft = 0.1f, MinWidth = 100, MaxWidth = 300)]
        [Button(ButtonSizes.Small)]
        public void Settings() { }

        [HorizontalGroup("Top", 0.0f, MinWidth = 100, MaxWidth = 3100, LabelWidth = 100)]
        [Button(ButtonSizes.Small, ButtonStyle.FoldoutButton, Expanded = true)]
        public void Sort(SortType sortType)
        {
            switch (sortType)
            {
                case (SortType.Name):
                {
                    itemTable = itemTable.OrderBy(o => o.Name).ToList();

                    break;
                }
                case (SortType.Category):
                {
                    itemTable = itemTable.OrderBy(o => o.Category).ToList();
                    break;
                }
                case (SortType.Rarity):
                {
                    itemTable = itemTable.OrderBy(o => o.Rarity).ToList();
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortType), sortType, null);
            }
        }

        [PropertyOrder(10)]
        [HorizontalGroup("Bottom")]
        [Button(ButtonSizes.Large)]
        public void SaveAll()
        {
            foreach (var item in itemTable)
            {
                item.Save();
            }
        }

        [PropertyOrder(10)]
        [HorizontalGroup("Bottom")]
        [Button(ButtonSizes.Large)]
        public void LoadAll()
        {
            //TODO: Check if sure?
            var toClear = EditorUtility.DisplayDialog("Confirmation", "Are you sure to load all?", "Yes", "No");
            if (!toClear)
                return;
            itemTable.Clear();
            
            ItemData[] items = AssetUtil.LoadItemAssets();

            if (items == null || items.Length <= 0) { return; } //return if null or empty

            foreach (var item in items)
            {
                itemTable.Add(new ItemTableViewData(item.name.ToSentenceCase(), item.Icon, item.Effects, item.Description, item.Category, item.Rarity));
            }

        }

        [PropertyOrder(10)]
        [HorizontalGroup("Bottom")]
        [Button(ButtonSizes.Large)]
        public void Clear()
        {
            var toClear = EditorUtility.DisplayDialog("Confirmation", "Are you sure to clear list?", "Yes", "No");
            if (!toClear)
                return;

            itemTable.Clear();
        }

        [PropertyOrder(-1)]
        [TableList(AlwaysExpanded = true, MinScrollViewHeight = 1000, HideToolbar = true)]
        [SerializeField]
        private List<ItemTableViewData> itemTable = new List<ItemTableViewData>();

    }


    [System.Serializable]
    public class ItemTableViewData
    {

        public ItemTableViewData(string name)
        {
            this.Name = name;
        }
        public ItemTableViewData(string name, Texture icon, List<ItemEffect> effects, string description, ItemCategory category, ItemRarity rarity)
        {
            this.Name = name;
            this.oldName = name;
            this.Icon = icon;
            this.Effects = effects;
            this.Description = description;
            this.Category = category;
            this.Rarity = rarity;
        }
        [PreviewField]
        [TableColumnWidth(64, Resizable = false)]
        [PropertyOrder(0)]
        public Texture Icon;

        [TableColumnWidth(120)] 
        [PropertyOrder(1)]
        public string Name;
        
        [AssetsOnly]
        [TableList(AlwaysExpanded = true, MinScrollViewHeight = 1000, HideToolbar = true)]//TODO: Remove the X in the list OR make X also delete Scriptable Object 
        [TableColumnWidth(500)]
        [InlineEditor]
        [PropertyOrder(2)]
        public List<ItemEffect> Effects;

        [Button(ButtonSizes.Medium,Name ="Details")]
        [TableColumnWidth(60,Resizable = false)]
        [PropertyOrder(3)]
        public void Details()
        {
            EffectCreationWindow.Item = this;
            EffectCreationWindow.OpenWindow(this);
        }

        [TableColumnWidth(300)]
        [TextArea(5, 10)]
        [PropertyOrder(5)]
        public string Description;

        [PropertyOrder(10)]
        [TableColumnWidth(100,Resizable = false)]
        public ItemCategory Category; 
        [PropertyOrder(11)]
        [TableColumnWidth(100,Resizable = false)]
        public ItemRarity Rarity;

        [Button(ButtonSizes.Medium)]
        [TableColumnWidth(60,Resizable = false)]
        [PropertyOrder(15)]
        [ResponsiveButtonGroup("Actions")]
        public void Save()
        {
            //Create SO
            ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
            //if new name, delete old asset
            if (!oldName.Equals(Name))
            {
                //TODO: Delete Asset
                AssetUtil.DeleteItemAsset(oldName.ToTitleCase());
                oldName = Name;
            }
            newItem.Name = Name;
            newItem.Icon = Icon;
            //newItem.Effects = Effects;
            newItem.Description = Description;
            newItem.Category = Category;
            newItem.Rarity = Rarity;
            AssetUtil.SaveAsset(newItem);

        }

//         [Button(ButtonSizes.Large)]
//         [ResponsiveButtonGroup("Actions")]
//         public void Revert()
//         {
//             
//         }

        private string oldName = "";
    }


}