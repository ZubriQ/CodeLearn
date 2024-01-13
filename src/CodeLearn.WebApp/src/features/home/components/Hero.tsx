import { Link } from 'react-router-dom';

export default function Hero() {
  return (
    <div className="relative isolate px-6 pt-14 md:px-8">
      <div
        className="absolute inset-x-0 -top-40 -z-10 transform-gpu overflow-hidden blur-3xl md:-top-80"
        aria-hidden="true"
      >
        <div
          className="relative left-[calc(50%-11rem)] aspect-[1155/678] w-[36.125rem] -translate-x-1/2 rotate-[30deg] bg-gradient-to-tr from-[#d9f99d] to-[#86efac] opacity-30 md:left-[calc(50%-30rem)] md:w-[72.1875rem]"
          style={{
            clipPath:
              'polygon(74.1% 44.1%, 100% 61.6%, 97.5% 26.9%, 85.5% 0.1%, 80.7% 2%, 72.5% 32.5%, 60.2% 62.4%, 52.4% 68.1%, 47.5% 58.3%, 45.2% 34.5%, 27.5% 76.7%, 0.1% 64.9%, 17.9% 100%, 27.6% 76.8%, 76.1% 97.7%, 74.1% 44.1%)',
          }}
        />
      </div>

      <div
        className="absolute inset-x-0 -top-40 -z-10 transform-gpu overflow-hidden blur-3xl md:-top-80"
        aria-hidden="true"
      ></div>
      <div className="mx-auto max-w-2xl py-24 pb-14 md:py-40 md:pb-24">
        <div className="text-center">
          <h1 className="text-4xl font-bold tracking-tight text-gray-900 md:text-6xl">
            Welcome to{' '}
            <span className="inline-block bg-gradient-to-r from-teal-500 via-green-500 to-lime-500 bg-clip-text text-transparent">
              CodeLearn
            </span>
          </h1>
          <p className="mt-6 bg-clip-text text-xl leading-8 text-gray-600">
            Join us and experience a new way of learning.
            <br />
            We automate the teaching of coding by allowing teachers to create tests, and students to solve them.
          </p>
          <div className="mt-10 flex items-center justify-center gap-x-6">
            <Link
              to="all-tests"
              className="rounded-md bg-green-600 px-3.5 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-green-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-green-600"
            >
              See available tests
            </Link>
            <Link to="about" className="text-sm font-semibold leading-6 text-gray-900">
              Learn more <span aria-hidden="true">→</span>
            </Link>
          </div>
        </div>
      </div>

      <div
        className="absolute inset-x-0 top-[calc(100%-13rem)] -z-10 transform-gpu overflow-hidden blur-3xl md:top-[calc(100%-30rem)]"
        aria-hidden="true"
      >
        <div
          className="relative left-[calc(50%+3rem)] aspect-[1155/678] w-[36.125rem] -translate-x-1/2 bg-gradient-to-tr from-[#d9f99d] to-[#86efac] opacity-30 md:left-[calc(50%+36rem)] md:w-[72.1875rem]"
          style={{
            clipPath:
              'polygon(74.1% 44.1%, 100% 61.6%, 97.5% 26.9%, 85.5% 0.1%, 80.7% 2%, 72.5% 32.5%, 60.2% 62.4%, 52.4% 68.1%, 47.5% 58.3%, 45.2% 34.5%, 27.5% 76.7%, 0.1% 64.9%, 17.9% 100%, 27.6% 76.8%, 76.1% 97.7%, 74.1% 44.1%)',
          }}
        />
      </div>
    </div>
  );
}
