using UnityEditor;
using UnityEngine;
using YourNamespace;

namespace YourNamespace.Editor
{
    /// <summary>
    /// Custom editor for YourAssetComponent.
    /// </summary>
    [CustomEditor(typeof(YourAssetComponent))]
    public class YourAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Get the target component
            YourAssetComponent component = (YourAssetComponent)target;
            
            // Add custom header
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Your Asset Component", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            
            // Draw the default inspector
            DrawDefaultInspector();
            
            // Add custom buttons
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Initialize"))
            {
                component.Initialize();
            }
            
            if (GUILayout.Button("Do Something"))
            {
                component.DoSomething();
            }
            
            EditorGUILayout.EndHorizontal();
            
            // Add documentation link
            EditorGUILayout.Space();
            if (GUILayout.Button("Open Documentation"))
            {
                Application.OpenURL("https://github.com/yourusername/your-repo/blob/main/Assets/YourAssetName/Documentation/README.md");
            }
        }
    }
}