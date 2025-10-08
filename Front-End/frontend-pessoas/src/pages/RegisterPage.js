import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

function RegisterPage() {
  const [username, setUsername] = useState('');
  const [name, setName] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleRegister = async (e) => {
    e.preventDefault();
    setError('');
    try {
      await api.post('/User/Register', { username, name, password });
      navigate('/');
    } catch (err) {
      setError('Falha no registro. Tente outro nome de usuário.');
      console.error(err);
    }
  };

   const handleGoToLogin = () => {
    navigate('/');
  };

  return (
    <div className="container form-container">
      <h1>Registro</h1>
      <form onSubmit={handleRegister}>
        {error && <p className="error-message">{error}</p>}
        <div className="form-group">
          <label>Login</label>
          <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} required />
        </div>
         <div className="form-group">
          <label>Nome</label>
          <input type="text" value={name} onChange={(e) => setName(e.target.value)} required />
        </div>
        <div className="form-group">
          <label>Senha</label>
          <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
        </div>
        <button type="submit" className="btn btn-primary">Registrar</button>
        <button type="button" className="btn btn-primary" onClick={handleGoToLogin}>Login</button>
      </form>
    </div>
  );
}

export default RegisterPage;