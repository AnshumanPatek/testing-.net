import Image from 'next/image';

export default function About({ data }: { data: any }) {
  return (
    <section className="py-20">
      <div className="container mx-auto px-6">
        <div className="grid md:grid-cols-2 gap-12 items-center">
          <div>
            {data.title && <h2 className="text-4xl font-bold mb-6">{data.title}</h2>}
            {data.content && <p className="text-lg text-gray-700 leading-relaxed">{data.content}</p>}
          </div>
          
          {data.imageUrl && (
            <div className="relative h-96">
              <Image
                src={data.imageUrl}
                alt={data.title || 'About'}
                fill
                className="object-cover rounded-lg"
              />
            </div>
          )}
        </div>
      </div>
    </section>
  );
}
