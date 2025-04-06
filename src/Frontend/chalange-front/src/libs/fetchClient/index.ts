"use client";
import { signOut, getSession } from "next-auth/react";

export const fetchClient = async (
  input: string | URL | Request,
  init?: RequestInit | undefined
): Promise<Response> => {
  const session = await getSession();
  const jwt = session?.accessToken;
  const url = process.env.NEXT_PUBLIC_API_URL;
  const finalUrl = `${url}/${input}`;
  console.log("URL:", finalUrl);
  console.log("JWT:", jwt);
  console.log("Request Init:", init);
  const response = await fetch(finalUrl, {
    ...init,
    headers: {
      ...init?.headers,
      ...(jwt && { Authorization: `Bearer ${jwt}` }),
    },
  });

  if (response.status === 401) {
    await signOut();
  }

  return response;
};
