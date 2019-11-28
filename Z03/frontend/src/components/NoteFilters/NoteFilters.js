import React from 'react';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Dropdown from 'react-bootstrap/Dropdown';

export default function NoteFilters() {
    const [chosenCategory, setChosenCategory] = React.useState('All');
    const [categories, setCategories] = React.useState(['All', 'Sport', 'NTR']);
    return (
        <Row style={{margin:"20px 0"}}>
          <Dropdown>
          <Dropdown.Toggle variant="outline-dark" id="dropdown-basic">
            {chosenCategory}
          </Dropdown.Toggle>
            <Dropdown.Menu>
              { categories.map((category) => (<Dropdown.Item key={category} onClick={() => setChosenCategory(category)}>
                  {category}
                </Dropdown.Item>))
              }
            </Dropdown.Menu>
          </Dropdown>
          <Button variant="dark">Fitler</Button>
          <Button variant="dark">Clear</Button>
        </Row>
    );
}
