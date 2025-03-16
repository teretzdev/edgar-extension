using UnityEditor;
using UnityEngine;

namespace YourNamespace.Editor
{
    /// <summary>
    /// Provides a custom editor window for creating and managing room templates.
    /// Delegates business logic to the RoomTemplateManager.
    /// </summary>
    public class RoomTemplateCreator : EditorWindow
    {
        private RoomTemplateManager roomTemplateManager;
        private string newTemplateName = string.Empty;
        private Vector2 newTemplateSize = Vector2.one;
        private GameObject newTemplatePrefab;

        private Vector2 scrollPosition;

        [MenuItem("Tools/Room Template Creator")]
        public static void ShowWindow()
        {
            GetWindow<RoomTemplateCreator>("Room Template Creator");
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
            EditorGUILayout.LabelField("Room Template Creator", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // Section for creating a new room template
            EditorGUILayout.LabelField("Create New Room Template", EditorStyles.boldLabel);
            newTemplateName = EditorGUILayout.TextField("Template Name", newTemplateName);
            newTemplateSize = EditorGUILayout.Vector2Field("Template Size", newTemplateSize);
            newTemplatePrefab = (GameObject)EditorGUILayout.ObjectField("Template Prefab", newTemplatePrefab, typeof(GameObject), false);

            if (GUILayout.Button("Add Room Template"))
            {
                AddRoomTemplate();
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Existing Room Templates", EditorStyles.boldLabel);

            // Scrollable list of existing room templates
            if (roomTemplateManager != null)
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
                foreach (var template in roomTemplateManager.GetAllRoomTemplates())
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(template.Name, GUILayout.Width(150));
                    if (GUILayout.Button("Remove", GUILayout.Width(100)))
                    {
                        RemoveRoomTemplate(template);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            }
            else
            {
                EditorGUILayout.HelpBox("RoomTemplateManager not found in the scene.", MessageType.Warning);
            }
        }

        private void AddRoomTemplate()
        {
            if (string.IsNullOrEmpty(newTemplateName))
            {
                Debug.LogError("Template name cannot be empty.");
                return;
            }

            if (newTemplatePrefab == null)
            {
                Debug.LogError("Template prefab cannot be null.");
                return;
            }

            RoomTemplate newTemplate = new RoomTemplate(newTemplateName, newTemplateSize, newTemplatePrefab);
            roomTemplateManager.AddRoomTemplate(newTemplate);

            // Clear input fields
            newTemplateName = string.Empty;
            newTemplateSize = Vector2.one;
            newTemplatePrefab = null;
        }

        private void RemoveRoomTemplate(RoomTemplate template)
        {
            roomTemplateManager.RemoveRoomTemplate(template);
        }
    }
}
