import React, { createContext, useContext, useState, useCallback } from "react";

type Prompt = {
  id: string;
  name: string;
  content: string;
};

type PromptLibraryContextType = {
  prompts: Prompt[];
  addPrompt: (name: string, content: string) => void;
  editPrompt: (id: string, name: string, content: string) => void;
  deletePrompt: (id: string) => void;
};

const PromptLibraryContext = createContext<PromptLibraryContextType | undefined>(undefined);

export const usePromptLibrary = (): PromptLibraryContextType => {
  const context = useContext(PromptLibraryContext);
  if (!context) {
    throw new Error("usePromptLibrary must be used within a PromptLibraryProvider");
  }
  return context;
};

export const PromptLibraryProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [prompts, setPrompts] = useState<Prompt[]>([]);

  const addPrompt = useCallback((name: string, content: string) => {
    const newPrompt: Prompt = {
      id: Date.now().toString(),
      name,
      content,
    };
    setPrompts((prevPrompts) => [...prevPrompts, newPrompt]);
  }, []);

  const editPrompt = useCallback((id: string, name: string, content: string) => {
    setPrompts((prevPrompts) =>
      prevPrompts.map((prompt) =>
        prompt.id === id ? { ...prompt, name, content } : prompt
      )
    );
  }, []);

  const deletePrompt = useCallback((id: string) => {
    setPrompts((prevPrompts) => prevPrompts.filter((prompt) => prompt.id !== id));
  }, []);

  return (
    <PromptLibraryContext.Provider value={{ prompts, addPrompt, editPrompt, deletePrompt }}>
      {children}
    </PromptLibraryContext.Provider>
  );
};
