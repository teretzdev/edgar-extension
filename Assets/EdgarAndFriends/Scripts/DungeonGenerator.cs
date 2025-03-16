using System.Collections.Generic;
using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Handles the generation of dungeons using Edgar.
    /// </summary>
    public class DungeonGenerator : MonoBehaviour
    {
        [Tooltip("Reference to the RoomTemplateManager for managing room templates.")]
        [SerializeField] private RoomTemplateManager roomTemplateManager;

        [Tooltip("Reference to the EdgarIntegration component for synchronizing with Edgar.")]
        [SerializeField] private EdgarIntegration edgarIntegration;

        [Tooltip("Reference to the AssetPlacementManager for placing assets in the dungeon.")]
        [SerializeField] private AssetPlacementManager assetPlacementManager;

        /// <summary>
        /// Generates a dungeon using the available room templates and Edgar integration.
        /// </summary>
        public void GenerateDungeon()
        {
            if (roomTemplateManager == null || edgarIntegration == null || assetPlacementManager == null)
            {
                Debug.LogError("One or more required components are not assigned. Please assign them in the inspector.");
                return;
            }

            Debug.Log("Starting dungeon generation...");

            // Step 1: Synchronize room templates with Edgar
            edgarIntegration.SendTemplatesToEdgar();
            Debug.Log("Room templates synchronized with Edgar.");

            // Step 2: Simulate receiving processed templates from Edgar
            List<RoomTemplateData> processedTemplates = edgarIntegration.ReceiveTemplatesFromEdgar();
            if (processedTemplates == null || processedTemplates.Count == 0)
            {
                Debug.LogError("Failed to receive processed templates from Edgar.");
                return;
            }

            Debug.Log($"Received {processedTemplates.Count} processed templates from Edgar.");

            // Step 3: Add processed templates to the RoomTemplateManager
            foreach (var templateData in processedTemplates)
            {
                bool added = roomTemplateManager.AddRoomTemplate(templateData);
                if (added)
                {
                    Debug.Log($"Processed room template '{templateData.TemplateName}' added to RoomTemplateManager.");
                }
            }

            // Step 4: Generate dungeon layout using processed templates
            GenerateDungeonLayout(processedTemplates);

            Debug.Log("Dungeon generation completed successfully.");
        }

        /// <summary>
        /// Generates the dungeon layout using the provided room templates.
        /// </summary>
        /// <param name="templates">The list of room templates to use for dungeon generation.</param>
        private void GenerateDungeonLayout(List<RoomTemplateData> templates)
        {
            if (templates == null || templates.Count == 0)
            {
                Debug.LogError("No room templates provided for dungeon layout generation.");
                return;
            }

            Debug.Log("Generating dungeon layout...");

            foreach (var template in templates)
            {
                // Example: Place assets in the room template
                RoomTemplate roomTemplate = new RoomTemplate(
                    template.TemplateName,
                    template.TemplateSize,
                    template.TemplatePrefab
                );

                assetPlacementManager.PlaceAssetsInRoom(roomTemplate);
                Debug.Log($"Assets placed in room template '{roomTemplate.Name}'.");
            }

            Debug.Log("Dungeon layout generation completed.");
        }
    }
}
