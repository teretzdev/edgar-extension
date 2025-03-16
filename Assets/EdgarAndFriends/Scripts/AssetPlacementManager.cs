using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Manages the placement of assets in a scene based on predefined rules and constraints.
    /// </summary>
    public class AssetPlacementManager : MonoBehaviour
    {
        [Header("Placement Settings")]
        [Tooltip("The list of assets to be placed.")]
        [SerializeField] private List<GameObject> assetsToPlace;

        [Tooltip("The area within which assets can be placed.")]
        [SerializeField] private Rect placementArea;

        [Tooltip("The minimum distance between placed assets.")]
        [SerializeField] private float minimumDistance = 1.0f;

        [Tooltip("The maximum number of placement attempts for each asset.")]
        [SerializeField] private int maxPlacementAttempts = 100;

        private List<Vector2> placedPositions = new List<Vector2>();

        /// <summary>
        /// Places all assets within the defined placement area while respecting constraints.
        /// </summary>
        public void PlaceAssets()
        {
            if (assetsToPlace == null || assetsToPlace.Count == 0)
            {
                Debug.LogError("No assets provided for placement.");
                return;
            }

            foreach (var asset in assetsToPlace)
            {
                if (asset == null)
                {
                    Debug.LogError("Asset in the list is null. Skipping placement for this asset.");
                    continue;
                }

                Vector2 position = GenerateValidPosition();
                if (position != Vector2.zero)
                {
                    Instantiate(asset, new Vector3(position.x, position.y, 0), Quaternion.identity);
                    placedPositions.Add(position);
                    Debug.Log($"Placed asset '{asset.name}' at position {position}.");
                }
                else
                {
                    Debug.LogWarning($"Failed to place asset '{asset.name}' after {maxPlacementAttempts} attempts.");
                }
            }
        }

        /// <summary>
        /// Generates a valid position within the placement area, ensuring minimum distance constraints.
        /// </summary>
        /// <returns>A valid position for asset placement, or Vector2.zero if no valid position is found.</returns>
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
            } while (!IsPositionValid(position) && attempts < maxPlacementAttempts);

            return attempts < maxPlacementAttempts ? position : Vector2.zero;
        }

        /// <summary>
        /// Checks if the given position is valid based on minimum distance constraints.
        /// </summary>
        /// <param name="position">The position to validate.</param>
        /// <returns>True if the position is valid, otherwise false.</returns>
        private bool IsPositionValid(Vector2 position)
        {
            foreach (var placedPosition in placedPositions)
            {
                if (Vector2.Distance(position, placedPosition) < minimumDistance)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Clears all placed assets from the scene and resets the placement data.
        /// </summary>
        public void ClearPlacedAssets()
        {
            foreach (var asset in assetsToPlace)
            {
                if (asset != null)
                {
                    Destroy(asset);
                }
            }

            placedPositions.Clear();
            Debug.Log("Cleared all placed assets and reset placement data.");
        }

        /// <summary>
        /// Places a single asset at a specified position.
        /// </summary>
        /// <param name="asset">The asset to place.</param>
        /// <param name="position">The position to place the asset at.</param>
        public void PlaceAssetAtPosition(GameObject asset, Vector2 position)
        {
            if (asset == null)
            {
                Debug.LogError("Asset is null. Cannot place it.");
                return;
            }

            if (!placementArea.Contains(position))
            {
                Debug.LogError($"Position {position} is outside the placement area. Cannot place asset.");
                return;
            }

            if (!IsPositionValid(position))
            {
                Debug.LogError($"Position {position} violates minimum distance constraints. Cannot place asset.");
                return;
            }

            Instantiate(asset, new Vector3(position.x, position.y, 0), Quaternion.identity);
            placedPositions.Add(position);
            Debug.Log($"Placed asset '{asset.name}' at position {position}.");
        }
        public void ClearPlacedAssets()
        {
            foreach (var asset in assetsToPlace)
            {
                if (asset != null)
                {
                    Destroy(asset);
                }
            }

            placedPositions.Clear();
            Debug.Log("Cleared all placed assets and reset placement data.");
        }

        /// <summary>
        /// Visualizes the placement area in the editor for debugging purposes.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(placementArea.center.x, placementArea.center.y, 0), new Vector3(placementArea.width, placementArea.height, 0));
        }
    }
}