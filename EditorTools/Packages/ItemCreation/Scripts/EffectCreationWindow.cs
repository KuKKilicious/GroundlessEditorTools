using Game.Base;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{

    public class EffectCreationWindow : OdinMenuEditorWindow
    {
        private static ItemTableViewData item;
        public static ItemTableViewData Item {
            get => item;
            set => item = value;
        }

        private CreateNewEffectData statGainEffect;
        private CreateNewEffectData spawnEffect;
        private CreateNewEffectData customEffect;


        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            if (Item == null || Item.Name.Length == 0) { return null; }

            statGainEffect = new CreateNewEffectData(item);
            tree.Add("New StatGain", statGainEffect);
            tree.Add("New Spawn", spawnEffect);
            tree.Add("New Custom", customEffect);
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
            DestroyImmediate(statGainEffect.EffectData);
            DestroyImmediate(spawnEffect.EffectData);
            DestroyImmediate(customEffect.EffectData);
        }

        public static void ForceTreeRebuild(){
            GetWindow<EffectCreationWindow>().ForceMenuTreeRebuild();
        }
    }

    public class CreateNewEffectData
    {
        public CreateNewEffectData(ItemTableViewData item)
        {
            EffectData = ScriptableObject.CreateInstance<StatGainEffect>();
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
