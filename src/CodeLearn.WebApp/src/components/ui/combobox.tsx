import { forwardRef } from 'react';
import { useFormContext } from 'react-hook-form';
import { CaretSortIcon, CheckIcon } from '@radix-ui/react-icons';
import { cn } from '@/lib/utils';
import { Popover, PopoverTrigger, PopoverContent } from '@/components/ui/popover';
import { Command, CommandItem, CommandList, CommandGroup } from '@/components/ui/command';
import { Button } from '@/components/ui/button.tsx';

type Option = {
  id: number;
  alias: string;
};

interface ComboboxProps {
  name: string;
  options: Option[];
  placeholder: string;
  className?: string;
  onChange?: (selectedId: number) => void;
}

const Combobox = forwardRef<HTMLButtonElement, ComboboxProps>(
  ({ name, options, placeholder, className, onChange }, ref) => {
    const { watch, setValue } = useFormContext();

    const selectedValue = watch(name);
    const selectedOption = options.find((o: Option) => o.id === selectedValue);

    const handleSelect = (selectedId: number) => {
      setValue(name, selectedId);
      if (onChange) {
        onChange(selectedId);
      }
    };

    return (
      <Popover>
        <PopoverTrigger asChild>
          <Button
            ref={ref}
            variant="outline"
            role="combobox"
            className={cn('justify-between font-normal', !selectedOption && 'text-muted-foreground', className)}
          >
            {selectedOption?.alias || placeholder}
            <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
          </Button>
        </PopoverTrigger>

        <PopoverContent className="p-0">
          <Command>
            <CommandList>
              <CommandGroup>
                {options.map((option: Option) => (
                  <CommandItem key={option.id} onSelect={() => handleSelect(option.id)}>
                    {option.alias}
                    <CheckIcon
                      className={cn('ml-auto h-4 w-4', option.id === selectedValue ? 'opacity-100' : 'opacity-0')}
                    />
                  </CommandItem>
                ))}
              </CommandGroup>
            </CommandList>
          </Command>
        </PopoverContent>
      </Popover>
    );
  },
);

export default Combobox;
