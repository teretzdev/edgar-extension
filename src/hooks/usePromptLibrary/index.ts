import { useState, useCallback } from "react";

type Prompt = {
  id: string;
  name: string;
  content: string;
};

type UsePromptLibraryReturn = {
  prompts: Prompt[];
  addPrompt: (name: string, content: string) => void;
  editPrompt: (id: string, name: string, content: string) => void;
  deletePrompt: (id: string) => void;
  remixPrompt: (id: string) => void;
};

/**
 * Custom hook for managing the prompt library, including functions for adding, editing, remixing, and deleting prompts.
 */
const usePromptLibrary = (): UsePromptLibraryReturn => {
  const [prompts, setPrompts] = useState<Prompt[]>([]);

  const addPrompt = useCallback((name: string, content: string) => {
    if (!name.trim() || !content.trim()) {
      console.error("Prompt name and content cannot be empty.");
      return;
    }

    const newPrompt: Prompt = {
      id: Date.now().toString(),
      name,
      content,
    };

    setPrompts((prevPrompts) => [...prevPrompts, newPrompt]);
  }, []);

  const editPrompt = useCallback((id: string, name: string, content: string) => {
    if (!id || !name.trim() || !content.trim()) {
      console.error("Prompt ID, name, and content cannot be empty.");
      return;
    }

    setPrompts((prevPrompts) =>
      prevPrompts.map((prompt) =>
        prompt.id === id ? { ...prompt, name, content } : prompt
      )
    );
  }, []);

  const deletePrompt = useCallback((id: string) => {
    if (!id) {
      console.error("Prompt ID cannot be empty.");
      return;
    }

    setPrompts((prevPrompts) => prevPrompts.filter((prompt) => prompt.id !== id));
  }, []);

  const remixPrompt = useCallback((id: string) => {
    const prompt = prompts.find((p) => p.id === id);
    if (!prompt) {
      console.error("Prompt not found.");
      return;
    }

    const remixedContent = `${prompt.content} [Remixed]`;
    const remixedPrompt: Prompt = {
      id: Date.now().toString(),
      name: `${prompt.name} (Remixed)`,
      content: remixedContent,
    };

    setPrompts((prevPrompts) => [...prevPrompts, remixedPrompt]);
  }, [prompts]);

  return {
    prompts,
    addPrompt,
    editPrompt,
    deletePrompt,
    remixPrompt,
  };
};

export default usePromptLibrary;
