using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YourNamespace.Editor
{
    /// <summary>
    /// Generates room template prefabs based on RoomTemplateData.
    /// </summary>
    public class RoomTemplateGenerator : EditorWindow
    {
        private RoomTemplateManager roomTemplateManager;
        private List<RoomTemplateData> roomTemplates = new List<RoomTemplateData>();
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

            // Section for generating room templates
            EditorGUILayout.LabelField("Generate Room Templates", EditorStyles.boldLabel);

            if (GUILayout.Button("Generate All Room Templates"))
            {
                GenerateRoomTemplates();
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Existing Room Templates", EditorStyles.boldLabel);

            // Scrollable list of existing room templates
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
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

        private void GenerateRoomTemplates()
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

            GameObject roomTemplateInstance = Instantiate(template.TemplatePrefab);
            roomTemplateInstance.name = template.TemplateName;
            roomTemplateInstance.transform.localScale = new Vector3(template.TemplateSize.x, template.TemplateSize.y, 1);

            Debug.Log($"Generated room template '{template.TemplateName}' successfully.");
        }
    }
}
