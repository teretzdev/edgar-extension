using System;
using System.Collections.Generic;
using UnityEngine;

namespace YourNamespace
{
    /// <summary>
    /// A custom asset database for organizing and categorizing assets for scalability.
    /// </summary>
    public class CustomAssetDatabase : MonoBehaviour
    {
        [Tooltip("List of asset categories.")]
        [SerializeField] private List<AssetCategory> assetCategories = new List<AssetCategory>();

        /// <summary>
        /// Adds a new asset to a specified category. If the category does not exist, it will be created.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="asset">The asset to add.</param>
        public void AddAsset(string categoryName, GameObject asset)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                Debug.LogError("Category name cannot be null or empty.");
                return;
            }

            if (asset == null)
            {
                Debug.LogError("Asset cannot be null.");
                return;
            }

            var category = FindCategoryByName(categoryName);
            if (category == null)
            {
                category = new AssetCategory(categoryName);
                assetCategories.Add(category);
                Debug.Log($"Created new category '{categoryName}'.");
            }

            category.AddAsset(asset);
            Debug.Log($"Added asset '{asset.name}' to category '{categoryName}'.");
        }

        /// <summary>
        /// Removes an asset from a specified category. Logs a warning if the category or asset does not exist.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="asset">The asset to remove.</param>
        public void RemoveAsset(string categoryName, GameObject asset)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                Debug.LogError("Category name cannot be null or empty.");
                return;
            }

            if (asset == null)
            {
                Debug.LogError("Asset cannot be null.");
                return;
            }

            var category = FindCategoryByName(categoryName);
            if (category == null)
            {
                Debug.LogWarning($"Category '{categoryName}' not found.");
                return;
            }

            category.RemoveAsset(asset);
            Debug.Log($"Removed asset '{asset.name}' from category '{categoryName}'.");
        }

        /// <summary>
        /// Retrieves all assets in a specified category. Returns null if the category does not exist.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <returns>A list of assets in the category, or null if the category does not exist.</returns>
        public List<GameObject> GetAssetsByCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                Debug.LogError("Category name cannot be null or empty.");
                return null;
            }

            var category = FindCategoryByName(categoryName);
            if (category == null)
            {
                Debug.LogWarning($"Category '{categoryName}' not found.");
                return null;
            }

            return category.Assets;
        }

        /// <summary>
        /// Retrieves all categories in the database as a new list.
        /// </summary>
        /// <returns>A list of all asset categories.</returns>
        public List<AssetCategory> GetAllCategories()
        {
            return new List<AssetCategory>(assetCategories);
        }

        /// <summary>
        /// Finds a category by its name. Returns null if no matching category is found.
        /// </summary>
        /// <param name="categoryName">The name of the category to find.</param>
        /// <returns>The category if found, otherwise null.</returns>
        private AssetCategory FindCategoryByName(string categoryName)
        {
            return assetCategories.Find(category => category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// Represents a category of assets.
    /// </summary>
    [Serializable]
    public class AssetCategory
    {
        [Tooltip("The name of the category.")]
        [SerializeField] private string name;

        [Tooltip("The list of assets in this category.")]
        [SerializeField] private List<GameObject> assets = new List<GameObject>();

        /// <summary>
        /// Gets the name of the category.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the list of assets in the category.
        /// </summary>
        public List<GameObject> Assets => assets;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetCategory"/> class.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        public AssetCategory(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Adds an asset to the category.
        /// </summary>
        /// <param name="asset">The asset to add.</param>
        public void AddAsset(GameObject asset)
        {
            if (asset == null)
            {
                Debug.LogError("Cannot add a null asset.");
                return;
            }

            if (assets.Contains(asset))
            {
                Debug.LogWarning($"Asset '{asset.name}' is already in the category '{name}'.");
                return;
            }

            assets.Add(asset);
        }

        /// <summary>
        /// Removes an asset from the category.
        /// </summary>
        /// <param name="asset">The asset to remove.</param>
        public void RemoveAsset(GameObject asset)
        {
            if (asset == null)
            {
                Debug.LogError("Cannot remove a null asset.");
                return;
            }

            if (!assets.Remove(asset))
            {
                Debug.LogWarning($"Asset '{asset.name}' not found in the category '{name}'.");
            }
        }
    }
}