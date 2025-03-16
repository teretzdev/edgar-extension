import { useState, useCallback } from "react";

type PromptHistoryItem = {
  id: string;
  content: string;
};

type UsePromptHistoryReturn = {
  history: PromptHistoryItem[];
  addPrompt: (content: string) => void;
  removePrompt: (id: string) => void;
  clearHistory: () => void;
};

const usePromptHistory = (): UsePromptHistoryReturn => {
  const [history, setHistory] = useState<PromptHistoryItem[]>([]);

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

  return {
    history,
    addPrompt,
    removePrompt,
    clearHistory,
  };
};

export default usePromptHistory;
