import Image from 'next/image';

export default function ProjectDetail({ project }: { project: any }) {
  const details = project.detailsJson ? JSON.parse(project.detailsJson) : [];
  const highlights = project.highlightsJson ? JSON.parse(project.highlightsJson) : [];

  return (
    <div className="pt-24 pb-20">
      <div className="container mx-auto px-6">
        <div className="mb-12">
          <h1 className="text-5xl font-bold mb-4">{project.title}</h1>
          {project.brandName && <p className="text-2xl text-gray-600">{project.brandName}</p>}
        </div>

        {details.length > 0 && (
          <div className="grid md:grid-cols-4 gap-6 mb-12">
            {details.map((detail: any, i: number) => (
              <div key={i}>
                <p className="text-sm text-gray-500 mb-1">{detail.label}</p>
                <p className="font-semibold">{detail.value}</p>
              </div>
            ))}
          </div>
        )}

        {highlights.length > 0 && (
          <div className="mb-12">
            <h2 className="text-2xl font-bold mb-4">Project Highlights</h2>
            <ul className="list-disc list-inside space-y-2">
              {highlights.map((highlight: string, i: number) => (
                <li key={i}>{highlight}</li>
              ))}
            </ul>
          </div>
        )}

        {project.descriptionHtml && (
          <div
            className="prose max-w-none mb-12"
            dangerouslySetInnerHTML={{ __html: project.descriptionHtml }}
          />
        )}

        <div className="grid md:grid-cols-2 gap-6">
          {project.images?.map((image: any, i: number) => (
            <div key={i} className="relative h-96">
              <Image
                src={image.url}
                alt={image.caption || `Project image ${i + 1}`}
                fill
                className="object-cover rounded-lg"
              />
              {image.caption && (
                <p className="mt-2 text-sm text-gray-600">{image.caption}</p>
              )}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
