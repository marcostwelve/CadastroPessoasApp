import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';

function EditPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  

  const [pessoa, setPessoa] = useState({
    nome: '',
    cpf: '',
    dataNascimento: '',
    email: '',
    sexo: ''
  });
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(true);

  useEffect(() => {

    const fetchPessoa = async () => {
      try {
        const response = await api.get(`api/cadastro/pessoas/${id}`);
        const formattedData = {
          ...response.data,
          dataNascimento: new Date(response.data.dataNascimento).toISOString().split('T')[0]
        };
        setPessoa(formattedData);
        setLoading(false);
      } catch (err) {
        console.error("Erro ao buscar pessoa:", err);
        setError("Não foi possível carregar os dados para edição.");
        setLoading(false);
      }
    };
    fetchPessoa();
  }, [id]);


  const handleChange = (e) => {
    const { name, value } = e.target;
    setPessoa(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

 
  const handleUpdate = async (e) => {
    e.preventDefault();
    setError('');
    try {
      await api.put(`api/cadastro/pessoas/${id}`, pessoa);
      navigate('/home');
    } catch (err) {
      setError('Falha ao atualizar. Verifique os dados e tente novamente.');
      console.error(err);
    }
  };

  if (loading) {
    return <div className="container">Carregando...</div>;
  }

  return (
    <div className="container form-container">
      <h1>Editar Pessoa</h1>
      <form onSubmit={handleUpdate}>
        {error && <p className="error-message">{error}</p>}
        <div className="form-group">
          <label>Nome</label>
          <input type="text" name="nome" value={pessoa.nome || ''} onChange={handleChange} required />
        </div>
        <div className="form-group">
          <label>Email</label>
          <input type="email" name="email" value={pessoa.email || ''} onChange={handleChange} />
        </div>
         <div className="form-group">
          <label>Sexo</label>
          <select name="sexo" value={pessoa.sexo || ''} onChange={handleChange}>
            <option value="">Selecione...</option>
            <option value="M">Masculino</option>
            <option value="F">Feminino</option>
          </select>
        </div>
        <button type="submit" className="btn btn-primary">Salvar Alterações</button>
        <button type="button" onClick={() => navigate('/home')} className="btn btn-secondary">Cancelar</button>
      </form>
    </div>
  );
}

export default EditPage;