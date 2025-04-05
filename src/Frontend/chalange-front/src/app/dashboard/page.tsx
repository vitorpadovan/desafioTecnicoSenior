'use client';
import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useSession } from "next-auth/react";

export default function Dashboard() {
  const { data: session, status } = useSession();
  const router = useRouter();

  useEffect(() => {
    if (typeof window === "undefined") return; // Garante que o código só será executado no cliente

    const accessToken = localStorage.getItem("accessToken");

    if (!accessToken) {
      router.replace("/login"); // Redireciona imediatamente se não houver token
      return;
    }

    if (status === "authenticated" && session?.accessToken) {
      localStorage.setItem("accessToken", session.accessToken); // Atualiza o token no localStorage
    } else if (status === "unauthenticated") {
      localStorage.removeItem("accessToken"); // Remove o token se não autenticado
      router.replace("/login");
    }
  }, [session, status, router]);

  if (status === "loading") {
    return <p>Carregando...</p>; // Exibe um estado de carregamento
  }

  return <p>Bem-vindo ao Dashboard!</p>; // Conteúdo do dashboard
}
