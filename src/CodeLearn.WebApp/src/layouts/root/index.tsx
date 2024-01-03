import { Outlet } from 'react-router-dom';
import Header from '@/components/header';
import Hero from '@/components/hero';
import Footer from '@/components/footer';
import FeaturesSection from '@/components/features-section';

export default function RootLayout() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <Hero />
      <FeaturesSection />
      <Outlet />
      <Footer />
    </div>
  );
}
