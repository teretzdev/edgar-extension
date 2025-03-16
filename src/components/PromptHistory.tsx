import React, { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useToast } from "@/hooks/use-toast";

type PromptHistoryItem = {
  id: string;
  content: string;
};

type PromptHistoryProps = {
  onSelectPrompt: (prompt: PromptHistoryItem) => void;
  onSavePrompt: (prompt: PromptHistoryItem) => void;
};

const PromptHistory: React.FC<PromptHistoryProps> = ({ onSelectPrompt, onSavePrompt }) => {
  const [history, setHistory] = useState<PromptHistoryItem[]>([]);
  const [newPrompt, setNewPrompt] = useState("");
  const { toast } = useToast();

  const handleAddToHistory = () => {
    if (!newPrompt.trim()) {
      toast({
        title: "Error",
        description: "Prompt cannot be empty.",
        variant: "destructive",
      });
      return;
    }

    const newHistoryItem: PromptHistoryItem = {
      id: Date.now().toString(),
      content: newPrompt,
    };

    setHistory((prevHistory) => [...prevHistory, newHistoryItem]);
    setNewPrompt("");

    toast({
      title: "Success",
      description: "Prompt added to history.",
    });
  };

  const handleSelectPrompt = (prompt: PromptHistoryItem) => {
    onSelectPrompt(prompt);
    toast({
      title: "Prompt Selected",
      description: `You selected the prompt: ${prompt.content}`,
    });
  };

  const handleSavePrompt = (prompt: PromptHistoryItem) => {
    onSavePrompt(prompt);
    toast({
      title: "Prompt Saved",
      description: `The prompt "${prompt.content}" has been saved to the library.`,
    });

    toast({
      title: "Remix Prompt",
      description: `The prompt "${prompt.content}" has been remixed successfully.`,
    });
  };

  return (
    <div className="max-w-3xl mx-auto p-6 bg-white shadow-md rounded-lg">
      <h1 className="text-2xl font-bold mb-4">Prompt History</h1>
      <div className="mb-6">
        <h2 className="text-xl font-semibold mb-2">Add New Prompt</h2>
        <div className="grid gap-4">
          <div>
            <Label htmlFor="new-prompt">New Prompt</Label>
            <Input
              id="new-prompt"
              type="text"
              placeholder="Enter a new prompt"
              value={newPrompt}
              onChange={(e) => setNewPrompt(e.target.value)}
            />
          </div>
          <Button onClick={handleAddToHistory}>Add to History</Button>
        </div>
      </div>
      <div>
        <h2 className="text-xl font-semibold mb-2">Prompt History</h2>
        {history.length > 0 ? (
          <ul className="space-y-4">
            {history.map((item) => (
              <li key={item.id} className="p-4 bg-gray-100 rounded-lg shadow-sm">
                <div className="flex justify-between items-center">
                  <div>
                    <p className="text-gray-600">{item.content}</p>
                  </div>
                  <div className="flex space-x-2">
                    <Button variant="outline" onClick={() => handleSelectPrompt(item)}>
                      Select
                    </Button>
                    <Button variant="primary" onClick={() => handleSavePrompt(item)}>
                      Save to Library
                    </Button>
                    <Button variant="secondary" onClick={() => handleRemixPrompt(item)}>
                      Remix
                    </Button>
                  </div>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-gray-600">No prompts in history. Add a new prompt to get started.</p>
        )}
      </div>
    </div>
  );
};

export default PromptHistory;