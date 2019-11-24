import React from 'react';
import ListGroup from 'react-bootstrap/ListGroup';

export default function CategoryList({ categories, chooseCategory }) {
  console.log(categories);
  return (
    <ListGroup>
      {categories.map(({title}) => (
        <ListGroup.Item onClick={() => chooseCategory(title)} action variant="secondary" key={title}>{title}</ListGroup.Item>
      ))}
    </ListGroup>
  );
}
