import Header from '@/components/header';
import Hero from '@/components/hero';
import Footer from '@/components/footer';
import FeaturesSection from '@/components/features-section';

export default function HomePage() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <Hero />
      <FeaturesSection />
      <Footer />
    </div>
  );
}
