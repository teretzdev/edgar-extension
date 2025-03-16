import React from "react";
import { Button } from "@/components/ui/button";
import { Tooltip, TooltipContent, TooltipTrigger } from "@/components/ui/tooltip";

type PromptLibraryItemProps = {
  id: string;
  name: string;
  content: string;
  onSelect: (id: string) => void;
  onEdit: (id: string) => void;
  onRemix: (id: string) => void;
  onDelete: (id: string) => void;
};

const PromptLibraryItem: React.FC<PromptLibraryItemProps> = ({
  id,
  name,
  content,
  onSelect,
  onEdit,
  onRemix,
  onDelete,
}) => {
  return (
    <div className="flex items-center justify-between p-4 bg-gray-100 rounded-lg shadow-sm">
      <div className="flex-1">
        <h3 className="text-lg font-bold text-gray-800">{name}</h3>
        <p className="text-gray-600">{content}</p>
      </div>
      <div className="flex space-x-2">
        <Tooltip>
          <TooltipTrigger asChild>
            <Button variant="outline" size="sm" onClick={() => onSelect(id)}>
              Select
            </Button>
          </TooltipTrigger>
          <TooltipContent>Select this template</TooltipContent>
        </Tooltip>
        <Tooltip>
          <TooltipTrigger asChild>
            <Button variant="secondary" size="sm" onClick={() => onEdit(id)}>
              Edit
            </Button>
          </TooltipTrigger>
          <TooltipContent>Edit this template</TooltipContent>
        </Tooltip>
        <Tooltip>
          <TooltipTrigger asChild>
            <Button variant="secondary" size="sm" onClick={() => onRemix(id)}>
              Remix
            </Button>
          </TooltipTrigger>
          <TooltipContent>Remix this template</TooltipContent>
        </Tooltip>
        <Tooltip>
          <TooltipTrigger asChild>
            <Button variant="destructive" size="sm" onClick={() => onDelete(id)}>
              Delete
            </Button>
          </TooltipTrigger>
          <TooltipContent>Delete this template</TooltipContent>
        </Tooltip>
      </div>
    </div>
  );
};

export default PromptLibraryItem;
