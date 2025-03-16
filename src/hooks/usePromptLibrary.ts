import { useContext } from "react";
import { PromptLibraryContext } from "@/components/PromptLibrary/PromptLibraryContext";

/**
 * Custom hook for managing the prompt library state, including saving, editing, and remixing prompts.
 */
const usePromptLibrary = () => {
  const context = useContext(PromptLibraryContext);

  if (!context) {
    throw new Error("usePromptLibrary must be used within a PromptLibraryProvider");
  }

  const { prompts, addPrompt, editPrompt, deletePrompt } = context;

  /**
   * Saves a new prompt to the library.
   * @param name - The name of the prompt.
   * @param content - The content of the prompt.
   */
  const savePrompt = (name: string, content: string) => {
    if (!name.trim() || !content.trim()) {
      console.error("Prompt name and content cannot be empty.");
      return;
    }
    addPrompt(name, content);
  };

  /**
   * Edits an existing prompt in the library.
   * @param id - The ID of the prompt to edit.
   * @param name - The new name of the prompt.
   * @param content - The new content of the prompt.
   */
  const updatePrompt = (id: string, name: string, content: string) => {
    if (!id || !name.trim() || !content.trim()) {
      console.error("Prompt ID, name, and content cannot be empty.");
      return;
    }
    editPrompt(id, name, content);
  };

  /**
   * Deletes a prompt from the library.
   * @param id - The ID of the prompt to delete.
   */
  const removePrompt = (id: string) => {
    if (!id) {
      console.error("Prompt ID cannot be empty.");
      return;
    }
    deletePrompt(id);
  };

  /**
   * Remixes an existing prompt by appending a remix tag to its content.
   * @param id - The ID of the prompt to remix.
   */
  const remixPrompt = (id: string) => {
    const prompt = prompts.find((p) => p.id === id);
    if (!prompt) {
      console.error("Prompt not found.");
      return;
    }
    const remixedContent = `${prompt.content} [Remixed]`;
    editPrompt(id, prompt.name, remixedContent);
  };

  return {
    prompts,
    savePrompt,
    updatePrompt,
    removePrompt,
    remixPrompt,
  };
};

export default usePromptLibrary;
