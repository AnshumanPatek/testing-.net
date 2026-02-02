'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import ProtectedRoute from '@/components/ProtectedRoute';
import AdminLayout from '@/components/AdminLayout';
import { getHeroSection, updateHeroSection, uploadMedia, publishSection } from '@/lib/api';

function HeroEditor() {
  const router = useRouter();
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  
  const [formData, setFormData] = useState({
    headline: '',
    tagline: '',
    backgroundType: 'Image',
    backgroundUrl: '',
    backgroundId: '',
    isDraft: true
  });

  useEffect(() => {
    loadHero();
  }, []);

  const loadHero = async () => {
    try {
      const data = await getHeroSection();
      setFormData({
        headline: data.headline || '',
        tagline: data.tagline || '',
        backgroundType: data.backgroundType || 'Image',
        backgroundUrl: data.backgroundUrl || '',
        backgroundId: data.backgroundId || '',
        isDraft: data.isDraft ?? true
      });
    } catch (error) {
      console.error('Failed to load hero', error);
    } finally {
      setLoading(false);
    }
  };

  const handleBackgroundUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    try {
      setSaving(true);
      const assetType = formData.backgroundType === 'Video' ? 'Video' : 'Image';
      const media = await uploadMedia(file, assetType);
      setFormData(prev => ({
        ...prev,
        backgroundUrl: media.s3Url,
        backgroundId: media.id
      }));
    } catch (error) {
      alert('Failed to upload background');
    } finally {
      setSaving(false);
    }
  };

  const handleSaveDraft = async () => {
    setSaving(true);
    try {
      await updateHeroSection({ ...formData, isDraft: true });
      alert('Draft saved successfully!');
    } catch (error) {
      alert('Failed to save draft');
    } finally {
      setSaving(false);
    }
  };

  const handlePublish = async () => {
    setSaving(true);
    try {
      await updateHeroSection({ ...formData, isDraft: false });
      await publishSection('hero');
      alert('Hero section published successfully!');
      router.push('/dashboard/content');
    } catch (error) {
      alert('Failed to publish');
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
        <h1 className="text-3xl font-bold mb-2">Edit Hero Section</h1>
        <p className="text-gray-600">Main banner on your homepage</p>
      </div>

      <div className="bg-white rounded-lg shadow p-6 space-y-6">
        {/* Headline */}
        <div>
          <label className="block text-sm font-medium mb-2">
            Headline *
          </label>
          <input
            type="text"
            value={formData.headline}
            onChange={(e) => setFormData(prev => ({ ...prev, headline: e.target.value }))}
            className="w-full px-4 py-2 border rounded-lg text-2xl font-bold"
            placeholder="Welcome to Gainateliê"
            maxLength={255}
            required
          />
          <p className="text-sm text-gray-500 mt-1">
            {formData.headline.length}/255 characters
          </p>
        </div>

        {/* Tagline */}
        <div>
          <label className="block text-sm font-medium mb-2">
            Tagline
          </label>
          <input
            type="text"
            value={formData.tagline}
            onChange={(e) => setFormData(prev => ({ ...prev, tagline: e.target.value }))}
            className="w-full px-4 py-2 border rounded-lg"
            placeholder="Architecture & Design Studio"
            maxLength={500}
          />
          <p className="text-sm text-gray-500 mt-1">
            {formData.tagline.length}/500 characters
          </p>
        </div>

        {/* Background Type */}
        <div>
          <label className="block text-sm font-medium mb-2">
            Background Type
          </label>
          <div className="flex gap-4">
            <label className="flex items-center">
              <input
                type="radio"
                value="Image"
                checked={formData.backgroundType === 'Image'}
                onChange={(e) => setFormData(prev => ({ ...prev, backgroundType: e.target.value }))}
                className="mr-2"
              />
              Image
            </label>
            <label className="flex items-center">
              <input
                type="radio"
                value="Video"
                checked={formData.backgroundType === 'Video'}
                onChange={(e) => setFormData(prev => ({ ...prev, backgroundType: e.target.value }))}
                className="mr-2"
              />
              Video
            </label>
          </div>
        </div>

        {/* Background Upload */}
        <div>
          <label className="block text-sm font-medium mb-2">
            Background {formData.backgroundType}
          </label>
          
          {formData.backgroundUrl && (
            <div className="mb-4">
              {formData.backgroundType === 'Video' ? (
                <video src={formData.backgroundUrl} className="w-full h-48 object-cover rounded" controls />
              ) : (
                <img src={formData.backgroundUrl} alt="Background" className="w-full h-48 object-cover rounded" />
              )}
            </div>
          )}

          <div className="flex gap-4">
            <label className="px-4 py-2 bg-blue-600 text-white rounded cursor-pointer hover:bg-blue-700">
              Upload {formData.backgroundType}
              <input
                type="file"
                accept={formData.backgroundType === 'Video' ? 'video/*' : 'image/*'}
                onChange={handleBackgroundUpload}
                className="hidden"
              />
            </label>
            
            {formData.backgroundUrl && (
              <button
                type="button"
                onClick={() => setFormData(prev => ({ ...prev, backgroundUrl: '', backgroundId: '' }))}
                className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
              >
                Remove
              </button>
            )}
          </div>
          <p className="text-sm text-gray-500 mt-2">
            Recommended: {formData.backgroundType === 'Video' ? 'MP4, max 50MB' : 'JPG/PNG, 1920x1080px'}
          </p>
        </div>

        {/* Preview */}
        <div className="border-t pt-6">
          <h3 className="font-semibold mb-4">Preview</h3>
          <div className="relative h-96 bg-gray-900 rounded-lg overflow-hidden flex items-center justify-center">
            {formData.backgroundUrl && (
              formData.backgroundType === 'Video' ? (
                <video src={formData.backgroundUrl} className="absolute inset-0 w-full h-full object-cover" muted loop autoPlay />
              ) : (
                <div 
                  className="absolute inset-0 bg-cover bg-center"
                  style={{ backgroundImage: `url(${formData.backgroundUrl})` }}
                />
              )
            )}
            <div className="absolute inset-0 bg-black/30" />
            <div className="relative z-10 text-center text-white px-6">
              <h1 className="text-5xl font-bold mb-4">{formData.headline || 'Your Headline'}</h1>
              <p className="text-xl">{formData.tagline || 'Your tagline here'}</p>
            </div>
          </div>
        </div>

        {/* Status */}
        <div className="border-t pt-6">
          <div className="flex items-center gap-2">
            <span className="text-sm font-medium">Status:</span>
            <span className={`px-3 py-1 rounded text-sm ${
              formData.isDraft ? 'bg-yellow-100 text-yellow-800' : 'bg-green-100 text-green-800'
            }`}>
              {formData.isDraft ? 'Draft' : 'Published'}
            </span>
          </div>
        </div>

        {/* Actions */}
        <div className="flex gap-4 pt-6 border-t">
          <button
            type="button"
            onClick={handleSaveDraft}
            disabled={saving}
            className="px-6 py-2 bg-gray-600 text-white rounded-lg hover:bg-gray-700 disabled:bg-gray-400"
          >
            {saving ? 'Saving...' : 'Save Draft'}
          </button>
          <button
            type="button"
            onClick={handlePublish}
            disabled={saving || !formData.headline}
            className="px-6 py-2 bg-green-600 text-white rounded-lg hover:bg-green-700 disabled:bg-gray-400"
          >
            {saving ? 'Publishing...' : 'Publish'}
          </button>
          <button
            type="button"
            onClick={() => router.push('/dashboard/content')}
            className="px-6 py-2 bg-gray-200 rounded-lg hover:bg-gray-300"
          >
            Cancel
          </button>
        </div>
      </div>
      </div>
    </AdminLayout>
  );
}

export default function HeroPage() {
  return (
    <ProtectedRoute>
      <HeroEditor />
    </ProtectedRoute>
  );
}
