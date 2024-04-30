import {
  AcademicCapIcon,
  ChartBarIcon,
  CheckCircleIcon,
  CircleStackIcon,
  CodeBracketIcon,
  GlobeAsiaAustraliaIcon,
} from '@heroicons/react/24/outline';

export default function FeaturesSection() {
  const features = [
    {
      icon: <CodeBracketIcon />,
      title: 'Create Tests',
      description: 'Effortlessly create coding tests tailored to students’ needs.',
    },
    {
      icon: <AcademicCapIcon />,
      title: 'Test Your Students',
      description: 'Solve the tests created by teachers; enhance your coding skills.',
    },
    {
      icon: <CircleStackIcon />,
      title: 'Track Test Results',
      description: 'Monitor students’ written code and discuss test results, making the learning effective.',
    },
    {
      icon: <CheckCircleIcon />,
      title: 'Automate Code Check',
      description: 'Relieve teachers of the burden of manual work.',
    },
    {
      icon: <ChartBarIcon />,
      title: 'Scalability',
      description: 'Experience uninterrupted learning with our high-performance scalability.',
    },
    {
      icon: <GlobeAsiaAustraliaIcon />,
      title: 'Accessibility',
      description: 'Access from anywhere and anytime.',
    },
  ];

  return (
    <section className="pb-16">
      <div className="mx-auto max-w-screen-xl px-4 text-zinc-600 md:px-8">
        <div className="relative mt-12">
          <ul className="grid gap-2 sm:grid-cols-2 lg:grid-cols-3 ">
            {features.map((item, idx) => (
              <li key={idx} className="space-y-3 bg-white p-10 shadow-lg shadow-zinc-100">
                <div className="flex">
                  <div className="mr-3 w-6 pt-0.5 text-green-600">{item.icon}</div>
                  <h4 className="text-lg font-semibold text-zinc-800">{item.title}</h4>
                </div>
                <p>{item.description}</p>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </section>
  );
}
