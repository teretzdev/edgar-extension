import React, { useState } from "react";
import RoomTemplateGenerator from "@/components/RoomTemplateGenerator";
import DungeonVisualizer from "@/components/DungeonVisualizer";
import PromptLibrary from "@/components/PromptLibrary";
import PromptHistory from "@/components/PromptHistory";
import EdgarIntegration from "@/components/EdgarIntegration";
import useRoomTemplates from "@/hooks/useRoomTemplates";
import { RoomTemplate } from "@/types/roomTemplate";

const WorkflowDemo: React.FC = () => {
  const {
    roomTemplates,
    addRoomTemplate,
    deleteRoomTemplate,
    clearSelectedTemplate,
  } = useRoomTemplates();
  const [selectedPrompt, setSelectedPrompt] = useState<string | null>(null);

  const handlePromptSelect = (prompt: { id: string; content: string }) => {
    setSelectedPrompt(prompt.content);
  };

  const handleRemixPrompt = (prompt: { id: string; content: string }) => {
    const remixedContent = `Remix of: ${prompt.content}`;
    setSelectedPrompt(remixedContent);
    usePromptHistory().addPrompt(remixedContent);
    console.log(`Prompt remixed: ${remixedContent}`);
  };

  const handleTemplateGenerated = (template: RoomTemplate) => {
    addRoomTemplate(template);
  };

  const handleTemplatesUpdated = (templates: RoomTemplate[]) => {
    templates.forEach((template) => addRoomTemplate(template));
  };

  const handleSavePromptToLibrary = (prompt: { id: string; content: string }) => {
    usePromptLibrary().addPrompt(prompt.id, prompt.content);
    console.log(`Prompt saved to library: ${prompt.content}`);
  };

  return (
    <div className="max-w-5xl mx-auto p-6 bg-gray-50 shadow-md rounded-lg">
      <h1 className="text-3xl font-bold mb-6 text-center">
        Workflow Demo: From Prompt to Dungeon
      </h1>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div className="bg-white p-4 shadow rounded-lg">
          <h2 className="text-xl font-semibold mb-4">Prompt Library</h2>
          <PromptLibrary onSelectTemplate={handlePromptSelect} />
        </div>
        <div className="bg-white p-4 shadow rounded-lg">
          <h2 className="text-xl font-semibold mb-4">Room Template Generator</h2>
          <RoomTemplateGenerator
            onTemplateGenerated={handleTemplateGenerated}
            initialPrompt={selectedPrompt || ""}
          />
        </div>
      </div>
      <div className="mt-6 bg-white p-4 shadow rounded-lg">
        <h2 className="text-xl font-semibold mb-4">Prompt History</h2>
        <PromptHistory
          onSelectPrompt={handlePromptSelect}
          onSavePrompt={handleSavePromptToLibrary}
          onRemixPrompt={handleRemixPrompt}
        />
      </div>
      <div className="mt-6 bg-white p-4 shadow rounded-lg">
        <h2 className="text-xl font-semibold mb-4">Dungeon Visualizer</h2>
        <DungeonVisualizer templates={roomTemplates} />
      </div>
      <div className="mt-6 bg-white p-4 shadow rounded-lg">
        <h2 className="text-xl font-semibold mb-4">Edgar Integration</h2>
        <EdgarIntegration onTemplatesUpdated={handleTemplatesUpdated} />
      </div>
      <div className="mt-6 text-center">
        <button
          className="bg-red-500 text-white px-6 py-3 rounded-lg shadow-md hover:bg-red-600"
          onClick={() => {
            deleteRoomTemplate("");
            clearSelectedTemplate();
          }}
        >
          Clear All Templates
        </button>
      </div>
    </div>
  );
};

export default WorkflowDemo;