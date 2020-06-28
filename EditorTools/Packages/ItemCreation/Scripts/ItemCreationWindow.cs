using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;

using Game.Base;
using Game.Base.Utilities;

namespace Game.Editor
{
    public class ItemCreationWindow : OdinEditorWindow
    {
        [MenuItem("Game/ItemCreation")]
        private static void OpenWindow()
        {
            GetWindow<ItemCreationWindow>().Show();
        }

        [PropertyOrder(-10), HorizontalGroup("Top", 0.4f, MinWidth = 100, MaxWidth = 1000), LabelWidth(100)]
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton, Expanded = true)]
        public void Search(string searchTerm)
        {

        }


        [HorizontalGroup("Top", 0.4f, MarginLeft = 0.1f, MinWidth = 150, MaxWidth = 1000), LabelWidth(150)]
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton, Expanded = true)]
        public void AddNewItem(string itemName)
        {


            //Create SO
            ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
            newItem.Name = itemName;


           

            AssetUtil.SaveItemAsset(newItem);
            //Create ViewData

            itemTable.Add(new ItemTableViewData(itemName));
        }

    

        //TODO: replace with settings icon
        [HorizontalGroup("Top", 0.1f, MarginLeft = 0.1f, MinWidth = 100, MaxWidth = 1000)]
        [Button(ButtonSizes.Small)]
        public void Settings() { }

        [PropertyOrder(10)]
        [HorizontalGroup("Bottom")]
        [Button(ButtonSizes.Large)]
        public void SaveAll()
        {

        }

        [PropertyOrder(10)]
        [HorizontalGroup("Bottom")]
        [Button(ButtonSizes.Large)]
        public void LoadAll()
        {
            //Check if sure?
        }

        [PropertyOrder(10)]
        [HorizontalGroup("Bottom")]
        [Button(ButtonSizes.Large)]
        public void Clear()
        {

        }

        [PropertyOrder(-1)]
        [TableList(AlwaysExpanded = true, MinScrollViewHeight = 1000)]
        public List<ItemTableViewData> itemTable = new List<ItemTableViewData>(); //TODO: Figure out how to sort table columns

        //         [OnInspectorGUI]
        //         public void OnInspectorGUIUpdate()
        //         {
        //             Debug.Log("");
        // 
        //         }


       
    }



    [System.Serializable]
    public class ItemTableViewData
    {

        public ItemTableViewData(string name)
        {
            this.Name = name;
        }

        [PreviewField]
        [TableColumnWidth(64, Resizable = false)]
        public Texture Icon;

        [TableColumnWidth(40)] //TODO: set dirty /w OnValueChanged to apply new fileName when saving
        public string Name;

        [AssetsOnly]
        public string Effects; //TODO: Use Effect Data

        [TextArea(2, 10)]
        public string Description;

        [TableColumnWidth(40)]
        public string Category; //TODO: Use category Enum 

        [TableColumnWidth(40)]
        public string Rarity; //TODO: Use rarity Enum 

        [Button(ButtonSizes.Large)]
        [ResponsiveButtonGroup("Actions")]
        public void Save()
        {
            //Create SO
            ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
            newItem.Name = Name;
            newItem.Icon = Icon;
            newItem.Effects = Effects;
            newItem.Description= Description;
            newItem.Category= Category;
            newItem.Rarity= Rarity;
            AssetUtil.SaveItemAsset(newItem);

        }

        [Button(ButtonSizes.Large)]
        [ResponsiveButtonGroup("Actions")]
        public void Revert()
        {

        }

    }
}