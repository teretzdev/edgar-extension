import { RoomTemplate } from "@/types/roomTemplate";

/**
 * Service for handling Edgar integration through Unity's scripting system.
 */
class EdgarService {
  private static instance: EdgarService;

  private constructor() {}

  /**
   * Gets the singleton instance of the EdgarService.
   * @returns {EdgarService} The singleton instance.
   */
  public static getInstance(): EdgarService {
    if (!EdgarService.instance) {
      EdgarService.instance = new EdgarService();
    }
    return EdgarService.instance;
  }

  /**
   * Sends room templates to Edgar for processing via Unity's scripting system.
   * @param {RoomTemplate[]} templates - The list of room templates to process.
   * @returns {Promise<void>} Resolves when the templates are successfully sent.
   */
  public async sendTemplatesToEdgar(templates: RoomTemplate[]): Promise<void> {
    try {
      // Directly call Unity's scripting system to send templates
      console.log("Sending templates to Edgar using Unity's scripting system:", templates);
      // Implement Unity scripting integration logic here
    } catch (error) {
      console.error("Failed to send templates to Edgar:", error);
      throw new Error("Failed to send templates to Edgar. Please try again.");
    }
  }

  /**
   * Retrieves processed room templates from Edgar via Unity's scripting system.
   * @returns {Promise<RoomTemplate[]>} The list of processed room templates.
   * @throws {Error} If the retrieval fails.
   */
  public async fetchProcessedTemplates(): Promise<RoomTemplate[]> {
    try {
      // Directly call Unity's scripting system to fetch processed templates
      console.log("Fetching processed templates from Edgar using Unity's scripting system");
      // Implement Unity scripting integration logic here
      return []; // Replace with actual processed templates from Unity
    } catch (error) {
      console.error("Failed to fetch processed templates from Edgar:", error);
      throw new Error("Failed to fetch processed templates from Edgar. Please try again.");
    }
  }
}

export default EdgarService.getInstance();