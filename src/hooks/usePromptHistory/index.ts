import { useState, useCallback, useEffect } from "react";

type PromptHistoryItem = {
  id: string;
  content: string;
};

type UsePromptHistoryReturn = {
  history: PromptHistoryItem[];
  addPrompt: (content: string) => void;
  selectPrompt: (id: string) => PromptHistoryItem | undefined;
  remixPrompt: (id: string) => void;
  savePromptToLibrary: (id: string) => void;
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

  const selectPrompt = useCallback(
    (id: string): PromptHistoryItem | undefined => {
      return history.find((item) => item.id === id);
    },
    [history]
  );

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

  const savePromptToLibrary = useCallback((id: string) => {
    const prompt = history.find((item) => item.id === id);
    if (!prompt) {
      console.error("Prompt not found.");
      return;
    }
    console.log(`Saving prompt to library: ${prompt.content}`);
    // Add logic to save the prompt to the library
  }, [history]);

  return {
    history,
    addPrompt,
    selectPrompt,
    remixPrompt,
    savePromptToLibrary,
  };
};

export default usePromptHistory;
