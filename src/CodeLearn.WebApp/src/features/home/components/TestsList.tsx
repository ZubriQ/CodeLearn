export default function TestsList() {
  const features = [
    {
      title: 'Test 1',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    },
    {
      title: 'Test 2',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    },
  ];

  return (
    <section className="pb-14 pt-6">
      <div className="mx-auto max-w-screen-xl px-4 text-zinc-600 md:px-8">
        <div className="relative mx-auto max-w-2xl sm:text-center">
          <div className="relative z-10">
            <h3 className="text-3xl font-semibold text-zinc-800 sm:text-4xl">Available tests</h3>
            <p className="mt-3">Take part in tests to test your knowledge.</p>
          </div>
          <div
            className="absolute inset-0 mx-auto h-44 max-w-xs blur-[118px]"
            style={{
              background: 'linear-gradient(152.92deg, rgba(134, 239, 172, 0.2) 4.54%, rgba(134, 239, 172, 0.26) 34.2%)',
            }}
          ></div>
        </div>

        <div className="relative mt-12">
          <ul className="grid gap-8 sm:grid-cols-1 lg:grid-cols-2">
            {features.map((item, idx) => (
              <li key={idx} className="space-y-3 rounded-lg border bg-white p-4 shadow-sm">
                <h4 className="text-lg font-semibold text-zinc-800">{item.title}</h4>
                <p>{item.description}</p>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </section>
  );
}
