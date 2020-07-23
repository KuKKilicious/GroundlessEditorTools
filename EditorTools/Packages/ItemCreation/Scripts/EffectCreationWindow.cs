using Game.Base;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Editor
{

    public class EffectCreationWindow : OdinMenuEditorWindow
    {
        private static ItemTableViewData item;
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            if(item ==null || item.Name.Length ==0){return null;}
            tree.AddAllAssetsAtPath(item.Name, AssetUtil.GetItemFolderPath(item.Name), typeof(ItemEffect));

            return tree;

        }

        public static void OpenWindow(ItemTableViewData itemToSet)
        {
            item = itemToSet;
            GetWindow<EffectCreationWindow>().Show();
        }
    }

    public class CreateNewEffectData
    {

    }
}
