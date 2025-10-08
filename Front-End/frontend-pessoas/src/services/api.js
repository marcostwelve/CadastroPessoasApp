import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7004/', // IMPORTANTE: Verifique a porta da sua API!
});

// Interceptor para adicionar o token JWT em todas as requisições
api.interceptors.request.use(async config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;