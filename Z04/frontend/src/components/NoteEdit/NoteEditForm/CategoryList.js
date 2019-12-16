import React from 'react';
import ListGroup from 'react-bootstrap/ListGroup';

export default function CategoryList({ categories, chooseCategory }) {
  return (
    <ListGroup>
      {categories.map((title, i) => (
        <ListGroup.Item
          type="button"
          onClick={() => chooseCategory(title)}
          action
          variant="secondary"
          key={i}
        >
          {title}
        </ListGroup.Item>
      ))}
    </ListGroup>
  );
}
