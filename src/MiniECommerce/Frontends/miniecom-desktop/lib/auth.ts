import NextAuth from "next-auth/next"
import { AuthOptions } from "next-auth/index"
import IdentityServerProvider from 'next-auth/providers/duende-identity-server6'

export const authOptions: AuthOptions = {
    debug: true,
    providers: [
      IdentityServerProvider({
          id: "sample-identity-server",
          name: "IdentityServer",
          authorization: { params: { scope: "openid profile" } },
          issuer:  process.env.IDP_ISSUER ?? "",
          clientId: "nextjs_web_app",
          clientSecret: process.env.IDP_CLIENT_SECRET ?? "",
      })
    ],
    theme: {
      colorScheme: "dark",
    },
    callbacks: {
      async jwt({ token, user}) {
        console.log('User: ', user);
        token.userRole = "admin"
        return token;
      },
    }
  }
  
  export default NextAuth(authOptions)
  