using System.Collections.Generic;
using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Integrates the room template system with Edgar, enabling seamless data exchange and functionality.
    /// </summary>
    public class EdgarIntegration : MonoBehaviour
    {
        [Tooltip("Reference to the RoomTemplateManager for managing room templates.")]
        [SerializeField] private RoomTemplateManager roomTemplateManager;

        [Tooltip("Reference to the DungeonGenerator for generating dungeons.")]
        [SerializeField] private DungeonGenerator dungeonGenerator;

        [Tooltip("The list of Edgar-compatible room templates.")]
        [SerializeField] private List<RoomTemplateData> edgarRoomTemplates = new List<RoomTemplateData>();

        /// <summary>
        /// Initializes the integration with Edgar by synchronizing room templates.
        /// </summary>
        private void Start()
        {
            if (roomTemplateManager == null)
            {
                Debug.LogError("RoomTemplateManager is not assigned. Please assign it in the inspector.");
                return;
            }

            SyncRoomTemplatesWithEdgar();
        }

        /// <summary>
        /// Synchronizes room templates from the RoomTemplateManager with Edgar.
        /// </summary>
        private void SyncRoomTemplatesWithEdgar()
        {
            var allRoomTemplates = roomTemplateManager.GetAllRoomTemplates();

            if (allRoomTemplates == null || allRoomTemplates.Count == 0)
            {
                Debug.LogWarning("No room templates found in the RoomTemplateManager to sync with Edgar.");
                return;
            }

            edgarRoomTemplates.Clear();

            foreach (var template in allRoomTemplates)
            {
                var edgarTemplate = ConvertToEdgarTemplate(template);
                if (edgarTemplate != null)
                {
                    edgarRoomTemplates.Add(edgarTemplate);
                }
            }

            Debug.Log($"Successfully synchronized {edgarRoomTemplates.Count} room templates with Edgar.");
        }

        /// <summary>
        /// Converts a RoomTemplate to an Edgar-compatible RoomTemplateData.
        /// </summary>
        /// <param name="template">The RoomTemplate to convert.</param>
        /// <returns>An Edgar-compatible RoomTemplateData object.</returns>
        private RoomTemplateData ConvertToEdgarTemplate(RoomTemplate template)
        {
            if (template == null)
            {
                Debug.LogError("Cannot convert a null RoomTemplate to an Edgar-compatible template.");
                return null;
            }

            try
            {
                var edgarTemplate = new RoomTemplateData(template.Name, template.Size, template.Prefab);
                Debug.Log($"Converted RoomTemplate '{template.Name}' to Edgar-compatible format.");
                return edgarTemplate;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to convert RoomTemplate '{template.Name}' to Edgar-compatible format: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Sends the synchronized room templates to Edgar for further processing.
        /// </summary>
        public void SendTemplatesToEdgar()
        {
            if (edgarRoomTemplates == null || edgarRoomTemplates.Count == 0)
            {
                Debug.LogWarning("No room templates available to send to Edgar.");
                return;
            }

            foreach (var template in edgarRoomTemplates)
            {
                Debug.Log($"Sending room template '{template.TemplateName}' to Edgar...");
                // Replace this with actual Edgar API integration logic.
            }

            Debug.Log("All room templates have been sent to Edgar.");
        }

        /// <summary>
        /// Receives processed room templates from Edgar and updates the RoomTemplateManager.
        /// </summary>
        /// <param name="processedTemplates">The list of processed room templates from Edgar.</param>
        public void ReceiveTemplatesFromEdgar(List<RoomTemplateData> processedTemplates)
        {
            if (processedTemplates == null || processedTemplates.Count == 0)
            {
                Debug.LogWarning("No processed room templates received from Edgar.");
                return;
            }

            foreach (var template in processedTemplates)
            {
                var existingTemplate = roomTemplateManager.FindRoomTemplateByName(template.TemplateName);
                if (existingTemplate != null)
                {
                    Debug.Log($"Updating existing room template '{template.TemplateName}' with data from Edgar.");
                    existingTemplate.Size = template.TemplateSize;
                    existingTemplate.Prefab = template.TemplatePrefab;
                }
                else
                {
                    Debug.Log($"Adding new room template '{template.TemplateName}' from Edgar.");
                    roomTemplateManager.AddRoomTemplate(new RoomTemplate(template.TemplateName, template.TemplateSize, template.TemplatePrefab));
                }
            }

            Debug.Log("Room templates have been updated with data from Edgar.");
        }
    }
}