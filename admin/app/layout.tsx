import type { Metadata } from 'next';
import './globals.css';
import AdminLayout from '@/components/AdminLayout';

export const metadata: Metadata = {
  title: 'Gainateliê CMS',
  description: 'Content Management System',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}
