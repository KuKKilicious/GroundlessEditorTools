#if UNITY_EDITOR
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
using Game.Settings;

namespace Game.Editor
{
    public enum SortType
    {
        ID, Name, Category, Rarity
    }
    /// <summary>
    /// Window to create Items
    /// </summary>
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
            if (GroundlessSettings.GetOrCreateSettings().ConfirmWhenLoadingOrQuitting)
            {
                if (EditorUtility.DisplayDialog("Save", "Do you want to save?", "Yes", "No"))
                {
                    SaveAll();
                }
            }
        }

        [PropertyOrder(-10), HorizontalGroup("Top", 0.4f, MinWidth = 100, MaxWidth = 1000, LabelWidth = 100)]
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton, Expanded = true)]
        public void Search(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            List<ItemTableViewData> searchResults = new List<ItemTableViewData>();
            foreach (var item in itemTable)
            {
                if (item.Name.ToLower().Contains(searchTerm)
                    || item.Description.ToLower().Contains(searchTerm)
                    || item.Category.ToString().ToLower().Contains(searchTerm)
                    || item.Rarity.ToString().ToLower().Contains(searchTerm)
                )
                {
                    searchResults.Add(item);
                }
            }

            if (searchResults.Count <= 0)
            {
                EditorUtility.DisplayDialog("Search", "No search results for given search term", "OK");
            }
            itemTable = searchResults;
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
            newItem.Id = AssetUtil.GetNextId();


            if (!AssetUtil.SaveAsset(newItem)) { return; }; //return if unsuccessful
            //Create ViewData

            itemTable.Add(new ItemTableViewData(itemName, newItem.Id));
        }



        //TODO: replace with settings icon
        [HorizontalGroup("Top", 0.1f, MarginLeft = 0.1f, MinWidth = 100, MaxWidth = 300)]
        [Button(ButtonSizes.Small)]
        public void Settings()
        {
            EditorUtility.DisplayDialog("Not implemented yet", "Go to Edit > Project Settings > Groundless to edit settings.", "OK");
        }
        //TODO: Add Sort by index and active passive
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
                case SortType.ID:
                    itemTable = itemTable.OrderBy(o => o.Id).ToList();
                    break;
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

            
            itemTable.Clear();

            ItemData[] items = AssetUtil.LoadItemAssets();

            if (items == null || items.Length <= 0) { return; } //return if null or empty

            foreach (var item in items)
            {
                itemTable.Add(new ItemTableViewData(item.Id, item.name.ToSentenceCase(), item.Icon, item.Effects, item.Description, item.Category, item.Rarity, item.Active));
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

        protected override void OnGUI()
        {
            base.OnGUI();
        }
    }

    /// <summary>
    /// Item Data that is visualized in Window
    /// </summary>
    [System.Serializable]
    public class ItemTableViewData
    {
        public ItemTableViewData(string name, int id)
        {
            this.Name = name;
            this.Id = id;
            this.oldName = Name;
        }
        public ItemTableViewData(int id, string name, Sprite icon, List<ItemEffect> effects, string description, ItemCategory category, ItemRarity rarity, bool active)
        {
            this.Id = id;
            this.Name = name;
            this.oldName = Name;
            this.Icon = icon;
            this.Effects = effects;
            this.Description = description;
            this.Category = category;
            this.Rarity = rarity;
            this.Active = active;
        }


        [PreviewField]
        [TableColumnWidth(64, Resizable = false)]
        [PropertyOrder(0)]
        [OnValueChanged("SetDirty")]
        public Sprite Icon;

        [TableColumnWidth(30, Resizable = false)]
        [PropertyOrder(13)]
        [ReadOnly]
        [OnValueChanged("SetDirty")]
        public int Id;

        [TableColumnWidth(120)]
        [PropertyOrder(1)]
        [OnValueChanged("SetDirty")]
        public string Name;

        [AssetsOnly]
        [TableList(AlwaysExpanded = true, MinScrollViewHeight = 1000, HideToolbar = true)]//TODO: Remove the X in the list OR make X also delete Scriptable Object 
        [TableColumnWidth(500)]
        [InlineEditor]
        [PropertyOrder(2)]
        [OnValueChanged("SetDirty")]

        public List<ItemEffect> Effects;

        [Button(ButtonSizes.Medium, Name = "Details")]
        [TableColumnWidth(60, Resizable = false)]
        [PropertyOrder(3)]
        public void Details()
        {
            EffectCreationWindow.Item = this;
            EffectCreationWindow.OpenWindow(this);
        }
        [TableColumnWidth(150)]
        [TextArea(2, 5)]
        [PropertyOrder(4)]
        [OnValueChanged("SetDirty")]

        public string Description;

        [PropertyOrder(10)]
        [TableColumnWidth(100, Resizable = false)]
        [OnValueChanged("SetDirty")]

        public ItemCategory Category;
        [PropertyOrder(11)]
        [TableColumnWidth(100, Resizable = false)]
        [OnValueChanged("SetDirty")]

        public ItemRarity Rarity;

        [PropertyOrder(12)]
        [TableColumnWidth(40, Resizable = false)]
        [OnValueChanged("SetDirty")]

        public bool Active;

        [Button(ButtonSizes.Medium)]
        [TableColumnWidth(60, Resizable = false)]
        [PropertyOrder(15)]
        [ResponsiveButtonGroup("Actions")]
        public void Save()
        {
            if (!dirty) { return; }
            //Create SO
            ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
            //if new name, delete old asset
            if (!oldName.Equals(Name))
            {
                //TODO: Delete Asset
                AssetUtil.ReplaceFolder(Name, oldName);
                AssetUtil.RenameAsset(Name, oldName);
                oldName = Name;
            }
            newItem.Id = Id;
            newItem.Name = Name;
            newItem.Icon = Icon;
            newItem.Effects = Effects;
            newItem.Description = Description;
            newItem.Category = Category;
            newItem.Rarity = Rarity;
            newItem.Active = Active;
            AssetUtil.SaveAsset(newItem);

            dirty = false;

        }

        [Button(ButtonSizes.Medium)]
        [TableColumnWidth(60, Resizable = false)]
        [PropertyOrder(20)]
        [ResponsiveButtonGroup("Delete")]
        public void Delete()
        {
            //Check if sure
            EditorUtility.DisplayDialog("Not implemented yet", "Please delete the assets you don't require anymore within Unity", "OK");
        }

        private string oldName = "";
        private bool dirty = false;

        private void SetDirty()
        {
            dirty = true;
        }
    }


}
#endif