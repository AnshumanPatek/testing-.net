import { getFooter } from '@/lib/api';

export default async function Footer() {
  const footer = await getFooter();

  return (
    <footer className="bg-gray-900 text-white py-12">
      <div className="container mx-auto px-6">
        <div className="grid md:grid-cols-3 gap-8">
          <div>
            <h3 className="text-xl font-bold mb-4">Contact</h3>
            {footer.email && <p>{footer.email}</p>}
            {footer.phone && <p>{footer.phone}</p>}
            {footer.address && <p className="mt-2">{footer.address}</p>}
          </div>
          
          <div>
            <h3 className="text-xl font-bold mb-4">Follow Us</h3>
            <div className="flex gap-4">
              {footer.instagram && (
                <a href={footer.instagram} target="_blank" rel="noopener noreferrer">
                  Instagram
                </a>
              )}
              {footer.linkedin && (
                <a href={footer.linkedin} target="_blank" rel="noopener noreferrer">
                  LinkedIn
                </a>
              )}
              {footer.behance && (
                <a href={footer.behance} target="_blank" rel="noopener noreferrer">
                  Behance
                </a>
              )}
            </div>
          </div>
          
          <div>
            <p className="text-sm text-gray-400">{footer.copyright}</p>
          </div>
        </div>
      </div>
    </footer>
  );
}
