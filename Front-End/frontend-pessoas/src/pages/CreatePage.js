import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

function CreatePage() {
  const navigate = useNavigate();
  
  const [pessoa, setPessoa] = useState({
    nome: '',
    cpf: '',
    dataNascimento: '',
    email: '',
    sexo: ''
  });
  const [error, setError] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPessoa(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    try {
      await api.post('api/cadastro/pessoas', pessoa);
      navigate('/home');
    } catch (err) {
      if (err.response && err.response.data && err.response.data.errors) {
        const validationErrors = err.response.data.errors;
        const errorMessages = Object.values(validationErrors).flat().join(' ');
        setError(errorMessages);
      } else {
        setError('Falha ao cadastrar. Verifique os dados.');
      }
      console.error(err);
    }
  };

  return (
    <div className="container form-container">
      <h1>Cadastrar Nova Pessoa</h1>
      <form onSubmit={handleSubmit}>
        {error && <p className="error-message">{error}</p>}
        <div className="form-group">
          <label>Nome</label>
          <input type="text" name="nome" value={pessoa.nome} onChange={handleChange} required />
        </div>
        <div className="form-group">
          <label>CPF</label>
          <input type="text" name="cpf" value={pessoa.cpf} onChange={handleChange} required />
        </div>
         <div className="form-group">
          <label>Email</label>
          <input type="email" name="email" value={pessoa.email} onChange={handleChange} />
        </div>
        <div className="form-group">
          <label>Sexo</label>
          <select name="sexo" value={pessoa.sexo} onChange={handleChange}>
            <option value="">Selecione...</option>
            <option value="M">Masculino</option>
            <option value="F">Feminino</option>
          </select>
        </div>
        <div className="form-group">
          <label>Data de Nascimento</label>
          <input type="date" name="dataNascimento" value={pessoa.dataNascimento} onChange={handleChange} required />
        </div>
        <button type="submit" className="btn btn-primary">Cadastrar</button>
        <button type="button" onClick={() => navigate('/home')} className="btn btn-secondary">Cancelar</button>
      </form>
    </div>
  );
}

export default CreatePage;