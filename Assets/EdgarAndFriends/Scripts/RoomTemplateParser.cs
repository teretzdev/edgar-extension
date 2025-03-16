using System;
using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Parses LLM responses into room template data.
    /// </summary>
    public class RoomTemplateParser : MonoBehaviour
    {
        /// <summary>
        /// Parses the LLM response JSON string into a RoomTemplateData object.
        /// </summary>
        /// <param name="response">The JSON response from the LLM.</param>
        /// <returns>A RoomTemplateData object if parsing is successful; otherwise, null.</returns>
        public RoomTemplateData ParseResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Debug.LogError("LLM response is null or empty.");
                return null;
            }

            try
            {
                RoomTemplateData roomTemplateData = JsonUtility.FromJson<RoomTemplateData>(response);

                if (roomTemplateData == null)
                {
                    Debug.LogError("Failed to parse LLM response into RoomTemplateData.");
                    return null;
                }

                if (!ValidateRoomTemplateData(roomTemplateData))
                {
                    Debug.LogError("Parsed RoomTemplateData is invalid.");
                    return null;
                }

                Debug.Log($"Successfully parsed room template: {roomTemplateData.TemplateName}");
                return roomTemplateData;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing LLM response: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Validates the parsed RoomTemplateData object.
        /// </summary>
        /// <param name="roomTemplateData">The RoomTemplateData object to validate.</param>
        /// <returns>True if the data is valid; otherwise, false.</returns>
        public bool ValidateRoomTemplateData(RoomTemplateData roomTemplateData)
        {
            if (roomTemplateData == null)
            {
                Debug.LogError("RoomTemplateData is null.");
                return false;
            }

            try
            {
                roomTemplateData.Validate();
                Debug.Log($"Room template '{roomTemplateData.TemplateName}' is valid.");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Validation failed for room template '{roomTemplateData.TemplateName}': {ex.Message}");
                return false;
            }
        }
    }
}