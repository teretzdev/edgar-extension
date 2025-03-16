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
        /// Validates the LLM response to ensure it meets the required criteria.
        /// </summary>
        /// <param name="response">The response from the LLM.</param>
        /// <returns>True if the response is valid; otherwise, false.</returns>
        public bool ValidateResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Debug.LogError("LLM response is null or empty.");
                return false;
            }

            // Example validation: Check if the response contains prohibited content
            if (ContainsProhibitedContent(response))
            {
                Debug.LogError("LLM response contains prohibited content.");
                return false;
            }

            Debug.Log("LLM response is valid.");
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
                Debug.LogWarning("LLM response is null or empty. Returning an empty string.");
                return string.Empty;
            }

            // Example sanitization: Remove HTML tags
            string sanitizedResponse = RemoveHtmlTags(response);

            // Additional sanitization logic can be added here
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