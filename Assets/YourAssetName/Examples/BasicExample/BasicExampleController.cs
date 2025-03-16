using UnityEngine;
using YourNamespace;

namespace YourNamespace.Examples
{
    /// <summary>
    /// Example controller demonstrating basic usage of the asset.
    /// </summary>
    public class BasicExampleController : MonoBehaviour
    {
        [SerializeField] private YourAssetComponent assetComponent;
        
        private void Start()
        {
            // Initialize the component
            assetComponent.Initialize();
            
            // Use the component
            assetComponent.DoSomething();
        }
        
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            GUILayout.Label("Basic Example", GUI.skin.box);
            GUILayout.Space(10);
            
            if (GUILayout.Button("Do Something"))
            {
                assetComponent.DoSomething();
            }
            
            GUILayout.EndArea();
        }
    }
}