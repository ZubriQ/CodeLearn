import { PlusIcon } from '@heroicons/react/24/outline';
import { Button } from '@/components/ui/button.tsx';

type NoItemsCardProps = {
  itemName: string;
  itemNamePlural: string;
};

function NoItemsCard(props: NoItemsCardProps) {
  return (
    <div className="flex h-[150px] min-w-72 flex-col items-center justify-center gap-1 rounded-md border border-dashed text-sm">
      <div className="m-3.5 -mt-1 text-center">
        <h3 className="text-lg font-normal text-zinc-800">No {props.itemNamePlural}</h3>
        <p className="text-zinc-500">Get started by creating a new {props.itemName}.</p>
      </div>

      <Button>
        <PlusIcon className="-ml-1 mr-1 h-4 w-4" />
        New {props.itemName}
      </Button>
    </div>
  );
}

export default NoItemsCard;
