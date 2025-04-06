import NextAuth from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";

declare module "next-auth" {
  interface Session {
    accessToken?: string;
  }
}
const url = `${process.env.NEXT_PUBLIC_API_URL}/api/User/login`;

interface AccessTokenResponse {
  accessToken: string;
}

export default NextAuth({
  providers: [
    CredentialsProvider({
      name: "Credentials",
      credentials: {
        email: { label: "Email", type: "text" },
        password: { label: "Password", type: "password" },
      },
      async authorize(credentials) {
        console.log('credentials', credentials);
        console.log(`URL a ser usada é ${url}`);
        if (!credentials) {
          return null;
        }
        try {
          const res = await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
              email: credentials?.email,
              password: credentials?.password,
            }),
          });

          if (!res.ok) {
            throw new Error("Credenciais inválidas");
          }

          const data = await res.json();
          return {
            id: data.userId || "default-id", // Ensure 'id' is provided
            name: credentials?.email,
            email: credentials?.email,
            accessToken: data.accessToken,
          };
        } catch (error) {
          console.error("Erro na autenticação:", error);
          return null;
        }
      },
    }),
  ],
  callbacks: {
    signIn({ user, account, profile }) {
      console.log("Usuário logado: callback", user, account, profile);
      return true;
    },
    async jwt({ token, user }) {
      if (user && "accessToken" in user) {
        token.accessToken = (user as AccessTokenResponse).accessToken;
      }
      return token;
    },
    async session({ session, token }) {
      session.accessToken = token.accessToken as string | undefined;
      return session;
    },
  },
  pages: {
    signIn: "/login",
  },
  events: {
    async signIn({ user, account, profile }) {
      console.log("Usuário logado: events", user, account, profile);
    },
    async signOut({ token }) {
      try {
        const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/User/logout`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token.accessToken}`,
          },
        });

        if (!res.ok) {
          console.error("Erro ao realizar logout no servidor:", res.statusText);
        }

        const cookies = document.cookie.split("; ");
        for (const cookie of cookies) {
          const [name] = cookie.split("=");
          document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
        }
      } catch (error) {
        console.error("Erro ao realizar logout:", error);
      }
    },
  },
});