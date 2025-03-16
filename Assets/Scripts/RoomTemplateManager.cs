using System.Collections.Generic;
using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Handles the business logic for room template operations, separate from the UI.
    /// </summary>
    public class RoomTemplateManager : MonoBehaviour
    {
        // A dictionary to store room templates by name for faster lookups
        private Dictionary<string, RoomTemplate> roomTemplates = new Dictionary<string, RoomTemplate>();

        /// <summary>
        /// Adds a new room template to the manager.
        /// </summary>
        /// <param name="template">The room template to add.</param>
        public void AddRoomTemplate(RoomTemplate template)
        {
            if (template == null)
            {
                Debug.LogError("Cannot add a null room template.");
                return;
            }

            if (roomTemplates.ContainsKey(template.Name))
            {
                Debug.LogWarning($"Room template with name '{template.Name}' already exists. Skipping addition.");
                return;
            }

            roomTemplates.Add(template.Name, template);
            Debug.Log($"Room template '{template.Name}' added successfully.");
        }

        /// <summary>
        /// Removes a room template from the manager.
        /// </summary>
        /// <param name="template">The room template to remove.</param>
        public void RemoveRoomTemplate(RoomTemplate template)
        {
            if (template == null)
            {
                Debug.LogError("Cannot remove a null room template.");
                return;
            }

            if (roomTemplates.Remove(template.Name))
            {
                Debug.Log($"Room template '{template.Name}' removed successfully.");
            }
            else
            {
                Debug.LogWarning($"Room template with name '{template.Name}' not found.");
            }
        }

        /// <summary>
        /// Retrieves all room templates managed by this class.
        /// </summary>
        /// <returns>A list of all room templates.</returns>
        public List<RoomTemplate> GetAllRoomTemplates()
        {
            return new List<RoomTemplate>(roomTemplates.Values);
        }

        /// <summary>
        /// Finds a room template by its name.
        /// </summary>
        /// <param name="name">The name of the room template to find.</param>
        /// <returns>The room template if found, otherwise null.</returns>
        public RoomTemplate FindRoomTemplateByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("Room template name cannot be null or empty.");
                return null;
            }

            if (roomTemplates.TryGetValue(name, out var template))
            {
                return template;
            }

            Debug.LogWarning($"Room template with name '{name}' not found.");
            return null;
        }
    }

    /// <summary>
    /// Represents a room template.
    /// </summary>
    [System.Serializable]
    public class RoomTemplate
    {
        public string Name;
        public Vector2 Size;
        public GameObject Prefab;

        public RoomTemplate(string name, Vector2 size, GameObject prefab)
        {
            Name = name;
            Size = size;
            Prefab = prefab;
        }
    }
}