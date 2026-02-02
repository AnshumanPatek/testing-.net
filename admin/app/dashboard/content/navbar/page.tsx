'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import ProtectedRoute from '@/components/ProtectedRoute';
import AdminLayout from '@/components/AdminLayout';
import MediaSelector from '@/components/MediaSelector';
import { getNavbar, updateNavbar, uploadMedia } from '@/lib/api';

function NavbarEditor() {
  const router = useRouter();
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [showMediaSelector, setShowMediaSelector] = useState(false);
  
  const [formData, setFormData] = useState({
    logoUrl: '',
    logoId: '',
    ctaText: 'Schedule a call',
    ctaUrl: ''
  });

  useEffect(() => {
    loadNavbar();
  }, []);

  const loadNavbar = async () => {
    try {
      const data = await getNavbar();
      setFormData({
        logoUrl: data.logoUrl || '',
        logoId: data.logoId || '',
        ctaText: data.ctaText || 'Schedule a call',
        ctaUrl: data.ctaUrl || ''
      });
    } catch (error) {
      console.error('Failed to load navbar', error);
    } finally {
      setLoading(false);
    }
  };

  const handleLogoUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    try {
      setSaving(true);
      const media = await uploadMedia(file, 'Image');
      setFormData(prev => ({
        ...prev,
        logoUrl: media.s3Url,
        logoId: media.id
      }));
    } catch (error) {
      alert('Failed to upload logo');
    } finally {
      setSaving(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);

    try {
      await updateNavbar(formData);
      alert('Navbar updated successfully!');
      router.push('/dashboard/content');
    } catch (error) {
      alert('Failed to update navbar');
    } finally {
      setSaving(false);
    }
  };

  if (loading) {
    return <div className="p-8">Loading...</div>;
  }

  return (
    <AdminLayout>
      <div className="p-8 max-w-4xl">
      <div className="mb-8">
        <h1 className="text-3xl font-bold mb-2">Edit Navbar</h1>
        <p className="text-gray-600">Update your website header</p>
      </div>

      <form onSubmit={handleSubmit} className="bg-white rounded-lg shadow p-6 space-y-6">
        {/* Logo Section */}
        <div>
          <label className="block text-sm font-medium mb-2">Logo</label>
          
          {formData.logoUrl && (
            <div className="mb-4">
              <img 
                src={formData.logoUrl} 
                alt="Logo" 
                className="h-16 object-contain border rounded p-2"
              />
            </div>
          )}

          <div className="flex gap-4">
            <label className="px-4 py-2 bg-blue-600 text-white rounded cursor-pointer hover:bg-blue-700">
              Upload New Logo
              <input
                type="file"
                accept="image/*"
                onChange={handleLogoUpload}
                className="hidden"
              />
            </label>
            
            {formData.logoUrl && (
              <button
                type="button"
                onClick={() => setFormData(prev => ({ ...prev, logoUrl: '', logoId: '' }))}
                className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
              >
                Remove Logo
              </button>
            )}
          </div>
          <p className="text-sm text-gray-500 mt-2">
            Recommended: PNG with transparent background, max 200px height
          </p>
        </div>

        {/* CTA Text */}
        <div>
          <label className="block text-sm font-medium mb-2">
            CTA Button Text
          </label>
          <input
            type="text"
            value={formData.ctaText}
            onChange={(e) => setFormData(prev => ({ ...prev, ctaText: e.target.value }))}
            className="w-full px-4 py-2 border rounded-lg"
            placeholder="Schedule a call"
            maxLength={100}
          />
          <p className="text-sm text-gray-500 mt-1">
            {formData.ctaText.length}/100 characters
          </p>
        </div>

        {/* CTA URL */}
        <div>
          <label className="block text-sm font-medium mb-2">
            CTA Button Link
          </label>
          <input
            type="url"
            value={formData.ctaUrl}
            onChange={(e) => setFormData(prev => ({ ...prev, ctaUrl: e.target.value }))}
            className="w-full px-4 py-2 border rounded-lg"
            placeholder="https://calendly.com/your-link"
          />
          <p className="text-sm text-gray-500 mt-1">
            Full URL including https://
          </p>
        </div>

        {/* Preview */}
        <div className="border-t pt-6">
          <h3 className="font-semibold mb-4">Preview</h3>
          <div className="border rounded-lg p-4 bg-gray-50">
            <div className="flex justify-between items-center">
              <div>
                {formData.logoUrl ? (
                  <img src={formData.logoUrl} alt="Logo" className="h-10" />
                ) : (
                  <span className="text-xl font-bold">Gainateliê</span>
                )}
              </div>
              <button
                type="button"
                className="px-6 py-2 bg-black text-white rounded-full"
              >
                {formData.ctaText}
              </button>
            </div>
          </div>
        </div>

        {/* Actions */}
        <div className="flex gap-4 pt-6 border-t">
          <button
            type="submit"
            disabled={saving}
            className="px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 disabled:bg-gray-400"
          >
            {saving ? 'Saving...' : 'Save Changes'}
          </button>
          <button
            type="button"
            onClick={() => router.push('/dashboard/content')}
            className="px-6 py-2 bg-gray-200 rounded-lg hover:bg-gray-300"
          >
            Cancel
          </button>
        </div>
      </form>
      </div>
    </AdminLayout>
  );
}

export default function NavbarPage() {
  return (
    <ProtectedRoute>
      <NavbarEditor />
    </ProtectedRoute>
  );
}
