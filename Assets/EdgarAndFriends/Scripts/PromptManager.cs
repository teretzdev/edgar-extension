using System.Collections.Generic;
using UnityEngine;

namespace EdgarAndFriends
{
    /// <summary>
    /// Manages prompt templates and handles prompt generation for the LLM.
    /// </summary>
    public class PromptManager : MonoBehaviour
    {
        [Tooltip("List of predefined prompt templates.")]
        [SerializeField] private List<string> promptTemplates;

        [Tooltip("Default prompt template to use if no specific template is selected.")]
        [SerializeField] private string defaultPromptTemplate;

        /// <summary>
        /// Generates a prompt based on the selected template and additional parameters.
        /// </summary>
        /// <param name="templateName">The name of the template to use.</param>
        /// <param name="parameters">A dictionary of parameters to replace in the template.</param>
        /// <returns>The generated prompt.</returns>
        public string GeneratePrompt(string templateName, Dictionary<string, string> parameters)
        {
            string template = GetTemplateByName(templateName);

            if (string.IsNullOrEmpty(template))
            {
                Debug.LogWarning($"Template '{templateName}' not found. Using default template.");
                template = defaultPromptTemplate;
            }

            return ReplaceParameters(template, parameters);
        }

        /// <summary>
        /// Retrieves a prompt template by its name.
        /// </summary>
        /// <param name="templateName">The name of the template to retrieve.</param>
        /// <returns>The template string if found; otherwise, null.</returns>
        private string GetTemplateByName(string templateName)
        {
            if (promptTemplates == null || promptTemplates.Count == 0)
            {
                Debug.LogError("No prompt templates are defined.");
                return null;
            }

            foreach (var template in promptTemplates)
            {
                if (template.StartsWith(templateName + ":"))
                {
                    return template.Substring(templateName.Length + 1).Trim();
                }
            }

            return null;
        }

        /// <summary>
        /// Replaces parameters in the template with their corresponding values.
        /// </summary>
        /// <param name="template">The template string containing placeholders.</param>
        /// <param name="parameters">A dictionary of parameter names and their values.</param>
        /// <returns>The template with placeholders replaced by actual values.</returns>
        private string ReplaceParameters(string template, Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return template;
            }

            foreach (var parameter in parameters)
            {
                string placeholder = $"{{{parameter.Key}}}";
                template = template.Replace(placeholder, parameter.Value);
            }

            return template;
        }

        /// <summary>
        /// Adds a new prompt template to the list.
        /// </summary>
        /// <param name="templateName">The name of the new template.</param>
        /// <param name="templateContent">The content of the new template.</param>
        public void AddPromptTemplate(string templateName, string templateContent)
        {
            if (string.IsNullOrEmpty(templateName) || string.IsNullOrEmpty(templateContent))
            {
                Debug.LogError("Template name and content cannot be null or empty.");
                return;
            }

            string newTemplate = $"{templateName}: {templateContent}";
            promptTemplates.Add(newTemplate);
            Debug.Log($"Added new prompt template: {templateName}");
        }

        /// <summary>
        /// Removes a prompt template by its name.
        /// </summary>
        /// <param name="templateName">The name of the template to remove.</param>
        public void RemovePromptTemplate(string templateName)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                Debug.LogError("Template name cannot be null or empty.");
                return;
            }

            for (int i = 0; i < promptTemplates.Count; i++)
            {
                if (promptTemplates[i].StartsWith(templateName + ":"))
                {
                    promptTemplates.RemoveAt(i);
                    Debug.Log($"Removed prompt template: {templateName}");
                    return;
                }
            }

            Debug.LogWarning($"Template '{templateName}' not found.");
        }
    }
}
