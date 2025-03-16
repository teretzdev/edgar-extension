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
      // Simulate Unity scripting system call
      console.log("Sending templates to Edgar via Unity scripting system:", templates);
      // Replace with actual Unity scripting integration logic
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
      // Simulate Unity scripting system call
      console.log("Fetching processed templates from Edgar via Unity scripting system");
      // Replace with actual Unity scripting integration logic
      return []; // Replace with actual processed templates
    } catch (error) {
      console.error("Failed to fetch processed templates from Edgar:", error);
      throw new Error("Failed to fetch processed templates from Edgar. Please try again.");
    }
  }
}

export default EdgarService.getInstance();