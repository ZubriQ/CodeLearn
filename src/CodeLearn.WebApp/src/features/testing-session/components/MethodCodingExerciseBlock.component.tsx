import { ChevronLeft, ChevronRight, RotateCw } from 'lucide-react';
import { CodeBracketIcon, CommandLineIcon } from '@heroicons/react/24/outline';
import { Textarea } from '@/components/ui/textarea.tsx';
import { Button } from '@/components/ui/button.tsx';

interface MethodCodingExerciseProps {
  methodSolutionCode: string;
  onMethodSolutionCodeChange: (value: string) => void;
  handleBack: () => void;
  handleReset: () => void;
  handleSendMethodCodingSolution: () => void;
  handleNext: () => void;
  outputTextareaValue: string;
}

export default function MethodCodingExerciseBlock({
  methodSolutionCode,
  onMethodSolutionCodeChange,
  handleBack,
  handleReset,
  handleSendMethodCodingSolution,
  handleNext,
  outputTextareaValue,
}: MethodCodingExerciseProps) {
  return (
    <>
      <div className="mb-4 flex-1 overflow-hidden rounded-xl border bg-white p-4">
        <div className="-mx-4 -mt-4 flex items-center rounded-t-lg bg-green-600 p-1.5 text-white">
          <CodeBracketIcon width="20" height="20" className="mx-2 min-w-5" />
          <p className="truncate font-semibold">Solution</p>
        </div>
        <div className="mt-4 flex h-full flex-col">
          <Textarea
            className="mb-9 flex-1 resize-none rounded-sm"
            value={methodSolutionCode}
            onChange={(e) => onMethodSolutionCodeChange(e.target.value)}
          />
        </div>
      </div>

      <div className="flex-none rounded-xl border bg-white px-4">
        <div className="-mx-4 flex items-center rounded-t-lg bg-green-600 p-1.5 text-white">
          <CommandLineIcon width="20" height="20" className="mx-2 min-w-5" />
          <p className="truncate font-semibold">Output</p>
        </div>
        <div className="mt-4 flex flex-col">
          <Textarea className="h-28 resize-none rounded-sm" readOnly={true} value={outputTextareaValue} />
        </div>
        <div className="mb-4 mt-4 flex flex-wrap justify-between gap-2 space-x-2">
          <div className="flex space-x-2">
            <Button variant="outline" onClick={handleBack}>
              <ChevronLeft width="18" className="-ml-1" />
              Back
            </Button>
            <Button variant="secondary" className="hover:bg-zinc-200" onClick={handleReset}>
              <RotateCw width="16" className="-ml-1 mr-2" />
              Reset
            </Button>
          </div>
          <div className="flex space-x-2">
            <Button className="bg-green-600 hover:bg-green-700" onClick={handleSendMethodCodingSolution}>
              Attempt
            </Button>
            <Button variant="outline" onClick={handleNext}>
              Next
              <ChevronRight width="18" className="-mr-1" />
            </Button>
          </div>
        </div>
      </div>
    </>
  );
}
