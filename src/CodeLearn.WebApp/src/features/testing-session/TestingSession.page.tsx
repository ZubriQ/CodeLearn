import { ResizableHandle, ResizablePanel, ResizablePanelGroup } from '@/components/ui/resizable.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';
import { CodeBracketIcon, CommandLineIcon } from '@heroicons/react/24/outline';
import { Button } from '@/components/ui/button.tsx';
import { ChevronLeft, ChevronRight, RotateCw } from 'lucide-react';
import { Badge } from '@/components/ui/badge.tsx';

export default function TestingSessionPage() {
  return (
    <div className="flex h-screen flex-col bg-zinc-50 p-4">
      <div className="mb-4">
        {/* Row for green boxes with text */}
        <div className="-mt-1 mb-2 flex flex-wrap items-center space-x-2 space-y-1">
          <span className="whitespace-nowrap font-medium">Questions</span>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            1
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            2
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            3
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            4
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            5
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            6
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            7
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            8
          </Button>
        </div>

        {/* Row for blue boxes with text */}
        <div className="-mt-1 flex flex-wrap items-center space-x-2 space-y-1">
          <span className="whitespace-nowrap font-medium">Exercises</span>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            1
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            2
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            3
          </Button>
          <Button className="h-8 w-8 rounded-md" variant="outline">
            4
          </Button>
        </div>
      </div>

      {/* Resizable panels for the main content */}
      <ResizablePanelGroup direction="horizontal" className="flex flex-1 overflow-hidden">
        {/* Left panel for exercise descriptions */}
        <ResizablePanel defaultSize={50} maxSize={60} className="overflow-auto">
          <div className="grid h-full grid-rows-[auto_1fr] gap-2">
            {/* Exercise number, Exercise difficulty, Testing time left, Topics */}
            <div className="space-x-2 space-y-1">
              <Badge variant="secondary" className="truncate">
                Remaining time
              </Badge>
              <Badge variant="secondary" className="truncate">
                Exercise 1
              </Badge>
              <Badge variant="secondary" className="truncate">
                Exercise difficulty
              </Badge>
              <Badge variant="secondary" className="truncate">
                Topic 1
              </Badge>
              <Badge variant="secondary" className="truncate">
                Topic 2
              </Badge>
              <Badge variant="secondary" className="truncate">
                Topic 3
              </Badge>
            </div>

            {/* Exercise description and related content */}
            <div className="space-y-3 overflow-auto rounded-xl border bg-zinc-100 p-4">
              <h2 className="text-2xl font-semibold">Exercise Title</h2>

              <p className="text-sm leading-6">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et
                dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip
                ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu
                fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                deserunt mollit anim id est laborum.
              </p>

              <div>
                <h3 className="font-semibold">Example 1:</h3>
                <pre className="rounded bg-zinc-200/70 p-2">...</pre>
              </div>
              <div>
                <h3 className="font-semibold">Example 2:</h3>
                <pre className="rounded bg-zinc-200/70 p-2">...</pre>
              </div>
              <div>
                <h3 className="font-semibold">Example 3:</h3>
                <pre className="rounded bg-zinc-200/70 p-2">...</pre>
              </div>

              <div>
                <h3 className="font-semibold">Notes:</h3>
                <ul className="list-inside list-disc ">
                  <li>Note 1...</li>
                  <li>Note 2...</li>
                </ul>
              </div>
            </div>
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
              <Textarea className="mb-9 flex-1 resize-none rounded-sm" />
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
                <Button variant="secondary" className="hover:bg-zinc-200">
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
