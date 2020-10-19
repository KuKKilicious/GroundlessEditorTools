#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Game.Settings
{
    // Register a SettingsProvider using IMGUI for the drawing framework:
    static class GroundlessSettingsIMGUIRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateGroundlessSettingsProvider()
        {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Project Settings window.
            var provider = new SettingsProvider("Project/GroundlessSettings", SettingsScope.Project)
            {
                // By default the last token of the path is used as display name if no label is provided.
                label = "Groundless",
                // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
                guiHandler = (searchContext) =>
                {
                    var settings = GroundlessSettings.GetSerializedSettings();
                    EditorGUILayout.PropertyField(settings.FindProperty("confirmWhenLoadingOrQuitting"),new GUIContent("Confirmation on Load/Quit"));
                    EditorGUILayout.PropertyField(settings.FindProperty("itemPath"), new GUIContent("Item Folder path"));
                    settings.ApplyModifiedProperties();
                },

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(new[] { "Auto Save", "Item Folder", "items" })
            };

            return provider;
        }

    }

}
#endif