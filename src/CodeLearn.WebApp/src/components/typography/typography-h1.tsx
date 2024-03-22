import { ReactNode } from 'react';

interface TypographyH1Props {
  children: ReactNode;
}

export function TypographyH1({ children }: TypographyH1Props) {
  return <h3 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">{children}</h3>;
}
