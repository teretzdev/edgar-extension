import React, { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useToast } from "@/hooks/use-toast";
import PromptHistory from "@/components/PromptHistory";

type RoomTemplate = {
  name: string;
  size: string;
  description: string;
};

const RoomTemplateGenerator: React.FC = () => {
  const [prompt, setPrompt] = useState("");
  const [generatedTemplate, setGeneratedTemplate] = useState<RoomTemplate | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [promptHistory, setPromptHistory] = useState<string[]>([]);
  const { toast } = useToast();

  const handleGenerateTemplate = async () => {
    if (!prompt.trim()) {
      toast({
        title: "Error",
        description: "Prompt cannot be empty.",
        variant: "destructive",
      });
      return;
    }

    setIsLoading(true);

    try {
      // Simulate API call to generate room template
      const response = await fetch("/api/generate-room-template", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ prompt }),
      });

      if (!response.ok) {
        throw new Error("Failed to generate room template.");
      }

      const data: RoomTemplate = await response.json();
      setGeneratedTemplate(data);

      // Add prompt to history
      setPromptHistory((prevHistory) => [...prevHistory, prompt]);

      toast({
        title: "Success",
        description: "Room template generated successfully.",
      });
    } catch (error) {
      console.error(error);
      toast({
        title: "Error",
        description: "Failed to generate room template. Please try again.",
        variant: "destructive",
      });
    } finally {
      setIsLoading(false);
    }
  };

  const handleSelectPrompt = (selectedPrompt: string) => {
    setPrompt(selectedPrompt);
  };

  return (
    <div className="max-w-2xl mx-auto p-6 bg-white shadow-md rounded-lg">
      <h1 className="text-2xl font-bold mb-4">Room Template Generator</h1>
      <div className="mb-4">
        <Label htmlFor="prompt">Enter Prompt</Label>
        <Input
          id="prompt"
          type="text"
          placeholder="Describe the room template you want to generate..."
          value={prompt}
          onChange={(e) => setPrompt(e.target.value)}
          disabled={isLoading}
        />
      </div>
      <Button onClick={handleGenerateTemplate} disabled={isLoading}>
        {isLoading ? "Generating..." : "Generate Template"}
      </Button>
      {generatedTemplate && (
        <div className="mt-6 p-4 bg-gray-100 rounded-lg">
          <h2 className="text-xl font-semibold mb-2">Generated Template</h2>
          <p>
            <strong>Name:</strong> {generatedTemplate.name}
          </p>
          <p>
            <strong>Size:</strong> {generatedTemplate.size}
          </p>
          <p>
            <strong>Description:</strong> {generatedTemplate.description}
          </p>
        </div>
      )}
      <div className="mt-6">
        <PromptHistory
          onSelectPrompt={(prompt) => handleSelectPrompt(prompt.content)}
          onSavePrompt={(prompt) => console.log("Save prompt:", prompt)}
        />
      </div>
    </div>
  );
};

export default RoomTemplateGenerator;