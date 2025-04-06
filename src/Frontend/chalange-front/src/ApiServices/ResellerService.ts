import { fetchClient } from "../ibs/fetchClient";

export class ResellerService {
    static async getResellers() {
        const url = "api/Reseller"; // Caminho relativo para o fetchClient
        const response = await fetchClient(url, {
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