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

// Auth
export async function login(email: string, password: string) {
  const { data } = await api.post('/auth/login', { email, password });
  return data;
}

// Analytics
export async function getDashboardMetrics() {
  const { data } = await api.get('/analytics/dashboard');
  return data;
}

// Projects
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

// Media
export async function uploadMedia(file: File, assetType: string) {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('assetType', assetType);
  const { data } = await api.post('/media/upload', formData);
  return data;
}

export async function getAllMedia(type?: string) {
  const { data } = await api.get('/media', { params: { type } });
  return data;
}

// Content - Navbar
export async function getNavbar() {
  const { data } = await axios.get(`${API_URL}/content/navbar`);
  return data;
}

export async function updateNavbar(navbar: any) {
  await api.put('/content/navbar', navbar);
}

// Content - Hero
export async function getHeroSection() {
  const { data } = await api.get('/content/hero');
  return data;
}

export async function updateHeroSection(hero: any) {
  await api.put('/content/hero', hero);
}

// Content - About
export async function getAboutSection() {
  const { data } = await api.get('/content/about');
  return data;
}

export async function updateAboutSection(about: any) {
  await api.put('/content/about', about);
}

// Content - YouTube
export async function getYouTubeSection() {
  const { data } = await api.get('/content/youtube');
  return data;
}

export async function updateYouTubeSection(youtube: any) {
  await api.put('/content/youtube', youtube);
}

// Content - Footer
export async function getFooter() {
  const { data } = await axios.get(`${API_URL}/content/footer`);
  return data;
}

export async function updateFooter(footer: any) {
  await api.put('/content/footer', footer);
}

// Publish
export async function publishSection(section: string) {
  await api.post(`/content/publish/${section}`);
}
