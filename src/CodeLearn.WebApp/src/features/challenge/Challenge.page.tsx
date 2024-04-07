import { ResizableHandle, ResizablePanel, ResizablePanelGroup } from '@/components/ui/resizable.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';
import { CodeBracketIcon, CommandLineIcon } from '@heroicons/react/24/outline';
import { Button } from '@/components/ui/button.tsx';
import { ChevronLeft, ChevronRight, RotateCw } from 'lucide-react';

export default function ChallengePage() {
  return (
    <div className="flex h-screen flex-col bg-zinc-50 p-4">
      {/* Header with exercise types */}
      <div className="mb-4">
        <div className="mb-2 flex space-x-2">
          <div className="h-10 w-10 rounded bg-green-300"></div>
          <div className="h-10 w-10 rounded bg-green-300"></div>
          {/* ...more exercises */}
        </div>
        <div className="flex space-x-2">
          <div className="h-10 w-10 rounded bg-blue-300"></div>
          <div className="h-10 w-10 rounded bg-blue-300"></div>
          {/* ...more exercises */}
        </div>
      </div>

      {/* Resizable panels for the main content */}
      <ResizablePanelGroup direction="horizontal" className="flex flex-1 overflow-hidden">
        {/* Left panel for exercise descriptions */}
        <ResizablePanel
          defaultSize={50}
          maxSize={60}
          className="overflow-auto rounded-xl border border-zinc-200 bg-white p-4"
        >
          {/* Exercise number, Exercise difficulty, Testing time left, Topics */}
          <div></div>

          {/* Exercise description and related content */}
          <div className="h-full p-4">
            <h1 className="mb-2 text-lg font-semibold">Exercise Title</h1>
            <p>Description...</p>
            <h2 className="font-semibold">Examples</h2>
            <pre className="rounded bg-zinc-200 p-2">...</pre>
            <h2 className="font-semibold">Notes</h2>
            <ul className="list-inside list-disc rounded bg-zinc-200 p-2">
              <li>Note 1...</li>
              <li>Note 2...</li>
            </ul>
          </div>
        </ResizablePanel>

        <ResizableHandle withHandle className="mx-2" />

        {/* Right panel for the code input */}
        <ResizablePanel defaultSize={50} minSize={40} className="flex h-full flex-col">
          {/* Solution */}
          <div className="mb-4 flex-1 overflow-hidden rounded-xl border bg-white p-4">
            {/* Header */}
            <div className="-mx-4 -mt-4 flex items-center rounded-t-lg bg-green-600 p-1.5 text-white">
              <CodeBracketIcon width="20" height="20" className="mx-2" />
              <p className="font-semibold">Solution</p>
            </div>
            {/* Textarea container */}
            <div className="mt-4 flex h-full flex-col">
              {/* Stretch the textarea to fill the container */}
              <Textarea className="mb-8 flex-1 resize-none rounded-sm" />
            </div>
          </div>

          {/* Output */}
          <div className="flex-none rounded-xl border bg-white px-4">
            {/* Header */}
            <div className="-mx-4 flex items-center rounded-t-lg bg-green-600 p-1.5 text-white">
              <CommandLineIcon width="20" height="20" className="mx-2" />
              <p className="font-semibold">Output</p>
            </div>
            <div className="mt-4 flex flex-col">
              {/* Stretch the textarea to fill the container */}
              <Textarea className="h-28 resize-none rounded-sm" readOnly={true} />
            </div>
            {/* Buttons at the bottom */}
            <div className="mb-4 mt-4 flex flex-wrap justify-between gap-2 space-x-2">
              <div className="flex space-x-2">
                <Button variant="outline">
                  <ChevronLeft width="18" className="-ml-1" />
                  Back
                </Button>
                <Button variant="secondary">
                  <RotateCw width="16" className="-ml-1 mr-2" />
                  Reset
                </Button>
              </div>
              <div className="flex space-x-2">
                <Button className="bg-green-600 hover:bg-green-700">Attempt</Button>
                <Button variant="outline">
                  Next
                  <ChevronRight width="18" className="-mr-1" />
                </Button>
              </div>
            </div>
          </div>
        </ResizablePanel>
      </ResizablePanelGroup>
    </div>
  );
}
