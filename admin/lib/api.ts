import axios from 'axios';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_URL,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export async function login(email: string, password: string) {
  const { data } = await api.post('/auth/login', { email, password });
  return data;
}

export async function getDashboardMetrics() {
  const { data } = await api.get('/analytics/dashboard');
  return data;
}

export async function getAllProjects() {
  const { data } = await api.get('/projects/admin/all');
  return data;
}

export async function createProject(project: any) {
  const { data } = await api.post('/projects', project);
  return data;
}

export async function updateProject(id: string, project: any) {
  const { data } = await api.put(`/projects/${id}`, project);
  return data;
}

export async function deleteProject(id: string) {
  await api.delete(`/projects/${id}`);
}

export async function publishProject(id: string) {
  await api.post(`/projects/${id}/publish`);
}

export async function uploadMedia(file: File, assetType: string) {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('assetType', assetType);
  const { data } = await api.post('/media/upload', formData);
  return data;
}

export async function updateHeroSection(hero: any) {
  await api.put('/content/hero', hero);
}

export async function publishSection(section: string) {
  await api.post(`/content/publish/${section}`);
}
