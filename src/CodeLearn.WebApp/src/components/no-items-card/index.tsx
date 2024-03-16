type NoItemsCardProps = {
  itemName: string;
  itemNamePlural: string;
};

function NoItemsCard(props: NoItemsCardProps) {
  return (
    <div className="flex h-[150px] min-w-72 flex-col items-center justify-center rounded-md border border-dashed text-sm">
      <div className="text-center">
        <h3 className="text-lg font-normal text-zinc-800">No {props.itemNamePlural}</h3>
        <p className="text-zinc-500">Get started by creating a new {props.itemName}.</p>
      </div>
    </div>
  );
}

export default NoItemsCard;
