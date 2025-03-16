using System.Collections.Generic;
using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Manages the creation, retrieval, and deletion of room templates.
    /// </summary>
    public class RoomTemplateManager : MonoBehaviour
    {
        [Tooltip("List of room templates managed by this component.")]
        [SerializeField] private List<RoomTemplateData> roomTemplates = new List<RoomTemplateData>();

        /// <summary>
        /// Adds a new room template to the manager.
        /// </summary>
        /// <param name="template">The room template to add.</param>
        public void AddRoomTemplate(RoomTemplateData template)
        {
            if (template == null)
            {
                Debug.LogError("Cannot add a null room template.");
                return;
            }

            if (roomTemplates.Exists(t => t.TemplateName == template.TemplateName))
            {
                Debug.LogWarning($"Room template with name '{template.TemplateName}' already exists. Skipping addition.");
                return;
            }

            roomTemplates.Add(template);
            Debug.Log($"Room template '{template.TemplateName}' added successfully.");
        }

        /// <summary>
        /// Removes a room template from the manager.
        /// </summary>
        /// <param name="template">The room template to remove.</param>
        public void RemoveRoomTemplate(RoomTemplateData template)
        {
            if (template == null)
            {
                Debug.LogError("Cannot remove a null room template.");
                return;
            }

            if (roomTemplates.Remove(template))
            {
                Debug.Log($"Room template '{template.TemplateName}' removed successfully.");
            }
            else
            {
                Debug.LogWarning($"Room template with name '{template.TemplateName}' not found.");
            }
        }

        /// <summary>
        /// Retrieves all room templates managed by this component.
        /// </summary>
        /// <returns>A list of all room templates.</returns>
        public List<RoomTemplateData> GetAllRoomTemplates()
        {
            return new List<RoomTemplateData>(roomTemplates);
        }

        /// <summary>
        /// Finds a room template by its name.
        /// </summary>
        /// <param name="name">The name of the room template to find.</param>
        /// <returns>The room template if found, otherwise null.</returns>
        public RoomTemplateData FindRoomTemplateByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("Room template name cannot be null or empty.");
                return null;
            }

            var template = roomTemplates.Find(t => t.TemplateName == name);
            if (template == null)
            {
                Debug.LogWarning($"Room template with name '{name}' not found.");
            }

            return template;
        }
    }
}