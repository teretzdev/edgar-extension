import React, { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useToast } from "@/hooks/use-toast";
import usePromptHistory from "@/hooks/usePromptHistory";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";

const PromptHistory: React.FC = () => {
  const { history, addPrompt, remixPrompt, savePromptToLibrary } = usePromptHistory();
  const [newPrompt, setNewPrompt] = useState("");
  const [remixDialogOpen, setRemixDialogOpen] = useState(false);
  const [selectedPromptId, setSelectedPromptId] = useState<string | null>(null);
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

    addPrompt(newPrompt);
    setNewPrompt("");

    toast({
      title: "Success",
      description: "Prompt added to history.",
    });
  };

  const handleRemixPrompt = (id: string) => {
    setSelectedPromptId(id);
    setRemixDialogOpen(true);
  };

  const confirmRemixPrompt = () => {
    if (selectedPromptId) {
      remixPrompt(selectedPromptId);
      toast({
        title: "Prompt Remixed",
        description: "The prompt has been remixed successfully.",
      });
    }
    setRemixDialogOpen(false);
    setSelectedPromptId(null);
  };

  const handleSavePrompt = (id: string) => {
    savePromptToLibrary(id);
    toast({
      title: "Prompt Saved",
      description: "The prompt has been saved to the library.",
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
                    <Button variant="secondary" onClick={() => handleRemixPrompt(item.id)}>
                      Remix
                    </Button>
                    <Button variant="primary" onClick={() => handleSavePrompt(item.id)}>
                      Save to Library
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
      <Dialog open={remixDialogOpen} onOpenChange={setRemixDialogOpen}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Confirm Remix</DialogTitle>
          </DialogHeader>
          <p>Are you sure you want to remix this prompt?</p>
          <div className="flex justify-end space-x-2 mt-4">
            <Button variant="outline" onClick={() => setRemixDialogOpen(false)}>
              Cancel
            </Button>
            <Button variant="primary" onClick={confirmRemixPrompt}>
              Confirm
            </Button>
          </div>
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default PromptHistory;