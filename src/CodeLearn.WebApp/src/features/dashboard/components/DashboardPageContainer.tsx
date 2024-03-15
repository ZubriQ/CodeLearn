import { ReactNode } from 'react';

interface DashboardPageContainerProps {
  children: ReactNode;
}

export default function DashboardPageContainer({ children }: DashboardPageContainerProps) {
  return <div className="mx-auto whitespace-nowrap lg:px-11 xl:px-24">{children}</div>;
}
