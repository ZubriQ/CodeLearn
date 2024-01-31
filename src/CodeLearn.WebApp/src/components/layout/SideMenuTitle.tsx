type SideMenuTitleProps = {
  name: string;
};

function SideMenuTitle(props: SideMenuTitleProps) {
  return (
    <div className="border-default flex h-12 max-h-12 items-center border-b px-6">
      <h4 className="mb-0 truncate text-lg font-medium text-gray-900" title={props.name}>
        {props.name}
      </h4>
    </div>
  );
}

export default SideMenuTitle;
