'use client';

import { useEffect, useState } from 'react';
import { getAllProjects, deleteProject } from '@/lib/api';
import Link from 'next/link';
import ProtectedRoute from '@/components/ProtectedRoute';

function ProjectsPage() {
  const [projects, setProjects] = useState<any[]>([]);

  useEffect(() => {
    loadProjects();
  }, []);

  const loadProjects = async () => {
    const data = await getAllProjects();
    setProjects(data);
  };

  const handleDelete = async (id: string) => {
    if (confirm('Delete this project?')) {
      await deleteProject(id);
      loadProjects();
    }
  };

  return (
    <div className="p-8">
      <div className="flex justify-between items-center mb-8">
        <h1 className="text-3xl font-bold">Projects</h1>
        <Link
          href="/dashboard/projects/new"
          className="px-4 py-2 bg-black text-white rounded-lg"
        >
          New Project
        </Link>
      </div>
      
      <div className="bg-white rounded-lg shadow overflow-hidden">
        <table className="w-full">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left">Title</th>
              <th className="px-6 py-3 text-left">Brand</th>
              <th className="px-6 py-3 text-left">Status</th>
              <th className="px-6 py-3 text-left">Actions</th>
            </tr>
          </thead>
          <tbody>
            {projects.map((project) => (
              <tr key={project.id} className="border-t">
                <td className="px-6 py-4">{project.title}</td>
                <td className="px-6 py-4">{project.brandName}</td>
                <td className="px-6 py-4">
                  <span className={`px-2 py-1 rounded text-sm ${
                    project.isPublished ? 'bg-green-100 text-green-800' : 'bg-gray-100 text-gray-800'
                  }`}>
                    {project.isPublished ? 'Published' : 'Draft'}
                  </span>
                </td>
                <td className="px-6 py-4">
                  <Link
                    href={`/dashboard/projects/${project.id}`}
                    className="text-blue-600 hover:underline mr-4"
                  >
                    Edit
                  </Link>
                  <button
                    onClick={() => handleDelete(project.id)}
                    className="text-red-600 hover:underline"
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default function Projects() {
  return (
    <ProtectedRoute>
      <ProjectsPage />
    </ProtectedRoute>
  );
}
