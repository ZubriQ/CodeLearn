import { useState } from 'react';
import { Dialog } from '@headlessui/react';
import { ArrowLeftStartOnRectangleIcon, Bars2Icon, XMarkIcon } from '@heroicons/react/24/outline';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { ROLES } from '@/constants/roles.ts';
import { RootState } from '@/app/store.ts';

export default function Header() {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const isAuthenticated = useSelector((state: RootState) => state.auth.isAuthenticated);
  const userRole = useSelector((state: RootState) => state.auth.role);

  return (
    <header className="absolute inset-x-0 top-0 z-50">
      <nav className="mx-auto flex max-w-screen-xl items-center justify-between p-6 md:px-8" aria-label="Global">
        <div className="flex md:flex-1">
          <Link to="#" className="-m-1.5 p-1.5">
            <span className="sr-only">CodeLearn</span>
            <img
              className="h-8 w-auto"
              src="https://tailwindui.com/img/logos/mark.svg?color=teal&shade=600"
              alt="CodeLearn"
            />
          </Link>
        </div>
        <div className="flex md:hidden">
          <button
            type="button"
            className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-zinc-700"
            onClick={() => setMobileMenuOpen(true)}
          >
            <span className="sr-only">Open main menu</span>
            <Bars2Icon className="h-6 w-6" aria-hidden="true" />
          </button>
        </div>
        <div className="hidden md:flex md:flex-1 md:justify-end">
          {!isAuthenticated ? (
            <Link to="sign-in" className="text-sm font-semibold leading-6 text-zinc-900">
              Sign in <span aria-hidden="true">&rarr;</span>
            </Link>
          ) : userRole === ROLES.STUDENT ? (
            <div className="flex grid-cols-2 items-center space-x-10">
              <Link to="sign-out" className="text-sm font-semibold leading-6 text-zinc-900">
                Sign out
              </Link>
              <Link to="curriculum" className="text-sm font-semibold leading-6 text-zinc-900">
                Curriculum <span aria-hidden="true">&rarr;</span>
              </Link>
            </div>
          ) : (
            <div className="flex grid-cols-2 items-center space-x-10">
              <Link to="sign-out" className="text-sm font-semibold leading-6 text-zinc-900">
                Sign out
              </Link>
              <Link to="dashboard" className="text-sm font-semibold leading-6 text-zinc-900">
                Dashboard <span aria-hidden="true">&rarr;</span>
              </Link>
            </div>
          )}
        </div>
      </nav>

      <Dialog as="div" className="md:hidden" open={mobileMenuOpen} onClose={setMobileMenuOpen}>
        <div className="fixed inset-0 z-50" />
        <Dialog.Panel className="fixed inset-y-0 right-0 z-50 w-full overflow-y-auto bg-white px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-zinc-900/10">
          <div className="flex items-center justify-between">
            <Link to="#" className="-m-1.5 p-1.5">
              <span className="sr-only">CodeLearn</span>
              <img className="h-8 w-auto" src="https://tailwindui.com/img/logos/mark.svg?color=teal&shade=600" alt="" />
            </Link>
            <button
              type="button"
              className="-m-2.5 rounded-md p-2.5 text-zinc-700"
              onClick={() => setMobileMenuOpen(false)}
            >
              <span className="sr-only">Close menu</span>
              <XMarkIcon className="h-6 w-6" aria-hidden="true" />
            </button>
          </div>
          <div className="mt-6 flow-root">
            <div className="-my-6 divide-y divide-zinc-500/10">
              <div className="py-6">
                {!isAuthenticated ? (
                  <Link
                    to="sign-in"
                    className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-zinc-900 hover:bg-zinc-50"
                  >
                    Sign in
                  </Link>
                ) : userRole === ROLES.STUDENT ? (
                  <>
                    <Link
                      to="curriculum"
                      className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-zinc-900 hover:bg-zinc-50"
                    >
                      Curriculum
                    </Link>
                    <Link
                      to="sign-out"
                      className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-zinc-900 hover:bg-zinc-50"
                    >
                      Sign out
                    </Link>
                  </>
                ) : (
                  <>
                    <Link
                      to="dashboard"
                      className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-zinc-900 hover:bg-zinc-50"
                    >
                      Dashboard
                    </Link>
                    <Link
                      to="sign-out"
                      className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-zinc-900 hover:bg-zinc-50"
                    >
                      Sign out
                    </Link>
                  </>
                )}
              </div>
            </div>
          </div>
        </Dialog.Panel>
      </Dialog>
    </header>
  );
}
