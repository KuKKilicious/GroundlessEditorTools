#if UNITY_EDITOR
using Game.Base;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    /// <summary>
    /// Window to create effects for particular item
    /// </summary>
    public class EffectCreationWindow : OdinMenuEditorWindow
    {
        private static ItemTableViewData s_item;
        public static ItemTableViewData Item {
            get => s_item;
            set => s_item = value;
        }

        private CreateNewEffectData statGainEffect;
        private CreateNewEffectData spawnEffect;
        private CreateNewEffectData usableEffect;

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            if (Item == null || Item.Name.Length == 0) { return null; }
            //create temporary scriptable objects
            statGainEffect = new CreateNewEffectData(s_item,ScriptableObject.CreateInstance<StatGainEffect>());
            spawnEffect= new CreateNewEffectData(s_item,ScriptableObject.CreateInstance<SpawnEffect>());
            usableEffect= new CreateNewEffectData(s_item,ScriptableObject.CreateInstance<UsableEffect>());

            //create trees and display assets
            tree.Add("New StatGain", statGainEffect);
            tree.Add("New Spawn", spawnEffect);
            tree.Add("New Usable", usableEffect);
            tree.AddAllAssetsAtPath(Item.Name, AssetUtil.GetItemFolderPath(Item.Name), typeof(ItemEffect));
            

            return tree;

        }

        public static void OpenWindow(ItemTableViewData itemToSet)
        {
            GetWindow<EffectCreationWindow>().Show();
            GetWindow<EffectCreationWindow>().ForceMenuTreeRebuild();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            //destroy temporary so
            DestroyImmediate(statGainEffect.EffectData);
            DestroyImmediate(spawnEffect.EffectData);
            DestroyImmediate(usableEffect.EffectData);
        }

        public static void ForceTreeRebuild(){
            GetWindow<EffectCreationWindow>().ForceMenuTreeRebuild();
        }
    }
    /// <summary>
    /// Effect Data that is visualized in Window
    /// </summary>
    [System.Serializable]
    public class CreateNewEffectData
    {
        public CreateNewEffectData(ItemTableViewData item, ScriptableObject instance)
        {
            if (!(instance is ItemEffect effect))
                return;

            EffectData = effect;
            this.item = item;
        }

        [PropertyOrder(-1)]
        public string Name;

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]

        public ItemEffect EffectData;

        private ItemTableViewData item;


        [Button("Create Effect", ButtonSizes.Gigantic)]
        [GUIColor(0, 0.65f, 0.05f)]
        private void CreateNewEffect()
        {
            if (Name == null || Name.Length <= 3)
            {
                EditorUtility.DisplayDialog("Warning", "Please enter a name", "OK");
                return;
            } 
            //Create Asset, Save
            AssetUtil.SaveAsset(EffectData,item.Name,Name);
            item.Effects.Add(EffectData);
            EffectCreationWindow.ForceTreeRebuild();

        }
    }
}


#endif