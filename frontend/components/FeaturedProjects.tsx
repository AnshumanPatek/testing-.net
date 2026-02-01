import { getFeaturedProjects } from '@/lib/api';
import Link from 'next/link';
import Image from 'next/image';

export default async function FeaturedProjects() {
  const projects = await getFeaturedProjects();

  return (
    <section className="py-20 bg-gray-50">
      <div className="container mx-auto px-6">
        <h2 className="text-4xl font-bold text-center mb-12">Featured Projects</h2>
        
        <div className="grid md:grid-cols-3 gap-8">
          {projects.map((project: any) => (
            <Link key={project.id} href={`/projects/${project.slug}`}>
              <div className="bg-white rounded-lg overflow-hidden shadow-lg hover:shadow-xl transition">
                {project.thumbnailUrl && (
                  <div className="relative h-64">
                    <Image
                      src={project.thumbnailUrl}
                      alt={project.title}
                      fill
                      className="object-cover"
                    />
                  </div>
                )}
                <div className="p-6">
                  <h3 className="text-xl font-bold mb-2">{project.title}</h3>
                  {project.brandName && (
                    <p className="text-gray-600">{project.brandName}</p>
                  )}
                </div>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </section>
  );
}
