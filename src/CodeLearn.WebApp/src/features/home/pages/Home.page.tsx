import { Header, Hero, FeaturesSection, Footer } from '../components';

function HomePage() {
  return (
    <div className="flex min-h-screen flex-col bg-zinc-50">
      <Header />
      <Hero />
      <FeaturesSection />
      <Footer />
    </div>
  );
}

export default HomePage;
