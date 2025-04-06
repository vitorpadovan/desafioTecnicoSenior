import { fetchClient } from "../libs/fetchClient";

export class OrderService {
  private static readonly baseUrl: string = "api/Order";

  static async createOrder(resellerId: string, orderDetails: { productId: number; quantity: number }[]): Promise<void> {
    const response = await fetchClient(`${this.baseUrl}/${resellerId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ orderDetails }),
    });
    if (!response.ok) {
      throw new Error("Failed to create order");
    }
  }
}
