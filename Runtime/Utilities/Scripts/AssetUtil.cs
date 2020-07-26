using System.Collections.Generic;
using System.Linq;
using Game.Base.Utilities;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Game.Base
{

    /// <summary>
    /// Utility class for saving loading assets from database
    /// </summary>
    public static class AssetUtil
    {
        private static string ITEM_SO_PATH = "Assets/Items/";
        private static string ITEM_SO_FOLDER_PATH = "Assets/Items";

        /// <summary>
        /// saves Item Scriptable Object via AssetDatabase in the project
        /// </summary>
        /// <param name="newItem">item to save</param>
        /// <returns>true if successful</returns>
        public static bool SaveAsset(ItemData newItem)
        {
            //format string
            string fileName = newItem.Name.ToTitleCase();
            fileName = fileName.Replace(" ", "");
            //check for min length
            if (fileName.Length <= 3)
            {
                Debug.LogWarning("ItemAsset Name not long enough");
                return false;
            }
            //check if Folder already exists
            if (!AssetDatabase.IsValidFolder(ITEM_SO_PATH + fileName))
            {
                //create new Folder
                AssetDatabase.CreateFolder(ITEM_SO_FOLDER_PATH, fileName);
            }
            AssetDatabase.CreateAsset(newItem, ITEM_SO_PATH + fileName + "/" + fileName + ".asset");
            AssetDatabase.SaveAssets();
            return true;
        }
        public static bool SaveAsset(ItemEffect newEffect,string itemName,string effectName)
        {
            //format string
            string fileName = itemName.ToTitleCase();
            fileName = fileName.Replace(" ", "");
            //check for min length
            if (fileName.Length <= 3)
            {
                Debug.LogWarning("Effect Name not long enough");
                return false;
            }
            //check if Folder already exists
            if (!AssetDatabase.IsValidFolder(ITEM_SO_PATH + fileName))
            {
                //create new Folder
                AssetDatabase.CreateFolder(ITEM_SO_FOLDER_PATH, fileName);
            }
            AssetDatabase.CreateAsset(newEffect, ITEM_SO_PATH + fileName + "/" +fileName.ToShortVersion()+"_"+ effectName + ".asset");
            AssetDatabase.SaveAssets();
            return true;
        }
        /// <summary>
        /// loads items from specified path via AssetDatabase from the project
        /// </summary>
        /// <returns>items, can be null</returns>
        [CanBeNull]
        public static ItemData[] LoadItemAssets()
        {
            List<ItemData> items = new List<ItemData>();
            try
            {
                string[] assetsGuid = AssetDatabase.FindAssets("t:ItemData", new[] { ITEM_SO_FOLDER_PATH });

                if (assetsGuid == null || assetsGuid.Length <= 0) { return null; } //return if folder doesn't contain items

                //Iterate over guids to get path and get ItemData from path and add each item to items list
                items.AddRange(assetsGuid.Select(t => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(t), typeof(ItemData)) as ItemData));
            }
            catch (System.Exception)
            {
                items = null;
            }
            return items?.ToArray();
        }

        public static void DeleteItemAsset(string fileName)
        {
            AssetDatabase.DeleteAsset(ITEM_SO_FOLDER_PATH + "/" + fileName + ".asset");
        }

        public static string GetItemFolderPath(string name)
        {
            var folderName = name.ToTitleCase();

            string path = "";
            if (!AssetDatabase.IsValidFolder(ITEM_SO_PATH + folderName)) { return null; }

            path = ITEM_SO_PATH + folderName;
            return path;
        }
    }
}
