using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundlessSettings : ScriptableObject
{
    // Create a new type of Settings Asset.
    
        public const string k_MyCustomSettingsPath = "Assets/Settings/GroundlessSettings.asset";

#pragma warning disable 414 //used by EditorToolsSettingsProvider
        [Header("Item Creation Window Settings")]
        [SerializeField]
        private bool autoSave = false;
        [SerializeField][Tooltip("Enter desired item folder path")]
        private string itemPath = "Assets/Items";

#pragma warning restore 414
        public bool AutoSave => autoSave;
        public string ItemPath => itemPath;

        internal static GroundlessSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<GroundlessSettings>(k_MyCustomSettingsPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<GroundlessSettings>();
                settings.autoSave = false;
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
