import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

function HomePage() {
  const [pessoas, setPessoas] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPessoas = async () => {
      try {
        const response = await api.get('api/cadastro/pessoas');
        setPessoas(response.data);
      } catch (error) {
        console.error("Erro ao buscar pessoas:", error);
        if (error.response && error.response.status === 401) {
            navigate('/');
        }
      }
    };

    fetchPessoas();
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate('/');
  };

  const handleEdit = (id) => {
    console.log("Editar pessoa com ID:", id);
    navigate(`/Edit/${id}`);
  };

  const handleDelete = async (id) => {
    if (window.confirm("Tem certeza que deseja excluir esta pessoa?")) {
        try {
            // Ajuste a rota se o seu endpoint de deletar for diferente
            await api.delete(`api/cadastro/pessoas/${id}`);
            // Remove a pessoa da lista local para atualizar a UI
            setPessoas(pessoas.filter(p => p.id !== id));
        } catch (error) {
            console.error("Erro ao deletar pessoa:", error);
            alert("Não foi possível excluir a pessoa.");
        }
    }
  };

   const handleGoToCreate = () => {
    navigate('/create');
  };

  return (
    <div className="container">
      <div style={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}}>
        <h1>Pessoas Cadastradas</h1>
        <button onClick={handleGoToCreate} className="btn btn-primary">Cadastrar Nova Pessoa</button>
        <button onClick={handleLogout} className="btn btn-secondary">Sair</button>
      </div>
      <table className="people-table">
        <thead>
          <tr>
            <th>Nome</th>
            <th>CPF</th>
            <th>Data de Nascimento</th>
            <th>Email</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.map(pessoa => (
            <tr key={pessoa.id}>
              <td>{pessoa.nome}</td>
              <td>{pessoa.cpf}</td>
              <td>{new Date(pessoa.dataNascimento).toLocaleDateString()}</td>
              <td>{pessoa.email}</td>
              <td>
                <button onClick={() => handleEdit(pessoa.id)} className="btn btn-primary">Editar</button>
                <button onClick={() => handleDelete(pessoa.id)} className="btn btn-danger">Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default HomePage;