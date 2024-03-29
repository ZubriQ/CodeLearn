import { ReactNode } from 'react';

interface TypographyH2Props {
  children: ReactNode;
}

export function TypographyH2({ children }: TypographyH2Props) {
  return <h3 className="scroll-m-20 text-3xl font-semibold tracking-tight first:mt-0">{children}</h3>;
}
