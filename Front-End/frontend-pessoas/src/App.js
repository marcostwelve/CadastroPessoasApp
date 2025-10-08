import React from 'react';
import { Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import HomePage from './pages/HomePage';
import EditPage from './pages/EditPage'
import './App.css';
import CreatePage from './pages/CreatePage';

function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/home" element={<HomePage />} />
      <Route path="/edit/:id" element={<EditPage />} />
      <Route path="/create" element={<CreatePage />} />
    </Routes>
  );
}

export default App;