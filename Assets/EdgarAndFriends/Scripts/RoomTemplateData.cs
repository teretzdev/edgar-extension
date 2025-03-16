using System;
using UnityEngine;

namespace YourNamespace
{
    /// <summary>
    /// Represents the data structure for room templates with validation and error handling.
    /// </summary>
    [Serializable]
    public class RoomTemplateData
    {
        [Tooltip("The name of the room template.")]
        [SerializeField] private string templateName;

        [Tooltip("The size of the room template.")]
        [SerializeField] private Vector2 templateSize;

        [Tooltip("The prefab associated with the room template.")]
        [SerializeField] private GameObject templatePrefab;

        /// <summary>
        /// Gets or sets the name of the room template.
        /// </summary>
        public string TemplateName
        {
            get => templateName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Template name cannot be null or empty.");
                }
                templateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the room template.
        /// </summary>
        public Vector2 TemplateSize
        {
            get => templateSize;
            set
            {
                if (value.x <= 0 || value.y <= 0)
                {
                    throw new ArgumentException("Template size must have positive dimensions.");
                }
                templateSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the prefab associated with the room template.
        /// </summary>
        public GameObject TemplatePrefab
        {
            get => templatePrefab;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Template prefab cannot be null.");
                }
                templatePrefab = value;
            }
        }

        /// <summary>
        /// Validates the room template data.
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(templateName))
            {
                Debug.LogError("Template name cannot be null or empty.");
                throw new InvalidOperationException("Template name cannot be null or empty.");
            }

            if (templateSize.x <= 0 || templateSize.y <= 0)
            {
                Debug.LogError("Template size must have positive dimensions.");
                throw new InvalidOperationException("Template size must have positive dimensions.");
            }

            if (templatePrefab == null)
            {
                Debug.LogError("Template prefab cannot be null.");
                throw new InvalidOperationException("Template prefab cannot be null.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomTemplateData"/> class.
        /// </summary>
        /// <param name="name">The name of the room template.</param>
        /// <param name="size">The size of the room template.</param>
        /// <param name="prefab">The prefab associated with the room template.</param>
        public RoomTemplateData(string name, Vector2 size, GameObject prefab)
        {
            TemplateName = name;
            TemplateSize = size;
            TemplatePrefab = prefab;
        }
    }
}
