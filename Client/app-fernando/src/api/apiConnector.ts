import axios, { AxiosResponse } from "axios";
import { AdvisorDto } from "../models/advisorDto";
import { API_BASE_URL } from "../config";
import { GetAdvisorByIdResponse } from "../models/getAdvisorByIdResponse";

const apiConnector = {
  getAdvisors: async (): Promise<AdvisorDto[]> => {
    try {
      const response: AxiosResponse<AdvisorDto[]> = await axios.get(
        `${API_BASE_URL}/advisors`
      );

      const advisors = response.data.map((advisor) => ({
        ...advisor,
      }));

      return advisors;
    } catch (error) {
      console.log("Error fetching advisors:", error);
      throw error;
    }
  },

  createAdvisor: async (advisor: AdvisorDto): Promise<void> => {
    try {
      await axios.post<number>(`${API_BASE_URL}/advisors`, advisor);
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  editAdvisor: async (advisor: AdvisorDto): Promise<void> => {
    try {
      await axios.put<number>(
        `${API_BASE_URL}/advisors/${advisor.id}`,
        advisor
      );
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  deleteAvisor: async (advisorId: number): Promise<void> => {
    try {
      await axios.delete<number>(`${API_BASE_URL}/advisors/${advisorId}`);
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  getAdvisorById: async (
    advisorId: number
  ): Promise<AdvisorDto | undefined> => {
    try {
      const response = await axios.get<AdvisorDto>(
        `${API_BASE_URL}/advisors/${advisorId}`
      );
      return response.data;
    } catch (error) {
      console.log(error);
      throw error;
    }
  },
};

export default apiConnector;
