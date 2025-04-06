"use client";
import { useSession, signOut } from "next-auth/react";
import { useRouter } from "next/navigation"; // Importação do useRouter
import Link from "next/link";
import { ReactNode, useEffect, useState } from "react";
import LoadingPage from "./LoadingPage";

interface DashboardHomeProps {
  children: ReactNode;
}

export default function DashboardHome({ children }: DashboardHomeProps) {
  const { data: session, status } = useSession();
  const router = useRouter();
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    if (status === "authenticated") {
      setIsAuthenticated(true);
    } else if (status === "unauthenticated") {
      setIsAuthenticated(false);
      router.push("/login");
    }
  }, [session, status, router]);

  return (
    <>
      {isAuthenticated ? (
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
                  <button
                    onClick={() => signOut()}
                    className="hover:underline text-red-500"
                  >
                    Logout
                  </button>
                </li>
              </ul>
            </nav>
          </aside>
          <main className="flex-1 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 p-4">
            {children}
          </main>
        </div>
      ) : <LoadingPage />}  
    </>
  );
}
