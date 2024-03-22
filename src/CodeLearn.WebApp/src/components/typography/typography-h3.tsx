import { ReactNode } from 'react';

interface TypographyH3Props {
  children: ReactNode;
}

export function TypographyH3({ children }: TypographyH3Props) {
  return <h3 className="text-2xl font-semibold tracking-tight">{children}</h3>;
}
