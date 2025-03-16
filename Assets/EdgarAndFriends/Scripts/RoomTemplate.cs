using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Represents a room template in the system.
    /// </summary>
    public class RoomTemplate
    {
        /// <summary>
        /// The name of the room template.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The size of the room template.
        /// </summary>
        public Vector2 Size { get; private set; }

        /// <summary>
        /// The prefab associated with the room template.
        /// </summary>
        public GameObject Prefab { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomTemplate"/> class.
        /// </summary>
        /// <param name="name">The name of the room template.</param>
        /// <param name="size">The size of the room template.</param>
        /// <param name="prefab">The prefab associated with the room template.</param>
        public RoomTemplate(string name, Vector2 size, GameObject prefab)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("Room template name cannot be null or empty.", nameof(name));
            }

            if (size.x <= 0 || size.y <= 0)
            {
                throw new System.ArgumentException("Room template size must have positive dimensions.", nameof(size));
            }

            Name = name;
            Size = size;
            Prefab = prefab;
        }

        /// <summary>
        /// Updates the size of the room template.
        /// </summary>
        /// <param name="newSize">The new size of the room template.</param>
        public void UpdateSize(Vector2 newSize)
        {
            if (newSize.x <= 0 || newSize.y <= 0)
            {
                throw new System.ArgumentException("New size must have positive dimensions.", nameof(newSize));
            }

            Size = newSize;
        }

        /// <summary>
        /// Updates the prefab associated with the room template.
        /// </summary>
        /// <param name="newPrefab">The new prefab to associate with the room template.</param>
        public void UpdatePrefab(GameObject newPrefab)
        {
            Prefab = newPrefab;
        }

        /// <summary>
        /// Provides a string representation of the room template.
        /// </summary>
        /// <returns>A string describing the room template.</returns>
        public override string ToString()
        {
            return $"RoomTemplate(Name: {Name}, Size: {Size}, Prefab: {(Prefab != null ? Prefab.name : "None")})";
        }
    }
}
