'use client';

import { useEffect, useState } from 'react';
import { getDashboardMetrics } from '@/lib/api';
import ProtectedRoute from '@/components/ProtectedRoute';
import AdminLayout from '@/components/AdminLayout';

function DashboardPage() {
  const [metrics, setMetrics] = useState<any>(null);

  useEffect(() => {
    loadMetrics();
  }, []);

  const loadMetrics = async () => {
    try {
      const data = await getDashboardMetrics();
      setMetrics(data);
    } catch (error) {
      console.error('Failed to load metrics', error);
    }
  };

  return (
    <AdminLayout>
      <div className="p-8">
        <h1 className="text-3xl font-bold mb-8">Dashboard</h1>
        
        <div className="grid md:grid-cols-3 gap-6">
          <div className="bg-white p-6 rounded-lg shadow">
            <h3 className="text-gray-500 text-sm">Traffic per Month</h3>
            <p className="text-3xl font-bold mt-2">{metrics?.trafficPerMonth || 0}</p>
          </div>
          
          <div className="bg-white p-6 rounded-lg shadow">
            <h3 className="text-gray-500 text-sm">Bounce Rate</h3>
            <p className="text-3xl font-bold mt-2">{metrics?.bounceRate || 0}%</p>
          </div>
          
          <div className="bg-white p-6 rounded-lg shadow">
            <h3 className="text-gray-500 text-sm">Conversion Rate</h3>
            <p className="text-3xl font-bold mt-2">{metrics?.conversionRate || 0}%</p>
          </div>
        </div>
      </div>
    </AdminLayout>
  );
}

export default function Dashboard() {
  return (
    <ProtectedRoute>
      <DashboardPage />
    </ProtectedRoute>
  );
}
