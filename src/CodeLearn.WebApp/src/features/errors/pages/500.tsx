import Error from '@/features/errors/components/Error.tsx';

function Error500Page() {
  return <Error statusCode={500} title={'Error occurred'} description={'Sorry, something went wrong.'} />;
}

export default Error500Page;
