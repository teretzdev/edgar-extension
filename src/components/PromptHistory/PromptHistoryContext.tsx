import React, { createContext, useContext, useState, useCallback } from "react";

type PromptHistoryItem = {
  id: string;
  content: string;
};

type PromptHistoryContextType = {
  history: PromptHistoryItem[];
  addPrompt: (content: string) => void;
  removePrompt: (id: string) => void;
  clearHistory: () => void;
  savePromptToLibrary: (prompt: PromptHistoryItem) => void;
};

const PromptHistoryContext = createContext<PromptHistoryContextType | undefined>(undefined);

export const usePromptHistory = (): PromptHistoryContextType => {
  const context = useContext(PromptHistoryContext);
  if (!context) {
    throw new Error("usePromptHistory must be used within a PromptHistoryProvider");
  }
  return context;
};

export const PromptHistoryProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
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

  const savePromptToLibrary = useCallback((prompt: PromptHistoryItem) => {
    console.log(`Saving prompt to library: ${prompt.content}`);
    // Add logic to save the prompt to the library
  }, []);

  return (
    <PromptHistoryContext.Provider
      value={{ history, addPrompt, removePrompt, clearHistory, savePromptToLibrary }}
    >
      {children}
    </PromptHistoryContext.Provider>
  );
};
