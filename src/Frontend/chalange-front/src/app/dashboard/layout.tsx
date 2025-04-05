"use client";
import { SessionProvider } from "next-auth/react";
import Link from "next/link";

export default function DashboardLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <SessionProvider>
      <div className={`h-screen flex`}>
        <aside className="w-64 bg-gray-200 dark:bg-gray-800 text-gray-900 dark:text-gray-100 p-4">
          <nav>
            <ul className="space-y-4">
              <li>
                <Link href="/dashboard" className="hover:underline">
                  Home
                </Link>
              </li>
              <li>
                <Link href="/dashboard/profile" className="hover:underline">
                  Perfil
                </Link>
              </li>
              <li>
                <Link href="/dashboard/settings" className="hover:underline">
                  Configurações
                </Link>
              </li>
            </ul>
          </nav>
        </aside>
        <main className="flex-1 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 p-4">
          {children}
        </main>
      </div>
    </SessionProvider>
  );
}
