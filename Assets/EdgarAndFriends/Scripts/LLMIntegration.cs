using UnityEngine;
using UnityEngine.Networking;

namespace EdgarAndFriends
{
    /// <summary>
    /// Handles integration with a Large Language Model (LLM) using a configurable prompt system.
    /// </summary>
    public class LLMIntegration : MonoBehaviour
    {
        [Header("Configuration")]
        [Tooltip("The PromptConfig ScriptableObject used to configure prompt generation.")]
        [SerializeField] private PromptConfig promptConfig;

        /// <summary>
        /// Sends a request to the LLM using the configured prompt.
        /// </summary>
        public void SendRequestToLLM()
        {
            if (promptConfig == null)
            {
                Debug.LogError("PromptConfig is not assigned. Please assign a PromptConfig ScriptableObject.");
                return;
            }

            string prompt = promptConfig.GeneratePrompt();
            if (string.IsNullOrEmpty(prompt))
            {
                Debug.LogError("Generated prompt is empty. Check the PromptConfig settings.");
                return;
            }

            // Send the prompt to the LLM API
            Debug.Log($"Sending prompt to LLM API: {prompt}");

            // Example: Receive a response from the LLM API
            string response = SendRequestToLLMApi(prompt);
            HandleLLMResponse(response);
        }

        /// <summary>
        /// Sends a request to the LLM API and handles the response.
        /// </summary>
        /// <param name="prompt">The prompt sent to the LLM.</param>
        /// <returns>The response from the LLM API.</returns>
        private string SendRequestToLLMApi(string prompt)
        {
            // Replace this with actual API call logic.
            // Example: Use UnityWebRequest or a third-party library to send the prompt to the LLM API.
            Debug.Log("Sending request to LLM API...");
            
            using (UnityWebRequest webRequest = UnityWebRequest.Post("https://api.llm-service.com/generate", prompt))
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes($"{{\"prompt\": \"{prompt}\"}}"));
                webRequest.downloadHandler = new DownloadHandlerBuffer();

                var operation = webRequest.SendWebRequest();
                while (!operation.isDone) { }

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Received response from LLM API.");
                    return webRequest.downloadHandler.text;
                }
                else
                {
                    Debug.LogError($"Error sending request to LLM API: {webRequest.error}");
                    return null;
                }
            }
        }

        /// <summary>
        /// Handles the response received from the LLM.
        /// </summary>
        /// <param name="response">The response from the LLM.</param>
        private void HandleLLMResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Debug.LogWarning("Received an empty response from the LLM.");
                return;
            }

            // Parse the response into RoomTemplateData
            RoomTemplateParser parser = new RoomTemplateParser();
            RoomTemplateData roomTemplateData = parser.ParseResponse(response);

            if (roomTemplateData == null)
            {
                Debug.LogError("Failed to parse LLM response into RoomTemplateData.");
                return;
            }

            // Example: Add the parsed room template to the RoomTemplateManager
            RoomTemplateManager roomTemplateManager = FindObjectOfType<RoomTemplateManager>();
            if (roomTemplateManager != null)
            {
                roomTemplateManager.AddRoomTemplate(new RoomTemplate(
                    roomTemplateData.TemplateName,
                    roomTemplateData.TemplateSize,
                    roomTemplateData.TemplatePrefab
                ));
                Debug.Log($"Room template '{roomTemplateData.TemplateName}' added to RoomTemplateManager.");
            }
            else
            {
                Debug.LogError("RoomTemplateManager not found in the scene.");
            }
        }
    }
}