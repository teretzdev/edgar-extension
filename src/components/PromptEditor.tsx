import React, { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useToast } from "@/hooks/use-toast";

type PromptEditorProps = {
  initialContent?: string;
  onSave: (content: string) => void;
  onCancel: () => void;
};

const PromptEditor: React.FC<PromptEditorProps> = ({
  initialContent = "",
  onSave,
  onCancel,
}) => {
  const [content, setContent] = useState(initialContent);
  const { toast } = useToast();

  const handleSave = () => {
    if (!content.trim()) {
      toast({
        title: "Error",
        description: "Prompt content cannot be empty.",
        variant: "destructive",
      });
      return;
    }

    onSave(content);
    toast({
      title: "Success",
      description: "Prompt saved successfully.",
    });
  };

  return (
    <div className="max-w-2xl mx-auto p-6 bg-white shadow-md rounded-lg">
      <h1 className="text-2xl font-bold mb-4">Edit Prompt</h1>
      <div className="mb-4">
        <Label htmlFor="prompt-editor">Prompt Content</Label>
        <Input
          id="prompt-editor"
          type="text"
          placeholder="Enter your prompt content..."
          value={content}
          onChange={(e) => setContent(e.target.value)}
        />
      </div>
      <div className="flex space-x-4">
        <Button variant="primary" onClick={handleSave}>
          Save
        </Button>
        <Button variant="outline" onClick={onCancel}>
          Cancel
        </Button>
      </div>
    </div>
  );
};

export default PromptEditor;
