import { NextApiRequest, NextApiResponse } from "next";

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
  if (req.method !== "POST") {
    return res.status(405).json({ message: "Método não permitido" });
  }

  const { token } = req.body;

  try {
    const url = `${process.env.NEXT_PUBLIC_API_URL}/api/User/refresh-token`;
    const response = await fetch(url, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ token }),
    });

    if (!response.ok) {
      throw new Error("Erro ao renovar o token");
    }

    const data = await response.json();
    return res.status(200).json({ accessToken: data.accessToken });
  } catch (error) {
    console.error("Erro ao renovar o token:", error);
    return res.status(500).json({ message: "Erro ao renovar o token" });
  }
}
