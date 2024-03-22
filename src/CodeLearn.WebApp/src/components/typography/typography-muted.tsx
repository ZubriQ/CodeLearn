import { ReactNode } from 'react';

interface TypographyMutedProps {
  children: ReactNode;
}

export function TypographyMuted({ children }: TypographyMutedProps) {
  return <h3 className="text-muted-foreground text-sm">{children}</h3>;
}
