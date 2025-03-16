using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YourNamespace.Editor
{
    /// <summary>
    /// Provides a custom editor window for generating room templates.
    /// This tool allows users to create and manage room template prefabs directly from the editor.
    /// </summary>
    public class RoomTemplateGenerator : EditorWindow
    {
        private RoomTemplateManager roomTemplateManager;
        private List<RoomTemplate> roomTemplates = new List<RoomTemplate>();
        private Vector2 scrollPosition;

        [MenuItem("Tools/Room Template Generator")]
        public static void ShowWindow()
        {
            GetWindow<RoomTemplateGenerator>("Room Template Generator");
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
            EditorGUILayout.LabelField("Room Template Generator", EditorStyles.boldLabel);
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
                EditorGUILayout.LabelField(template.Name, GUILayout.Width(150));
                EditorGUILayout.LabelField($"Size: {template.Size}", GUILayout.Width(100));
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

        private void GenerateSingleRoomTemplate(RoomTemplate template)
        {
            if (template.Prefab == null)
            {
                Debug.LogError($"Template '{template.Name}' has no prefab assigned. Skipping generation.");
                return;
            }

            // Instantiate the prefab and configure it based on the template data
            GameObject roomTemplateInstance = Instantiate(template.Prefab);
            roomTemplateInstance.name = template.Name;
            roomTemplateInstance.transform.localScale = new Vector3(template.Size.x, template.Size.y, 1);

            // Optionally, save the generated prefab to the project
            string path = $"Assets/GeneratedRoomTemplates/{template.Name}.prefab";
            string directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            PrefabUtility.SaveAsPrefabAsset(roomTemplateInstance, path);
            DestroyImmediate(roomTemplateInstance);

            Debug.Log($"Generated room template '{template.Name}' and saved as prefab at '{path}'.");
        }
    }
}
