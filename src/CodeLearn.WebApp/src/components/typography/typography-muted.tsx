import { ReactNode } from 'react';

interface TypographyMutedProps {
  children: ReactNode;
}

export function TypographyMuted({ children }: TypographyMutedProps) {
  return <h3 className="text-muted-foreground text-md mt-4 border-b border-zinc-200 pb-7 text-zinc-600">{children}</h3>;
}
