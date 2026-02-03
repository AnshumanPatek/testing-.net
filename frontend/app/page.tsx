// 'use client'


import { getHomeContent } from '@/lib/api';
import Hero from '@/components/Hero';
import YouTubeSection from '@/components/YouTubeSection';
import About from '@/components/About';
import FeaturedProjects from '@/components/FeaturedProjects';

export default async function Home() {
  const content = await getHomeContent();

  return (
    <main>
      {content.hero && <Hero data={content.hero} />}
      {content.youtube && <YouTubeSection data={content.youtube} />}
      {content.about && <About data={content.about} />}
      <FeaturedProjects />
    </main>
  );
}
