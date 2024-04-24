const QuestionCheckbox = ({ onClick, checked }) => {
  const handleChange = (event) => {
    onClick(event.target.checked);
  };

  return <input type="checkbox" onClick={onClick} checked={checked} />;
};

export { QuestionCheckbox };
