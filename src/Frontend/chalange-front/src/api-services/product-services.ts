import { fetchClient } from "../libs/fetchClient";
import { Product } from "@/interfaces/Product";

export class ProductService {
  private static readonly baseUrl: string = "api/Product";
  static async getProductsFromReseller(resellerId: string): Promise<Product[]> {
    const response = await fetchClient(`${this.baseUrl}/${resellerId}`, {
      cache: "no-store",
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (!response.ok) {
      throw new Error("Failed to fetch reseller products");
    }
    return response.json();
  }
}
