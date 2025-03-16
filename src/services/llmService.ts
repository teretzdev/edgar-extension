import axios from "axios";

type LLMRequestPayload = {
  prompt: string;
};

type LLMResponse = {
  name: string;
  size: string;
  description: string;
};

/**
 * Service for handling LLM API requests.
 */
class LLMService {
  private static instance: LLMService;
  private readonly apiUrl: string;

  private constructor(apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  /**
   * Gets the singleton instance of the LLMService.
   * @returns {LLMService} The singleton instance.
   */
  public static getInstance(): LLMService {
    if (!LLMService.instance) {
      const apiUrl = process.env.LLM_API_URL || "/api/generate-room-template";
      LLMService.instance = new LLMService(apiUrl);
    }
    return LLMService.instance;
  }

  /**
   * Sends a prompt to the LLM API and retrieves the generated room template.
   * @param {string} prompt - The prompt describing the desired room template.
   * @returns {Promise<LLMResponse>} The generated room template.
   * @throws {Error} If the API request fails.
   */
  public async generateRoomTemplate(prompt: string): Promise<LLMResponse> {
    const payload: LLMRequestPayload = { prompt };

    try {
      const response = await axios.post<LLMResponse>(this.apiUrl, payload, {
        headers: {
          "Content-Type": "application/json",
        },
      });

      return response.data;
    } catch (error) {
      console.error("Failed to generate room template:", error);
      throw new Error("Failed to generate room template. Please try again.");
    }
  }
}

export default LLMService.getInstance();
