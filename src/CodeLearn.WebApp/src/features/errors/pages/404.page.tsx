import Error from '@/features/errors/components/Error.tsx';

function NotFound404Page() {
  return (
    <Error
      statusCode={404}
      title={'Page not found'}
      description={'Sorry, we couldn’t find the page you’re looking for.'}
    />
  );
}

export default NotFound404Page;
