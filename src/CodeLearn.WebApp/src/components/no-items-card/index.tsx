import { cn } from '@/lib/utils.ts';

type NoItemsCardProps = {
  itemName: string;
  itemNamePlural: string;
  className?: string;
};

function NoItemsCard(props: NoItemsCardProps) {
  const containerClasses = cn(
    'flex h-[150px] min-w-72 flex-col items-center justify-center rounded-md border border-dashed text-sm',
    props.className,
  );

  return (
    <div className={containerClasses}>
      <div className="text-center">
        <h3 className="text-lg font-normal text-zinc-800">No {props.itemNamePlural} found</h3>
        <p className="text-zinc-500">Get started by creating a new {props.itemName}.</p>
      </div>
    </div>
  );
}

export default NoItemsCard;
