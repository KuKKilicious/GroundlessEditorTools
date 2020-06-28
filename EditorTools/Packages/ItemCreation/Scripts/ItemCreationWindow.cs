using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;

using Game.Base;

namespace Game.Editor
{

    public class ItemCreationWindow : OdinEditorWindow
    {
        [MenuItem("Game/ItemCreation")]
        private static void OpenWindow()
        {
            GetWindow<ItemCreationWindow>().Show();
        }
        
        [PropertyOrder(-10),HorizontalGroup("Top", 0.5f, MinWidth = 100, MaxWidth = 1000), LabelWidth(100)]
        [Button(ButtonSizes.Large,ButtonStyle.FoldoutButton, Expanded = true)]
        public void Search(string searchTerm)
        {
         
        }


        //TODO: Add Settings Button


        [HorizontalGroup("Top", 0.5f, MarginLeft = 0.1f, MinWidth = 100, MaxWidth = 1000)]
        [Button(ButtonSizes.Large)]
        public void AddNewItem()
        {

        }


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
        [PreviewField]
        [TableColumnWidth(64, Resizable = false)]
        public Texture icon;
        [TableColumnWidth(40)]
        public string name;

        [AssetsOnly]
        public string effects; //TODO: Use Effect Data

        [TextArea(2, 10)]
        public string description;

        [TableColumnWidth(40)]
        public string category; //TODO: Use category Enum 

        [TableColumnWidth(40)]
        public string rarity; //TODO: Use rarity Enum 

        [Button(ButtonSizes.Large)]
        [ResponsiveButtonGroup("Actions")]
        public void Save()
        {

        }

        [Button(ButtonSizes.Large)]
        [ResponsiveButtonGroup("Actions")]
        public void Revert()
        {

        }

    }
}