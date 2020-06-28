using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Base;
using Game.Base.Utilities;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
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
    public static bool SaveItemAsset(ItemData newItem)
    {
        //format string
        string fileName = newItem.Name.ToTitleCase();
        fileName = fileName.Replace(" ", "");
        //check for min length
        if (fileName.Length <= 3)
        {
            //TODO: throw hint to user
            return false;
        }
        AssetDatabase.CreateAsset(newItem, ITEM_SO_PATH + fileName + ".asset");
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
            string[] assetsGUID = AssetDatabase.FindAssets("t:ItemData", new[] { ITEM_SO_FOLDER_PATH });

            if (assetsGUID == null || assetsGUID.Length <= 0) { return null; } //return if folder doesn't contain items

            //Iterate over guids to get path and get ItemData from path and add each item to items list
            items.AddRange(assetsGUID
                .Select(guid => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assetsGUID[0]), typeof(ItemData)) as ItemData)
                .TakeWhile(item => item != null));
        }
        catch (System.Exception)
        {
            items = null;
        }
        return items?.ToArray();
    }


}
