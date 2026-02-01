import { getNavbar } from '@/lib/api';
import Link from 'next/link';
import Image from 'next/image';

export default async function Navbar() {
  const navbar = await getNavbar();

  return (
    <nav className="fixed top-0 w-full bg-white/90 backdrop-blur-sm z-50 border-b">
      <div className="container mx-auto px-6 py-4 flex justify-between items-center">
        <Link href="/">
          {navbar.logoUrl ? (
            <Image src={navbar.logoUrl} alt="Gainateliê" width={150} height={40} />
          ) : (
            <span className="text-2xl font-bold">Gainateliê</span>
          )}
        </Link>
        
        {navbar.ctaUrl && (
          <a
            href={navbar.ctaUrl}
            target="_blank"
            rel="noopener noreferrer"
            className="px-6 py-2 bg-black text-white rounded-full hover:bg-gray-800 transition"
          >
            {navbar.ctaText}
          </a>
        )}
      </div>
    </nav>
  );
}
