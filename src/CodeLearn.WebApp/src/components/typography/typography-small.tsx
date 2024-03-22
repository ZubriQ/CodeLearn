import { ReactNode } from 'react';

interface TypographySmallProps {
  children: ReactNode;
}

export function TypographySmall({ children }: TypographySmallProps) {
  return <h3 className="text-sm font-medium leading-none">{children}</h3>;
}
