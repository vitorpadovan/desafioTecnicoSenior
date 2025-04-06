"use client";
import DashboardHome from "@/components/DashboarHome";
import { SessionProvider } from "next-auth/react";

export default function DashboardLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <SessionProvider>
      <DashboardHome>{children}</DashboardHome>
    </SessionProvider>
  );
}
