import { SideMenuLink } from './SideMenuLink.ts';

type SideMenuSectionLinksListProps = {
  links: SideMenuLink[];
};

function SideMenuSectionLinksList(props: SideMenuSectionLinksListProps) {
  return (
    <ul className="space-y-1">
      {props.links.map((link) => (
        <li key={link.id} className="flex items-center">
          {link.icon && <span className="mr-2 w-4 pt-0.5 text-gray-400">{link.icon}</span>}

          <a className="block flex-grow" target="_self" href={link.href}>
            <span className="border-default ring-foreground group-hover:border-foreground-muted group flex max-w-full cursor-pointer space-x-2 py-1 font-normal text-gray-500 outline-none transition hover:text-gray-700">
              {link.name}
            </span>
          </a>
        </li>
      ))}
    </ul>
  );
}

export default SideMenuSectionLinksList;
