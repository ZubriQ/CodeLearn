import { SideMenuLink } from './SideMenuLink.ts';

export type SideMenuLinkGroup = {
  id: number;
  title?: string;
  links: Array<SideMenuLink>;
};
