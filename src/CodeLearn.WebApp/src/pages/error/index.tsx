import Error from '@/components/error';

function ErrorPage() {
  return <Error statusCode={500} title={'Error occurred'} description={'Sorry, something went wrong.'} />;
}

export default ErrorPage;
