import { Outlet } from 'react-router-dom';
import Header from '@/components/header';
import Hero from '@/components/hero';

export default function RootLayout() {
  return (
    <>
      <Header />
      <Hero />
      <Outlet />
      <div className="text-7xl text-amber-700">Footer</div>
    </>
  );
}
