import { Reseller, ResellerInformationData } from "@/interfaces/Reseller";
import { fetchClient } from "../libs/fetchClient";

export class ResellerService {
  private static readonly baseUrl: string = "api/Reseller";
  static async getResellerInformation(
    resellerId: string
  ): Promise<ResellerInformationData> {
    const response = await fetchClient(`${this.baseUrl}/${resellerId}`, {
      cache: "no-store",
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (!response.ok) {
      throw new Error("Failed to fetch reseller information");
    }
    return response.json();
  }
  static async getResellers(): Promise<Reseller[]> {
    const response = await fetchClient(this.baseUrl, {
      cache: "no-store",
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (!response.ok) {
      throw new Error("Failed to fetch resellers");
    }
    return response.json();
  }
}
