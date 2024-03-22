import { ReactNode } from 'react';

interface TypographyH4Props {
  children: ReactNode;
}

export function TypographyH4({ children }: TypographyH4Props) {
  return <h3 className="scroll-m-20 text-xl font-semibold tracking-tight">{children}</h3>;
}
