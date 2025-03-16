import { useState, useCallback } from "react";
import { RoomTemplate } from "@/types/roomTemplate";

/**
 * Custom hook for managing room templates state across components.
 */
const useRoomTemplates = () => {
  const [roomTemplates, setRoomTemplates] = useState<RoomTemplate[]>([]);
  const [selectedTemplate, setSelectedTemplate] = useState<RoomTemplate | null>(null);

  /**
   * Adds a new room template to the state.
   * @param template - The room template to add.
   */
  const addRoomTemplate = useCallback((template: RoomTemplate) => {
    setRoomTemplates((prevTemplates) => [...prevTemplates, template]);
  }, []);

  /**
   * Updates an existing room template in the state.
   * @param updatedTemplate - The updated room template.
   */
  const updateRoomTemplate = useCallback((updatedTemplate: RoomTemplate) => {
    setRoomTemplates((prevTemplates) =>
      prevTemplates.map((template) =>
        template.name === updatedTemplate.name ? updatedTemplate : template
      )
    );
  }, []);

  /**
   * Deletes a room template from the state.
   * @param templateName - The name of the room template to delete.
   */
  const deleteRoomTemplate = useCallback((templateName: string) => {
    setRoomTemplates((prevTemplates) =>
      prevTemplates.filter((template) => template.name !== templateName)
    );
  }, []);

  /**
   * Selects a room template for further operations.
   * @param template - The room template to select.
   */
  const selectRoomTemplate = useCallback((template: RoomTemplate) => {
    setSelectedTemplate(template);
  }, []);

  /**
   * Clears the currently selected room template.
   */
  const clearSelectedTemplate = useCallback(() => {
    setSelectedTemplate(null);
  }, []);

  return {
    roomTemplates,
    selectedTemplate,
    addRoomTemplate,
    updateRoomTemplate,
    deleteRoomTemplate,
    selectRoomTemplate,
    clearSelectedTemplate,
  };
};

export default useRoomTemplates;
