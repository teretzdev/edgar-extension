import React, { useState } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { useToast } from "@/hooks/use-toast";

type SavePromptDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  onSave: (name: string) => void;
};

const SavePromptDialog: React.FC<SavePromptDialogProps> = ({ isOpen, onClose, onSave }) => {
  const [promptName, setPromptName] = useState("");
  const { toast } = useToast();

  const handleSave = () => {
    if (!promptName.trim()) {
      toast({
        title: "Error",
        description: "Prompt name cannot be empty.",
        variant: "destructive",
      });
      return;
    }

    onSave(promptName);
    toast({
      title: "Success",
      description: `Prompt "${promptName}" has been saved to the library.`,
    });
    setPromptName("");
    onClose();
  };

  const handleCancel = () => {
    setPromptName("");
    onClose();
  };

  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Save Prompt</DialogTitle>
        </DialogHeader>
        <div className="space-y-4">
          <Input
            id="prompt-name"
            type="text"
            placeholder="Enter prompt name"
            value={promptName}
            onChange={(e) => setPromptName(e.target.value)}
          />
        </div>
        <DialogFooter>
          <Button variant="outline" onClick={handleCancel}>
            Cancel
          </Button>
          <Button variant="primary" onClick={handleSave}>
            Save
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default SavePromptDialog;