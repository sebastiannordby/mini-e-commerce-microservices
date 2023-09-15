import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { AuthProvider } from 'oidc-react';

const oidcConfig = {
  onSignIn: () => {
    // Redirect?
  },
  authority: 'https://localhost:60842',
  clientId: 'miniecommerce.desktop.react',
  redirectUri: 'http://127.0.0.1:5173/',
  response_type: 'token id_token',
  scope: 'fullaccess',
  loadUserInfo: true,
  monitorSession: false,
};

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <AuthProvider {...oidcConfig}>
      <App />
    </AuthProvider>
  </React.StrictMode>,
)
