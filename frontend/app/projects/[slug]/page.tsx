import { getProjectBySlug } from '@/lib/api';
import { notFound } from 'next/navigation';
import ProjectDetail from '@/components/ProjectDetail';

export default async function ProjectPage({ params }: { params: { slug: string } }) {
  const project = await getProjectBySlug(params.slug);

  if (!project) {
    notFound();
  }

  return <ProjectDetail project={project} />;
}
