import Header from './Header.tsx';
import Hero from './Hero.tsx';
import Footer from './Footer.tsx';
import FeaturesSection from './FeaturesSection.tsx';
import TestsSet from './TestsSet.tsx';

function HomePage() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <Hero />
      <FeaturesSection />
      <TestsSet />
      <Footer />
    </div>
  );
}

export default HomePage;
