import { Header, Hero, FeaturesSection, TestsList, Footer } from '@/features/home/components';

function HomePage() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <Hero />
      <FeaturesSection />
      <TestsList />
      <Footer />
    </div>
  );
}

export default HomePage;
