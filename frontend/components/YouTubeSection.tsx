export default function YouTubeSection({ data }: { data: any }) {
  const videoId = data.videoUrl?.split('v=')[1]?.split('&')[0];

  return (
    <section className="py-20 bg-gray-50">
      <div className="container mx-auto px-6">
        {data.title && <h2 className="text-4xl font-bold text-center mb-12">{data.title}</h2>}
        
        {videoId && (
          <div className="aspect-video max-w-4xl mx-auto">
            <iframe
              width="100%"
              height="100%"
              src={`https://www.youtube.com/embed/${videoId}`}
              allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
              allowFullScreen
              className="rounded-lg shadow-lg"
            />
          </div>
        )}
      </div>
    </section>
  );
}
