import { type ClassValue, clsx } from 'clsx';
import { twMerge } from 'tailwind-merge';
import { jwtDecode } from 'jwt-decode';

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

type DecodedToken = {
  [key: string]: unknown;
};

export function getRoleFromToken(token: string): string | undefined {
  try {
    const decodedToken: DecodedToken = jwtDecode(token);
    const roleClaimKey = Object.keys(decodedToken).find((key) => key.endsWith('/role') || key === 'role');

    if (!roleClaimKey) {
      return undefined;
    }

    const role = decodedToken[roleClaimKey];
    if (typeof role === 'string') {
      return role;
    }
    if (Array.isArray(role) && role.every((elem) => typeof elem === 'string')) {
      return role[0];
    }

    return undefined;
  } catch (error) {
    return undefined;
  }
}
