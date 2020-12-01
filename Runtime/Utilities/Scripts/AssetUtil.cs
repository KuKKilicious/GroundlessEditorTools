#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Base.Utilities;
using Game.Settings;
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

        /// <summary>
        /// saves Item Scriptable Object via AssetDatabase in the project
        /// </summary>
        /// <param name="newItem">item to save</param>
        /// <returns>true if successful</returns>
        public static bool SaveAsset(ItemData newItem)
        {
            string itemPath = GroundlessSettings.GetOrCreateSettings().ItemPath;
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
            if (!AssetDatabase.IsValidFolder(itemPath + "/" + fileName))
            {
                //create new Folder
                AssetDatabase.CreateFolder(itemPath, fileName);
            }
            AssetDatabase.CreateAsset(newItem, itemPath + "/" + fileName + "/" + fileName + ".asset");
            UnityEditor.VersionControl.Provider.Checkout(itemPath + "/" + fileName + "/" + fileName + ".asset"
                , UnityEditor.VersionControl.CheckoutMode.Both);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return true;
        }

        public static bool SaveAsset(ItemEffect newEffect, string itemName, string effectName)
        {
            string itemPath = GroundlessSettings.GetOrCreateSettings().ItemPath;
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
            if (!AssetDatabase.IsValidFolder(itemPath + "/" + fileName))
            {
                //create new Folder
                AssetDatabase.CreateFolder(itemPath, fileName);
            }
            AssetDatabase.CreateAsset(newEffect, itemPath + "/" + fileName + "/" + fileName.ToShortVersion() + "_" + effectName + ".asset");
            UnityEditor.VersionControl.Provider.Checkout(itemPath + "/" + fileName + "/" + fileName + ".asset"
                , UnityEditor.VersionControl.CheckoutMode.Both);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return true;
        }
        /// <summary>
        /// loads items from specified path via AssetDatabase from the project
        /// </summary>
        /// <returns>items, can be null</returns>
        [CanBeNull]
        public static ItemData[] LoadItemAssets()
        {
            string itemPath = GroundlessSettings.GetOrCreateSettings().ItemPath;
            List<ItemData> items = new List<ItemData>();
            try
            {
                string[] assetsGuid = AssetDatabase.FindAssets("t:ItemData", new[] { itemPath });

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

        
        public static void ReplaceFolder(string name, string oldName)
        {
            string itemPath = GroundlessSettings.GetOrCreateSettings().ItemPath;
            FileUtil.ReplaceDirectory(GetItemFolderPath(oldName), itemPath + "/" + name.ToTitleCase());
            FileUtil.DeleteFileOrDirectory(GetItemFolderPath(oldName));
            AssetDatabase.Refresh();
        }

        public static void RenameAsset(string name, string oldName)
        {
            AssetDatabase.RenameAsset(GetItemFolderPath(name)+"/"+oldName.ToTitleCase()+".asset",name.ToTitleCase());
            AssetDatabase.Refresh();
        }

        public static string GetItemFolderPath(string name)
        {
            string itemPath = GroundlessSettings.GetOrCreateSettings().ItemPath;
            var folderName = name.ToTitleCase();

            string path = "";
            if (!AssetDatabase.IsValidFolder(itemPath + "/" + folderName)) { throw new NotSupportedException("Folder "+name+" doesn't exist"); }

            path = itemPath + "/" + folderName;
            return path;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Current highest ID+1</returns>
        public static int GetNextId()
        {
            var items = LoadItemAssets();
            if (items == null)
            {
                return 0;}
            var ints = items.Select(x=>x.Id);
            var enumerable = ints as int[] ?? ints.ToArray();
            int counter = enumerable.Any() ? enumerable.First() : -1;

            while (counter < int.MaxValue)
            {
                if (!enumerable.Contains(++counter)) return counter;
            }

            return int.MinValue;
        }


    }
}
#endif