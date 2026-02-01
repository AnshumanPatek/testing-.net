'use client';

export default function Hero({ data }: { data: any }) {
  return (
    <section className="relative h-screen flex items-center justify-center">
      {data.backgroundType === 'Video' && data.backgroundUrl ? (
        <video
          autoPlay
          loop
          muted
          playsInline
          className="absolute inset-0 w-full h-full object-cover"
        >
          <source src={data.backgroundUrl} type="video/mp4" />
        </video>
      ) : data.backgroundUrl ? (
        <div
          className="absolute inset-0 bg-cover bg-center"
          style={{ backgroundImage: `url(${data.backgroundUrl})` }}
        />
      ) : null}
      
      <div className="relative z-10 text-center text-white px-6">
        <h1 className="text-6xl md:text-8xl font-bold mb-4">{data.headline}</h1>
        <p className="text-xl md:text-2xl">{data.tagline}</p>
      </div>
      
      <div className="absolute inset-0 bg-black/30" />
    </section>
  );
}
