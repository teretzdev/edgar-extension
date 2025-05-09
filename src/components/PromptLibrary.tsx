import React, { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useToast } from "@/hooks/use-toast";

type PromptTemplate = {
  id: string;
  name: string;
  content: string;
};

type PromptLibraryProps = {
  onSelectTemplate: (template: PromptTemplate) => void;
};

const PromptLibrary: React.FC<PromptLibraryProps> = ({ onSelectTemplate }) => {
  const [templates, setTemplates] = useState<PromptTemplate[]>([
    { id: "1", name: "Basic Dungeon Room", content: "Generate a basic dungeon room with minimal decorations." },
    { id: "2", name: "Treasure Room", content: "Generate a treasure room filled with gold and artifacts." },
    { id: "3", name: "Boss Room", content: "Generate a boss room with a large arena and obstacles." },
  ]);
  const [newTemplateName, setNewTemplateName] = useState("");
  const [newTemplateContent, setNewTemplateContent] = useState("");
  const [editMode, setEditMode] = useState<string | null>(null);
  const [editedTemplateName, setEditedTemplateName] = useState("");
  const [editedTemplateContent, setEditedTemplateContent] = useState("");
  const { toast } = useToast();

  const handleAddTemplate = () => {
    if (!newTemplateName.trim() || !newTemplateContent.trim()) {
      toast({
        title: "Error",
        description: "Template name and content cannot be empty.",
        variant: "destructive",
      });
      return;
    }

    const newTemplate: PromptTemplate = {
      id: Date.now().toString(),
      name: newTemplateName,
      content: newTemplateContent,
    };

    setTemplates((prevTemplates) => [...prevTemplates, newTemplate]);
    setNewTemplateName("");
    setNewTemplateContent("");

    toast({
      title: "Success",
      description: "New prompt template added successfully.",
    });
  };

  const handleSelectTemplate = (template: PromptTemplate) => {
    onSelectTemplate(template);
    toast({
      title: "Template Selected",
      description: `You selected the template: ${template.name}`,
    });
  };

  const handleSaveTemplate = (template: PromptTemplate) => {
    const confirmation = window.confirm(`Are you sure you want to save "${template.name}" to the library?`);
    if (!confirmation) return;

    // Add logic to save the template to the library
    console.log(`Saving template "${template.name}" to the library.`);

    toast({
      title: "Template Saved",
      description: `The template "${template.name}" has been saved to the library.`,
    });
  };

  const handleDeleteTemplate = (id: string) => {
    const confirmation = window.confirm("Are you sure you want to delete this template?");
    if (!confirmation) return;

    setTemplates((prevTemplates) => prevTemplates.filter((template) => template.id !== id));
    toast({
      title: "Template Deleted",
      description: "The prompt template has been deleted.",
    });
  };

  const handleEditTemplate = (id: string) => {
    if (!editedTemplateName.trim() || !editedTemplateContent.trim()) {
      toast({
        title: "Error",
        description: "Template name and content cannot be empty.",
        variant: "destructive",
      });
      return;
    }

    setTemplates((prevTemplates) =>
      prevTemplates.map((template) =>
        template.id === id
          ? { ...template, name: editedTemplateName, content: editedTemplateContent }
          : template
      )
    );
    setEditMode(null);
    setEditedTemplateName("");
    setEditedTemplateContent("");

    toast({
      title: "Success",
      description: "Template updated successfully.",
    });
  };

  return (
    <div className="max-w-3xl mx-auto p-6 bg-white shadow-md rounded-lg">
      <h1 className="text-2xl font-bold mb-4">Prompt Library</h1>
      <div className="mb-6">
        <h2 className="text-xl font-semibold mb-2">Add New Template</h2>
        <div className="grid gap-4">
          <div>
            <Label htmlFor="template-name">Template Name</Label>
            <Input
              id="template-name"
              type="text"
              placeholder="Enter template name"
              value={newTemplateName}
              onChange={(e) => setNewTemplateName(e.target.value)}
            />
          </div>
          <div>
            <Label htmlFor="template-content">Template Content</Label>
            <Input
              id="template-content"
              type="text"
              placeholder="Enter template content"
              value={newTemplateContent}
              onChange={(e) => setNewTemplateContent(e.target.value)}
            />
          </div>
          <Button onClick={handleAddTemplate}>Add Template</Button>
        </div>
      </div>
      <div>
        <h2 className="text-xl font-semibold mb-2">Available Templates</h2>
        {templates.length > 0 ? (
          <ul className="space-y-4">
            {templates.map((template) => (
              <li key={template.id} className="p-4 bg-gray-100 rounded-lg shadow-sm">
                {editMode === template.id ? (
                  <div className="grid gap-4">
                    <div>
                      <Label htmlFor="edit-template-name">Edit Template Name</Label>
                      <Input
                        id="edit-template-name"
                        type="text"
                        placeholder="Enter template name"
                        value={editedTemplateName}
                        onChange={(e) => setEditedTemplateName(e.target.value)}
                      />
                    </div>
                    <div>
                      <Label htmlFor="edit-template-content">Edit Template Content</Label>
                      <Input
                        id="edit-template-content"
                        type="text"
                        placeholder="Enter template content"
                        value={editedTemplateContent}
                        onChange={(e) => setEditedTemplateContent(e.target.value)}
                      />
                    </div>
                    <Button onClick={() => handleEditTemplate(template.id)}>Save Changes</Button>
                    <Button variant="outline" onClick={() => setEditMode(null)}>Cancel</Button>
                  </div>
                ) : (
                  <div className="flex justify-between items-center">
                    <div>
                      <h3 className="text-lg font-bold">{template.name}</h3>
                      <p className="text-gray-600">{template.content}</p>
                    </div>
                  <div className="flex space-x-2">
                    <Button variant="outline" onClick={() => handleSelectTemplate(template)}>
                      Select
                    </Button>
                    <Button variant="primary" onClick={() => handleSaveTemplate(template)}>
                      Save to Library
                    </Button>
                    <Button variant="secondary" onClick={() => setEditMode(template.id)}>
                      Edit
                    </Button>
                    <Button variant="destructive" onClick={() => handleDeleteTemplate(template.id)}>
                      Delete
                    </Button>
                  </div>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-gray-600">No templates available. Add a new template to get started.</p>
        )}
      </div>
    </div>
  );
};

export default PromptLibrary;