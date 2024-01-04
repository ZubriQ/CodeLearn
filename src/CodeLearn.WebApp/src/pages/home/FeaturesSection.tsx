import {
  AcademicCapIcon,
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
      title: 'Automated Code Check',
      description: 'Relieve teachers of the burden of manual work.',
    },
    {
      icon: <GlobeAsiaAustraliaIcon />,
      title: 'Accessibility',
      description: 'Access from anywhere and anytime.',
    },
  ];

  return (
    <section className="pb-16">
      <div className="mx-auto max-w-screen-xl px-4 text-gray-600 md:px-8">
        <div className="relative mt-12">
          <ul className="grid gap-8 sm:grid-cols-2 lg:grid-cols-3 ">
            {features.map((item, idx) => (
              <li key={idx} className="space-y-3 rounded-lg border bg-white p-4 shadow-sm">
                <div className="m-3 w-6 pb-3 text-green-600">{item.icon}</div>
                <h4 className="text-lg font-semibold text-gray-800">{item.title}</h4>
                <p>{item.description}</p>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </section>
  );
}
