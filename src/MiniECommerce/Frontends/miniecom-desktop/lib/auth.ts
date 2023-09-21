import NextAuth from "next-auth/next"
import { AuthOptions } from "next-auth/index"
import IdentityServerProvider from 'next-auth/providers/duende-identity-server6'

export const authOptions: AuthOptions = {
    debug: true,
    providers: [
      IdentityServerProvider({
          id: "demo-identity-server",
          name: "IdentityServer",
          authorization: { params: { scope: "openid profile SampleAPI" } },
          issuer:  "https://localhost:44335/",
          clientId: "nextjs_web_app",
          clientSecret: "0bced1832f9facd485eb66a70fb33094",
      })
    ],
    theme: {
      colorScheme: "dark",
    },
    callbacks: {
      async jwt({ token }) {
        token.userRole = "admin"
        return token;
      },
    }
  }
  
  export default NextAuth(authOptions)
  