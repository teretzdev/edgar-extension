import React, { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useToast } from "@/hooks/use-toast";
import { RoomTemplate } from "@/types/roomTemplate";
import LLMService from "@/services/llmService";

type EdgarIntegrationProps = {
  onTemplatesUpdated: (templates: RoomTemplate[]) => void;
};

const EdgarIntegration: React.FC<EdgarIntegrationProps> = ({ onTemplatesUpdated }) => {
  const [templates, setTemplates] = useState<RoomTemplate[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [templateName, setTemplateName] = useState("");
  const [templateSize, setTemplateSize] = useState({ width: 0, height: 0 });
  const [templateDescription, setTemplateDescription] = useState("");
  const { toast } = useToast();

  const handleSendTemplatesToEdgar = async () => {
    if (templates.length === 0) {
      toast({
        title: "Error",
        description: "No templates to send to Edgar.",
        variant: "destructive",
      });
      return;
    }

    setIsLoading(true);

    try {
      // Send templates to Edgar using direct function calls
      const edgarService = await import("@/services/edgarService");
      await edgarService.default.sendTemplatesToEdgar({ templates });

      toast({
        title: "Success",
        description: "Templates sent to Edgar successfully.",
      });
    } catch (error) {
      console.error(error);
      toast({
        title: "Error",
        description: "Failed to send templates to Edgar. Please try again.",
        variant: "destructive",
      });
    } finally {
      setIsLoading(false);
    }
  };

  const handleReceiveProcessedTemplates = async () => {
    setIsLoading(true);

    try {
      // Fetch processed templates from Edgar using direct function calls
      const edgarService = await import("@/services/edgarService");
      const response = await edgarService.default.fetchProcessedTemplates();

      setTemplates(response.processedTemplates);
      onTemplatesUpdated(response.processedTemplates);

      toast({
        title: "Success",
        description: "Processed templates received from Edgar.",
      });
    } catch (error) {
      console.error(error);
      toast({
        title: "Error",
        description: "Failed to receive processed templates from Edgar. Please try again.",
        variant: "destructive",
      });
    } finally {
      setIsLoading(false);
    }
  };

  const handleAddTemplate = () => {
    if (!templateName.trim() || templateSize.width <= 0 || templateSize.height <= 0 || !templateDescription.trim()) {
      toast({
        title: "Error",
        description: "All fields are required and size must be positive.",
        variant: "destructive",
      });
      return;
    }

    const newTemplate: RoomTemplate = {
      name: templateName,
      size: templateSize,
      description: templateDescription,
    };

    setTemplates((prevTemplates) => [...prevTemplates, newTemplate]);
    setTemplateName("");
    setTemplateSize({ width: 0, height: 0 });
    setTemplateDescription("");

    toast({
      title: "Success",
      description: "Template added successfully.",
    });
  };

  return (
    <div className="max-w-3xl mx-auto p-6 bg-white shadow-md rounded-lg">
      <h1 className="text-2xl font-bold mb-4">Edgar Integration</h1>
      <div className="mb-6">
        <h2 className="text-xl font-semibold mb-2">Add New Template</h2>
        <div className="grid gap-4">
          <div>
            <Label htmlFor="template-name">Template Name</Label>
            <Input
              id="template-name"
              type="text"
              placeholder="Enter template name"
              value={templateName}
              onChange={(e) => setTemplateName(e.target.value)}
            />
          </div>
          <div>
            <Label htmlFor="template-size">Template Size</Label>
            <div className="flex space-x-2">
              <Input
                id="template-width"
                type="number"
                placeholder="Width"
                value={templateSize.width}
                onChange={(e) => setTemplateSize({ ...templateSize, width: parseInt(e.target.value, 10) })}
              />
              <Input
                id="template-height"
                type="number"
                placeholder="Height"
                value={templateSize.height}
                onChange={(e) => setTemplateSize({ ...templateSize, height: parseInt(e.target.value, 10) })}
              />
            </div>
          </div>
          <div>
            <Label htmlFor="template-description">Template Description</Label>
            <Input
              id="template-description"
              type="text"
              placeholder="Enter template description"
              value={templateDescription}
              onChange={(e) => setTemplateDescription(e.target.value)}
            />
          </div>
          <Button onClick={handleAddTemplate}>Add Template</Button>
        </div>
      </div>
      <div className="mb-6">
        <h2 className="text-xl font-semibold mb-2">Templates</h2>
        {templates.length > 0 ? (
          <ul className="space-y-4">
            {templates.map((template, index) => (
              <li key={index} className="p-4 bg-gray-100 rounded-lg shadow-sm">
                <h3 className="text-lg font-bold">{template.name}</h3>
                <p className="text-gray-600">Size: {template.size.width}x{template.size.height}</p>
                <p className="text-gray-600">{template.description}</p>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-gray-600">No templates available. Add a new template to get started.</p>
        )}
      </div>
      <div className="flex space-x-4">
        <Button onClick={handleSendTemplatesToEdgar} disabled={isLoading}>
          {isLoading ? "Sending..." : "Send to Edgar"}
        </Button>
        <Button onClick={handleReceiveProcessedTemplates} disabled={isLoading}>
          {isLoading ? "Receiving..." : "Receive from Edgar"}
        </Button>
      </div>
    </div>
  );
};

export default EdgarIntegration;