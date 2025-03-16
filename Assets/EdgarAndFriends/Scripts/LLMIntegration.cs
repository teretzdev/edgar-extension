using UnityEngine;

namespace YourNamespace
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
            return $"Response from LLM API for prompt: {prompt}";
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

            // Process the response (e.g., display it in the UI, use it in gameplay logic, etc.)
            Debug.Log($"Received response from LLM: {response}");
        }
    }
}
