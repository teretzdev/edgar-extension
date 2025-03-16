import React from "react";
import { Button } from "@/components/ui/button";
import { Tooltip, TooltipContent, TooltipTrigger } from "@/components/ui/tooltip";

type PromptHistoryItemProps = {
  id: string;
  content: string;
  onSelect: (id: string) => void;
  onRemix: (id: string) => void;
  onSaveToLibrary: (id: string) => void;
};

const PromptHistoryItem: React.FC<PromptHistoryItemProps> = ({
  id,
  content,
  onSelect,
  onRemix,
  onSaveToLibrary,
}) => {
  return (
    <div className="flex items-center justify-between p-4 bg-gray-100 rounded-lg shadow-sm">
      <div className="flex-1">
        <p className="text-gray-800">{content}</p>
      </div>
      <div className="flex space-x-2">
        <Tooltip>
          <TooltipTrigger asChild>
            <Button variant="outline" size="sm" onClick={() => onSelect(id)}>
              Select
            </Button>
          </TooltipTrigger>
          <TooltipContent>Select this prompt</TooltipContent>
        </Tooltip>
        <Tooltip>
          <TooltipTrigger asChild>
            <Button variant="secondary" size="sm" onClick={() => onRemix(id)}>
              Remix
            </Button>
          </TooltipTrigger>
          <TooltipContent>Remix this prompt</TooltipContent>
        </Tooltip>
        <Tooltip>
          <TooltipTrigger asChild>
            <Button variant="primary" size="sm" onClick={() => onSaveToLibrary(id)}>
              Save
            </Button>
          </TooltipTrigger>
          <TooltipContent>Save this prompt to the library</TooltipContent>
        </Tooltip>
      </div>
    </div>
  );
};

export default PromptHistoryItem;
