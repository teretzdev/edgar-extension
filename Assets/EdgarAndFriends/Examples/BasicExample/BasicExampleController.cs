using UnityEngine;

namespace YourNamespace.Examples
{
    /// <summary>
    /// A basic example controller demonstrating the usage of Edgar and Friends components.
    /// </summary>
    public class BasicExampleController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RoomTemplateManager roomTemplateManager;
        [SerializeField] private EdgarIntegration edgarIntegration;
        [SerializeField] private AssetPlacementManager assetPlacementManager;

        private void Start()
        {
            // Initialize RoomTemplateManager with a sample template
            var sampleTemplate = new RoomTemplate("SampleRoom", new Vector2(10, 10), null);
            roomTemplateManager.AddRoomTemplate(sampleTemplate);
            Debug.Log("Added a sample room template to the RoomTemplateManager.");

            // Synchronize templates with Edgar
            edgarIntegration.SendTemplatesToEdgar();
            Debug.Log("Synchronized room templates with Edgar.");

            // Place assets in the scene
            assetPlacementManager.PlaceAssets();
            Debug.Log("Placed assets in the scene.");
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            GUILayout.Label("Edgar and Friends - Basic Example", GUI.skin.box);
            GUILayout.Space(10);

            if (GUILayout.Button("Add Room Template"))
            {
                var newTemplate = new RoomTemplate("NewRoom", new Vector2(15, 15), null);
                roomTemplateManager.AddRoomTemplate(newTemplate);
                Debug.Log("Added a new room template.");
            }

            if (GUILayout.Button("Synchronize with Edgar"))
            {
                edgarIntegration.SendTemplatesToEdgar();
                Debug.Log("Synchronized room templates with Edgar.");
            }

            if (GUILayout.Button("Place Assets"))
            {
                assetPlacementManager.PlaceAssets();
                Debug.Log("Placed assets in the scene.");
            }

            GUILayout.EndArea();
        }
    }
}
