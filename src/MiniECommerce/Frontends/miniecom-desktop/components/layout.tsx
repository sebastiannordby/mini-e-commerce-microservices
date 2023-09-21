import Header from "./header"
import Footer from "./footer"
import type { ReactNode } from "react"
import { NextAuthProvider } from "lib/providers"

export default function Layout({ children }: { children: ReactNode }) {
  return (
    <NextAuthProvider>
      <Header />
      <main>{children}</main>
      <Footer />
    </NextAuthProvider>
  )
}
