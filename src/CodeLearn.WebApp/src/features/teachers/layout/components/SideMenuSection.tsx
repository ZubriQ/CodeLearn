function SideMenuSection() {
  return (
    <div className="border-default border-b px-6 py-5">
      <ul className="space-y-1">
        <li>
          <a className="block" target="_self" href={link}>
            <span className="border-default ring-foreground group-hover:border-foreground-muted group flex max-w-full cursor-pointer items-center space-x-2 py-1 font-normal outline-none focus-visible:z-10 focus-visible:ring-1">
              <span className="text-foreground-light group-hover:text-foreground w-full truncate text-sm transition">
                {link}
              </span>
            </span>
          </a>
        </li>
      </ul>
    </div>
  );
}

export default SideMenuSection;
