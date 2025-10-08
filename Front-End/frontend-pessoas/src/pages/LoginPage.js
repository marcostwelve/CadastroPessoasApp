import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

function LoginPage() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setError('');
    try {
      
      const response = await api.post('/User/login', { username, password });
      localStorage.setItem('token', response.data.token);
      navigate('/home');
    } catch (err) {
      setError('Falha no login. Verifique suas credenciais.');
      console.error(err);
    }
  };

    const handleGoToRegister = () => {
    navigate('/register');
  };

  return (
    <div className="container form-container">
      <h1>Login</h1>
      <form onSubmit={handleLogin}>
        {error && <p className="error-message">{error}</p>}
        <div className="form-group">
          <label>Usuário</label>
          <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} required />
        </div>
        <div className="form-group">
          <label>Senha</label>
          <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
        </div>
        <button type="submit" className="btn btn-primary">Entrar</button>
        <button type="button" onClick={handleGoToRegister} className="btn btn-secondary">Registrar</button>
      </form>
    </div>
  );
}

export default LoginPage;