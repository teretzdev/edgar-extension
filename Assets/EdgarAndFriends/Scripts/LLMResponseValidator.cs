using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Validates and sanitizes responses from a Large Language Model (LLM).
    /// </summary>
    public class LLMResponseValidator : MonoBehaviour
    {
        /// <summary>
        /// Validates and sanitizes the LLM response to ensure it meets the required criteria.
        /// </summary>
        /// <param name="response">The response from the LLM.</param>
        /// <returns>True if the response is valid; otherwise, false. Logs errors for invalid responses.</returns>
        public bool ValidateResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Debug.LogError("LLM response is null or empty.");
                return false;
            }

            // Example validation: Check if the response contains prohibited content or invalid JSON
            if (ContainsProhibitedContent(response))
            {
                Debug.LogError("LLM response contains prohibited content.");
                return false;
            }

            Debug.Log("LLM response passed validation.");
            return true;
        }

        /// <summary>
        /// Sanitizes the LLM response by removing unwanted or unsafe content.
        /// </summary>
        /// <param name="response">The response from the LLM.</param>
        /// <returns>The sanitized response.</returns>
        public string SanitizeResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Debug.LogWarning("LLM response is null or empty. Returning sanitized empty string.");
                return string.Empty;
            }

            // Example sanitization: Remove HTML tags and trim whitespace
            string sanitizedResponse = RemoveHtmlTags(response);

            // Additional sanitization logic: Ensure no prohibited content remains
            if (ContainsProhibitedContent(sanitizedResponse))
            {
                Debug.LogWarning("Sanitized response still contains prohibited content. Returning empty string.");
                return string.Empty;
            }
            Debug.Log("LLM response sanitized successfully.");
            return sanitizedResponse;
        }

        /// <summary>
        /// Checks if the response contains prohibited content.
        /// </summary>
        /// <param name="response">The response to check.</param>
        /// <returns>True if prohibited content is found; otherwise, false.</returns>
        private bool ContainsProhibitedContent(string response)
        {
            // Example: Check for prohibited words or phrases
            string[] prohibitedWords = { "prohibitedWord1", "prohibitedWord2" };
            foreach (var word in prohibitedWords)
            {
                if (response.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes HTML tags from the given string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The string without HTML tags.</returns>
        private string RemoveHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}