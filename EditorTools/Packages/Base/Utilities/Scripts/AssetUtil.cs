using System.Collections;
using System.Collections.Generic;
using Game.Base;
using Game.Base.Utilities;


using UnityEditor;
using UnityEngine;
/// <summary>
/// Utility class for saving loading assets from database
/// </summary>
public static class AssetUtil {
    private static string ITEM_SO_PATH = "Assets/Items/";



    /// <summary>
    /// saves Item Scriptable Object via AssetDatabase in the project
    /// </summary>
    /// <param name="newItem">item to save</param>
    public static void SaveItemAsset(ItemData newItem)
    {
        //format string
        string fileName = newItem.Name.ToTitleCase();
        fileName = fileName.Replace(" ", "");
        //check for min length
        if (fileName.Length <= 3)
        {
            //TODO: throw hint to user
            return;
        }
        AssetDatabase.CreateAsset(newItem, ITEM_SO_PATH + fileName + ".asset");
        AssetDatabase.SaveAssets();
    }


   

}
