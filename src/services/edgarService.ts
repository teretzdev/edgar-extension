import axios from "axios";

type EdgarRequestPayload = {
  templates: {
    name: string;
    size: {
      width: number;
      height: number;
    };
    description: string;
    prefab?: string;
  }[];
};

type EdgarResponse = {
  success: boolean;
  processedTemplates: {
    name: string;
    size: {
      width: number;
      height: number;
    };
    description: string;
    prefab?: string;
  }[];
};

/**
 * Service for handling Edgar API integration.
 */
class EdgarService {
  private static instance: EdgarService;
  private readonly apiUrl: string;

  private constructor(apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  /**
   * Gets the singleton instance of the EdgarService.
   * @returns {EdgarService} The singleton instance.
   */
  public static getInstance(): EdgarService {
    if (!EdgarService.instance) {
      const apiUrl = process.env.EDGAR_API_URL || "/api/edgar";
      EdgarService.instance = new EdgarService(apiUrl);
    }
    return EdgarService.instance;
  }

  /**
   * Sends room templates to the Edgar API for processing.
   * @param {EdgarRequestPayload} payload - The payload containing room templates.
   * @returns {Promise<EdgarResponse>} The response from the Edgar API.
   * @throws {Error} If the API request fails.
   */
  public async sendTemplatesToEdgar(payload: EdgarRequestPayload): Promise<EdgarResponse> {
    try {
      const response = await axios.post<EdgarResponse>(`${this.apiUrl}/templates`, payload, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${process.env.EDGAR_API_TOKEN}`,
        },
      });

      return response.data;
    } catch (error) {
      console.error("Failed to send templates to Edgar API:", error);
      throw new Error("Failed to send templates to Edgar API. Please try again.");
    }
  }

  /**
   * Retrieves processed room templates from the Edgar API.
   * @returns {Promise<EdgarResponse>} The response containing processed templates.
   * @throws {Error} If the API request fails.
   */
  public async fetchProcessedTemplates(): Promise<EdgarResponse> {
    try {
      const response = await axios.get<EdgarResponse>(`${this.apiUrl}/templates/processed`, {
        headers: {
          Authorization: `Bearer ${process.env.EDGAR_API_TOKEN}`,
        },
      });

      return response.data;
    } catch (error) {
      console.error("Failed to fetch processed templates from Edgar API:", error);
      throw new Error("Failed to fetch processed templates from Edgar API. Please try again.");
    }
  }
}

export default EdgarService.getInstance();
