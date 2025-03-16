using System;
using System.Collections.Generic;
using UnityEngine;

namespace YourNamespace
{
    /// <summary>
    /// Manages advanced asset placement logic with flexible positioning options.
    /// </summary>
    public class AssetPlacementManager : MonoBehaviour
    {
        [Tooltip("The list of assets to be placed.")]
        [SerializeField] private List<GameObject> assets;

        [Tooltip("The area within which assets can be placed.")]
        [SerializeField] private Rect placementArea;

        [Tooltip("The minimum distance between assets.")]
        [SerializeField] private float minimumDistance = 1.0f;

        /// <summary>
        /// Places assets within the defined placement area using flexible positioning options.
        /// </summary>
        public void PlaceAssets()
        {
            if (assets == null || assets.Count == 0)
            {
                Debug.LogError("No assets provided for placement.");
                return;
            }

            foreach (var asset in assets)
            {
                if (asset == null)
                {
                    Debug.LogError("Asset in the list is null. Skipping placement for this asset.");
                    continue;
                }

                Vector2 position = GenerateValidPosition();
                Instantiate(asset, new Vector3(position.x, position.y, 0), Quaternion.identity);
                Debug.Log($"Placed asset '{asset.name}' at position {position}.");
            }
        }

        /// <summary>
        /// Generates a valid position within the placement area, ensuring minimum distance constraints.
        /// </summary>
        /// <returns>A valid position for asset placement.</returns>
        private Vector2 GenerateValidPosition()
        {
            Vector2 position;
            int attempts = 0;

            do
            {
                position = new Vector2(
                    UnityEngine.Random.Range(placementArea.xMin, placementArea.xMax),
                    UnityEngine.Random.Range(placementArea.yMin, placementArea.yMax)
                );
                attempts++;
            } while (!IsPositionValid(position) && attempts < 100);

            if (attempts >= 100)
            {
                Debug.LogWarning("Failed to find a valid position after 100 attempts. Using last generated position.");
            }

            return position;
        }

        /// <summary>
        /// Checks if the given position is valid based on minimum distance constraints.
        /// </summary>
        /// <param name="position">The position to validate.</param>
        /// <returns>True if the position is valid, otherwise false.</returns>
        private bool IsPositionValid(Vector2 position)
        {
            foreach (var asset in assets)
            {
                if (asset == null) continue;

                Vector3 assetPosition = asset.transform.position;
                float distance = Vector2.Distance(position, new Vector2(assetPosition.x, assetPosition.y));

                if (distance < minimumDistance)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Clears all assets placed in the scene and resets their positions.
        /// </summary>
        public void ClearPlacedAssets()
        {
            foreach (var asset in assets)
            {
                if (asset != null)
                {
                    asset.transform.position = Vector3.zero;
                    Debug.Log($"Reset position of asset '{asset.name}' to {Vector3.zero}.");
                }
            }

            Debug.Log("Cleared all placed assets and reset their positions.");
        }
    }
}