const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api';

export async function getHomeContent() {
  const res = await fetch(`${API_URL}/content/home`, { cache: 'no-store' });
  if (!res.ok) throw new Error('Failed to fetch home content');
  return res.json();
}

export async function getNavbar() {
  const res = await fetch(`${API_URL}/content/navbar`, { cache: 'no-store' });
  if (!res.ok) throw new Error('Failed to fetch navbar');
  return res.json();
}

export async function getFooter() {
  const res = await fetch(`${API_URL}/content/footer`, { cache: 'no-store' });
  if (!res.ok) throw new Error('Failed to fetch footer');
  return res.json();
}

export async function getFeaturedProjects() {
  const res = await fetch(`${API_URL}/projects/featured`, { cache: 'no-store' });
  if (!res.ok) throw new Error('Failed to fetch featured projects');
  return res.json();
}

export async function getProjectBySlug(slug: string) {
  const res = await fetch(`${API_URL}/projects/${slug}`, { cache: 'no-store' });
  if (!res.ok) return null;
  return res.json();
}
