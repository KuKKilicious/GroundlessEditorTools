using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;

namespace Game.Settings
{

    public class GroundlessSettings : ScriptableObject
    {
        // Create a new type of Settings Asset.

        public const string k_GroundlessSettingsPath = "Assets/Settings/GroundlessSettings.asset";

#pragma warning disable 414 //used by GroundlessSettingsProvider
        [Header("Item Creation Window Settings")]
        [SerializeField]
        private bool confirmWhenLoadingOrQuitting;
        [SerializeField]
        [Tooltip("Enter desired item folder path")]
        private string itemPath = "Assets/Items";
#pragma warning restore 414

        public bool ConfirmWhenLoadingOrQuitting => confirmWhenLoadingOrQuitting;
        public string ItemPath => itemPath;

        public static GroundlessSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<GroundlessSettings>(k_GroundlessSettingsPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<GroundlessSettings>();
                settings.confirmWhenLoadingOrQuitting = true;
                settings.itemPath = "Assets/Items";

                string folderPath = Path.GetDirectoryName(k_GroundlessSettingsPath);
                //Check if folder exists
                if (!AssetDatabase.IsValidFolder(Path.GetDirectoryName(k_GroundlessSettingsPath)))
                {

                    string t1 = Directory.GetParent(folderPath).Name;
                    string t2 = Path.GetDirectoryName(folderPath);
                    string t3 = Path.GetFileName(folderPath);
                    string t4 = Path.GetPathRoot(folderPath);
                    string t5 = new FileInfo(folderPath ?? throw new InvalidOperationException())?.DirectoryName;
                    string t6 = new FileInfo(folderPath)?.Directory?.Name;

                    //create Folder
                    AssetDatabase.CreateFolder(Path.GetDirectoryName(folderPath), Path.GetFileName(folderPath));
                };
                AssetDatabase.CreateAsset(settings, k_GroundlessSettingsPath);
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
