export type RoomTemplate = {
  name: string;
  size: {
    width: number;
    height: number;
  };
  description: string;
  prefab?: string; // Optional field for prefab reference
};

export type RoomTemplateData = {
  templates: RoomTemplate[];
};

export type LLMRequestPayload = {
  prompt: string;
};

export type LLMResponse = {
  name: string;
  size: {
    width: number;
    height: number;
  };
  description: string;
  prefab?: string;
};

export type RoomTemplateManagerState = {
  templates: RoomTemplate[];
  selectedTemplate?: RoomTemplate; // Optional field for the currently selected template
};
