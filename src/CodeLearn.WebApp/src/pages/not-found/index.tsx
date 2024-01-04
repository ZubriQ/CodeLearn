import Error from '@/components/error';

function NotFoundPage() {
  return (
    <Error
      statusCode={404}
      title={'Page not found'}
      description={'Sorry, we couldn’t find the page you’re looking for.'}
    />
  );
}

export default NotFoundPage;
