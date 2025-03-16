using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YourNamespace.Editor
{
    /// <summary>
    /// Optimized version of the room template generator with batch operations for better performance.
    /// </summary>
    public class RoomTemplateGeneratorOptimized : EditorWindow
    {
        private RoomTemplateManager roomTemplateManager;
        private List<RoomTemplateData> batchTemplates = new List<RoomTemplateData>();
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
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Room Template Generator Optimized", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // Section for batch operations
            EditorGUILayout.LabelField("Batch Operations", EditorStyles.boldLabel);

            if (GUILayout.Button("Add Batch Templates"))
            {
                AddBatchTemplates();
            }

            if (GUILayout.Button("Clear Batch Templates"))
            {
                ClearBatchTemplates();
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Batch Templates", EditorStyles.boldLabel);

            // Scrollable list of batch templates
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
            foreach (var template in batchTemplates)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(template.TemplateName, GUILayout.Width(150));
                if (GUILayout.Button("Remove", GUILayout.Width(100)))
                {
                    RemoveBatchTemplate(template);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Generate Room Templates", EditorStyles.boldLabel);

            if (GUILayout.Button("Generate All Templates"))
            {
                GenerateAllTemplates();
            }
        }

        private void AddBatchTemplates()
        {
            // Simulate adding multiple templates for batch processing
            for (int i = 0; i < 5; i++)
            {
                var template = new RoomTemplateData($"Template_{i + 1}", new Vector2(10, 10), null);
                batchTemplates.Add(template);
            }

            Debug.Log("Added batch templates for processing.");
        }

        private void ClearBatchTemplates()
        {
            batchTemplates.Clear();
            Debug.Log("Cleared all batch templates.");
        }

        private void RemoveBatchTemplate(RoomTemplateData template)
        {
            batchTemplates.Remove(template);
            Debug.Log($"Removed batch template '{template.TemplateName}'.");
        }

        private void GenerateAllTemplates()
        {
            if (roomTemplateManager == null)
            {
                Debug.LogError("RoomTemplateManager not found in the scene.");
                return;
            }

            foreach (var template in batchTemplates)
            {
                if (template.TemplatePrefab == null)
                {
                    Debug.LogWarning($"Template '{template.TemplateName}' has no prefab assigned. Skipping.");
                    continue;
                }

                var roomTemplate = new RoomTemplate(template.TemplateName, template.TemplateSize, template.TemplatePrefab);
                roomTemplateManager.AddRoomTemplate(roomTemplate);
            }

            Debug.Log("Generated all room templates from the batch.");
        }
    }
}
