using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Settings
{

    public class GroundlessSettings : ScriptableObject
    {
        // Create a new type of Settings Asset.

        public const string k_MyCustomSettingsPath = "Assets/Settings/GroundlessSettings.asset";

#pragma warning disable 414 //used by EditorToolsSettingsProvider
        [Header("Item Creation Window Settings")]
        [SerializeField]
        private bool saveOptionWhenLoadingOrQuitting;
        [SerializeField]
        [Tooltip("Enter desired item folder path")]
        private string itemPath = "Assets/Items";

#pragma warning restore 414
        public bool SaveOptionWhenLoadingOrQuitting => saveOptionWhenLoadingOrQuitting;
        public string ItemPath => itemPath;

        public static GroundlessSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<GroundlessSettings>(k_MyCustomSettingsPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<GroundlessSettings>();
                settings.saveOptionWhenLoadingOrQuitting = true;
                settings.itemPath = "Assets/Items";
                AssetDatabase.CreateAsset(settings, k_MyCustomSettingsPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }

    }
}
