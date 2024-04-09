import { ReactNode } from 'react';

interface DashboardPageContainerProps {
  children: ReactNode;
}

export default function DashboardPageContainer({ children }: DashboardPageContainerProps) {
  return <div className="mx-auto">{children}</div>;
}
