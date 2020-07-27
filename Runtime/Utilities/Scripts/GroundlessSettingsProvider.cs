using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

// Register a SettingsProvider using IMGUI for the drawing framework:
static class  GroundlessSettingsIMGUIRegister
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
                var settings =  GroundlessSettings.GetSerializedSettings();
                EditorGUILayout.PropertyField(settings.FindProperty("autoSave"), new GUIContent("Auto Save Items"));
                EditorGUILayout.PropertyField(settings.FindProperty("itemPath"), new GUIContent("Item Folder path"));
            },

            // Populate the search keywords to enable smart search filtering and label highlighting:
            keywords = new HashSet<string>(new[] { "Auto Save", "Item Folder","items" })
        };

        return provider;
    }
}




