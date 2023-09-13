import logo from './logo.svg';
import './App.css';
import { AuthProvider } from 'oidc-react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

const oidcConfig = {
  onSignIn: () => {
    // Redirect?
  },
  authority: 'https://localhost:5010/',
  clientId: 'miniecommerce.desktop.react',
  redirectUri: 'https://localhost:3000/',
};

function Home() {
  return (
    <h1>Hello</h1>
  );
}

function App() {
  return (
    <AuthProvider {...oidcConfig}>
      <BrowserRouter>
        <Routes>
          <Route exact path="/" element={<Home/>} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
