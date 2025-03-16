using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EdgarAndFriends.Editor
{
    /// <summary>
    /// Optimized version of the Room Template Generator with enhanced batch processing and prefab saving capabilities.
    /// </summary>
    public class RoomTemplateGeneratorOptimized : EditorWindow
    {
        private RoomTemplateManager roomTemplateManager;
        private List<RoomTemplateData> roomTemplates = new List<RoomTemplateData>();
        private Vector2 scrollPosition;

        [MenuItem("Tools/Room Template Generator Optimized")]
        public static void ShowWindow()
        {
            GetWindow<RoomTemplateGeneratorOptimized>("Room Template Generator Optimized");
        }

        private void OnEnable()
        {
            // Find or create a RoomTemplateManager in the scene
            roomTemplateManager = FindObjectOfType<RoomTemplateManager>();
            if (roomTemplateManager == null)
            {
                GameObject managerObject = new GameObject("RoomTemplateManager");
                roomTemplateManager = managerObject.AddComponent<RoomTemplateManager>();
                Debug.Log("Created a new RoomTemplateManager in the scene.");
            }

            // Load existing room templates
            roomTemplates = roomTemplateManager.GetAllRoomTemplates();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Room Template Generator Optimized", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // Section for generating all room templates
            if (GUILayout.Button("Generate All Room Templates"))
            {
                GenerateAllRoomTemplates();
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Existing Room Templates", EditorStyles.boldLabel);

            // Scrollable list of existing room templates
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));
            foreach (var template in roomTemplates)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(template.TemplateName, GUILayout.Width(150));
                EditorGUILayout.LabelField($"Size: {template.TemplateSize}", GUILayout.Width(100));
                if (GUILayout.Button("Generate", GUILayout.Width(100)))
                {
                    GenerateSingleRoomTemplate(template);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        private void GenerateAllRoomTemplates()
        {
            if (roomTemplates == null || roomTemplates.Count == 0)
            {
                Debug.LogWarning("No room templates available for generation.");
                return;
            }

            foreach (var template in roomTemplates)
            {
                GenerateSingleRoomTemplate(template);
            }

            Debug.Log("Generated all room templates.");
        }

        private void GenerateSingleRoomTemplate(RoomTemplateData template)
        {
            if (template.TemplatePrefab == null)
            {
                Debug.LogError($"Template '{template.TemplateName}' has no prefab assigned. Skipping generation.");
                return;
            }

            // Instantiate the prefab and configure it based on the template data
            GameObject roomTemplateInstance = Instantiate(template.TemplatePrefab);
            roomTemplateInstance.name = template.TemplateName;
            roomTemplateInstance.transform.localScale = new Vector3(template.TemplateSize.x, template.TemplateSize.y, 1);

            // Save the generated prefab to the project
            string path = $"Assets/GeneratedRoomTemplates/{template.TemplateName}.prefab";
            string directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            PrefabUtility.SaveAsPrefabAsset(roomTemplateInstance, path);
            DestroyImmediate(roomTemplateInstance);

            Debug.Log($"Generated room template '{template.TemplateName}' and saved as prefab at '{path}'.");
        }
    }
}