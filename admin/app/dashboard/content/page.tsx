'use client';

import { useState } from 'react';
import Link from 'next/link';
import ProtectedRoute from '@/components/ProtectedRoute';
import AdminLayout from '@/components/AdminLayout';

function ContentPage() {
  const sections = [
    {
      id: 'navbar',
      title: 'Navbar',
      description: 'Edit logo, CTA button text and link',
      icon: '🔝',
      href: '/dashboard/content/navbar'
    },
    {
      id: 'hero',
      title: 'Hero Section',
      description: 'Edit headline, tagline, and background image/video',
      icon: '🎯',
      href: '/dashboard/content/hero'
    },
    {
      id: 'youtube',
      title: 'YouTube Section',
      description: 'Add YouTube video URL and title',
      icon: '📺',
      href: '/dashboard/content/youtube'
    },
    {
      id: 'about',
      title: 'About Section',
      description: 'Edit about text and image',
      icon: '📝',
      href: '/dashboard/content/about'
    },
    {
      id: 'why-choose-us',
      title: 'Why Choose Us',
      description: 'Edit section title and items',
      icon: '⭐',
      href: '/dashboard/content/why-choose-us'
    },
    {
      id: 'what-we-do',
      title: 'What We Do',
      description: 'Edit services and offerings',
      icon: '🛠️',
      href: '/dashboard/content/what-we-do'
    },
    {
      id: 'footer',
      title: 'Footer',
      description: 'Edit contact info and social links',
      icon: '📧',
      href: '/dashboard/content/footer'
    }
  ];

  return (
    <AdminLayout>
      <div className="p-8">
        <div className="mb-8">
          <h1 className="text-3xl font-bold mb-2">Content Management</h1>
          <p className="text-gray-600">Edit all sections of your website</p>
        </div>

        <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
          {sections.map((section) => (
            <Link key={section.id} href={section.href}>
              <div className="bg-white p-6 rounded-lg shadow hover:shadow-lg transition cursor-pointer border-2 border-transparent hover:border-blue-500">
                <div className="text-4xl mb-4">{section.icon}</div>
                <h3 className="text-xl font-bold mb-2">{section.title}</h3>
                <p className="text-gray-600 text-sm">{section.description}</p>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </AdminLayout>
  );
}

export default function Content() {
  return (
    <ProtectedRoute>
      <ContentPage />
    </ProtectedRoute>
  );
}
