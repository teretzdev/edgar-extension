using UnityEngine;

namespace YourNamespace
{
    /// <summary>
    /// A ScriptableObject to configure prompt generation for LLM integration.
    /// </summary>
    [CreateAssetMenu(fileName = "PromptConfig", menuName = "YourNamespace/PromptConfig", order = 0)]
    public class PromptConfig : ScriptableObject
    {
        [Header("Prompt Settings")]
        [Tooltip("The base prompt text used for generation.")]
        [TextArea]
        public string basePrompt;

        [Tooltip("Additional context or instructions to append to the base prompt.")]
        [TextArea]
        public string additionalContext;

        [Tooltip("Maximum number of tokens allowed for the generated response.")]
        public int maxTokens = 256;

        [Tooltip("Temperature setting for the generation process. Higher values result in more randomness.")]
        [Range(0f, 1f)]
        public float temperature = 0.7f;

        [Tooltip("Top-p sampling value. Lower values reduce the diversity of generated responses.")]
        [Range(0f, 1f)]
        public float topP = 0.9f;

        [Tooltip("Frequency penalty to discourage repetition in the generated response.")]
        [Range(0f, 2f)]
        public float frequencyPenalty = 0f;

        [Tooltip("Presence penalty to encourage the inclusion of new topics in the generated response.")]
        [Range(0f, 2f)]
        public float presencePenalty = 0f;

        /// <summary>
        /// Generates the final prompt by combining the base prompt and additional context.
        /// </summary>
        /// <returns>The combined prompt string.</returns>
        public string GeneratePrompt()
        {
            if (string.IsNullOrEmpty(basePrompt))
            {
                Debug.LogWarning("Base prompt is empty. Returning an empty prompt.");
                return string.Empty;
            }

            if (string.IsNullOrEmpty(additionalContext))
            {
                return basePrompt;
            }

            return $"{basePrompt}\\n\\n{additionalContext}";
        }
    }
}