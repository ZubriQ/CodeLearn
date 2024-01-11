type DashboardHeaderProps = {
  title: string;
};

function DashboardHeader(props: DashboardHeaderProps) {
  return (
    <div className="border-default flex h-12 max-h-12 items-center justify-between border-b px-5 py-2">
      <span className="truncate text-sm font-medium text-gray-500">{props.title}</span>
    </div>
  );
}

export default DashboardHeader;
