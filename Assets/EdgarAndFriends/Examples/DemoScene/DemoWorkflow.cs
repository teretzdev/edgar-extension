using UnityEngine;

namespace EdgarAndFriends.Examples
{
    /// <summary>
    /// Demonstrates the full workflow of using prompts to generate room templates and build out a dungeon using Edgar.
    /// </summary>
    public class DemoWorkflow : MonoBehaviour
    {
        [Tooltip("Reference to the RoomTemplateManager for managing room templates.")]
        [SerializeField] private RoomTemplateManager roomTemplateManager;

        [Tooltip("Reference to the EdgarIntegration component for synchronizing with Edgar.")]
        [SerializeField] private EdgarIntegration edgarIntegration;

        [Tooltip("Reference to the LLMIntegration component for generating room templates.")]
        [SerializeField] private LLMIntegration llmIntegration;

        private void Start()
        {
            if (roomTemplateManager == null || edgarIntegration == null || llmIntegration == null)
            {
                Debug.LogError("One or more required components are not assigned. Please assign them in the inspector.");
                return;
            }

            RunDemoWorkflow();
        }

        /// <summary>
        /// Executes the demo workflow.
        /// </summary>
        private void RunDemoWorkflow()
        {
            Debug.Log("Starting demo workflow...");

            // Step 1: Generate a room template using LLM
            string prompt = "Generate a room template for a 15x15 dungeon room with basic decorations.";
            string llmResponse = llmIntegration.SendRequestToLLM(prompt);

            if (string.IsNullOrEmpty(llmResponse))
            {
                Debug.LogError("Failed to receive a response from the LLM.");
                return;
            }

            Debug.Log($"Received LLM response: {llmResponse}");

            // Step 2: Parse and validate the LLM response
            RoomTemplateParser parser = new RoomTemplateParser();
            RoomTemplateData roomTemplateData = parser.ParseResponse(llmResponse);

            if (roomTemplateData == null)
            {
                Debug.LogError("Failed to parse the LLM response into RoomTemplateData.");
                return;
            }

            LLMResponseValidator validator = new LLMResponseValidator();
            if (!validator.ValidateResponse(llmResponse))
            {
                Debug.LogError("The LLM response failed validation.");
                return;
            }

            Debug.Log($"Parsed RoomTemplateData: {roomTemplateData.TemplateName}, Size: {roomTemplateData.TemplateSize}");

            // Step 3: Add the room template to the RoomTemplateManager
            RoomTemplate newRoomTemplate = new RoomTemplate(
                roomTemplateData.TemplateName,
                roomTemplateData.TemplateSize,
                roomTemplateData.TemplatePrefab
            );

            roomTemplateManager.AddRoomTemplate(newRoomTemplate);
            Debug.Log($"Room template '{newRoomTemplate.Name}' added to RoomTemplateManager.");

            // Step 4: Synchronize room templates with Edgar
            edgarIntegration.SendTemplatesToEdgar();
            Debug.Log("Room templates synchronized with Edgar.");

            // Step 5: Simulate receiving processed templates from Edgar
            var processedTemplates = new System.Collections.Generic.List<RoomTemplateData>
            {
                new RoomTemplateData("ProcessedRoom", new Vector2(20, 20), null)
            };

            edgarIntegration.ReceiveTemplatesFromEdgar(processedTemplates);
            Debug.Log("Processed room templates received and updated in RoomTemplateManager.");

            // Step 6: Place assets in the room template
            AssetPlacementManager assetPlacementManager = FindObjectOfType<AssetPlacementManager>();
            if (assetPlacementManager != null)
            {
                assetPlacementManager.PlaceAssetsInRoom(newRoomTemplate);
                Debug.Log($"Assets placed in room template '{newRoomTemplate.Name}'.");
            }
            else
            {
                Debug.LogWarning("AssetPlacementManager not found in the scene.");
            }

            Debug.Log("Demo workflow completed successfully.");
        }
    }
}
