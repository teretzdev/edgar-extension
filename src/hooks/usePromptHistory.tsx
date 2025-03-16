import { useState, useEffect, useCallback } from "react";

type PromptHistoryItem = {
  id: string;
  content: string;
};

type UsePromptHistoryReturn = {
  history: PromptHistoryItem[];
  addPrompt: (content: string) => void;
  removePrompt: (id: string) => void;
  clearHistory: () => void;
  remixPrompt: (id: string) => void;
  savePromptToLibrary: (prompt: PromptHistoryItem) => void;
};

const LOCAL_STORAGE_KEY = "promptHistory";

const usePromptHistory = (): UsePromptHistoryReturn => {
  const [history, setHistory] = useState<PromptHistoryItem[]>(() => {
    const storedHistory = localStorage.getItem(LOCAL_STORAGE_KEY);
    return storedHistory ? JSON.parse(storedHistory) : [];
  });

  useEffect(() => {
    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(history));
  }, [history]);

  const addPrompt = useCallback((content: string) => {
    if (!content.trim()) {
      console.error("Cannot add an empty prompt to history.");
      return;
    }

    const newPrompt: PromptHistoryItem = {
      id: Date.now().toString(),
      content,
    };

    setHistory((prevHistory) => [...prevHistory, newPrompt]);
  }, []);

  const removePrompt = useCallback((id: string) => {
    setHistory((prevHistory) => prevHistory.filter((item) => item.id !== id));
  }, []);

  const clearHistory = useCallback(() => {
    setHistory([]);
  }, []);

  const remixPrompt = useCallback(
    (id: string) => {
      const prompt = history.find((item) => item.id === id);
      if (!prompt) {
        console.error("Prompt not found.");
        return;
      }
      const remixedContent = `${prompt.content} [Remixed]`;
      addPrompt(remixedContent);
    },
    [history, addPrompt]
  );

  const savePromptToLibrary = useCallback((prompt: PromptHistoryItem) => {
    console.log(`Saving prompt to library: ${prompt.content}`);
    // Add logic to save the prompt to the library
  }, []);

  return {
    history,
    addPrompt,
    removePrompt,
    clearHistory,
    remixPrompt,
    savePromptToLibrary,
  };
};

export default usePromptHistory;
