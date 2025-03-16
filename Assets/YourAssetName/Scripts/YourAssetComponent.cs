using UnityEngine;

namespace YourNamespace
{
    /// <summary>
    /// Main component for your asset.
    /// </summary>
    public class YourAssetComponent : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private float someValue = 1.0f;
        [SerializeField] private bool enableFeature = true;
        
        [Header("References")]
        [SerializeField] private Transform targetTransform;
        
        // Private variables
        private bool isInitialized = false;
        
        /// <summary>
        /// Initialize the component.
        /// </summary>
        public void Initialize()
        {
            if (isInitialized)
            {
                Debug.LogWarning("YourAssetComponent is already initialized.");
                return;
            }
            
            // Initialization logic here
            
            isInitialized = true;
            Debug.Log("YourAssetComponent initialized successfully.");
        }
        
        /// <summary>
        /// Example method demonstrating functionality.
        /// </summary>
        public void DoSomething()
        {
            if (!isInitialized)
            {
                Debug.LogError("YourAssetComponent must be initialized before use.");
                return;
            }
            
            // Example functionality
            Debug.Log("YourAssetComponent did something!");
        }
        
        private void OnValidate()
        {
            // Validation logic here
            if (someValue < 0)
            {
                Debug.LogWarning("someValue should not be negative.");
            }
        }
    }
}