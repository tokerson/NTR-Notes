import React from 'react';
import { Formik } from 'formik';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Dropdown from 'react-bootstrap/Dropdown';

const NoteFilters = props => {
  const [category, setCategory] = React.useState('All');
  const [startDate, setStartDate] = React.useState(null);
  const [endDate, setEndDate] = React.useState(null);
  const categories = ['All', ...props.categories];

  const handleOnSubmit = (values, { setSubmitting }) => {
    category === 'All'
      ? props.setFilteredNotes(props.notes)
      : props.setFilteredNotes(
          props.notes.filter(note => note.categories.includes(category))
        );
  };

  return (
    <Row style={{ margin: '20px 0' }}>
      <Formik
        initialValues={{ startDate, endDate, category }}
        // validate={validate}
        onSubmit={handleOnSubmit}
      >
        {({
          values,
          handleChange,
          handleSubmit,
          submitForm,
          isSubmitting,
        }) => (
          <Form onSubmit={handleSubmit} inline>
            <Form.Group controlId="formNoteDate">
              <Form.Label>Start Date:</Form.Label>
              <Form.Control
                type="date"
                name="startDate"
                onChange={handleChange}
                value={values.startDate}
              />
            </Form.Group>
            <Form.Group controlId="formNoteDate" style={{ margin: '0 10px' }}>
              <Form.Label>End Date:</Form.Label>
              <Form.Control
                type="date"
                name="endDate"
                onChange={handleChange}
                value={values.endDate}
              />
            </Form.Group>
            Category:
            <Dropdown>
              <Dropdown.Toggle variant="light" id="dropdown-basic">
                {category}
              </Dropdown.Toggle>

              <Dropdown.Menu>
                {categories.map(category => (
                  <Dropdown.Item
                    key={category}
                    onClick={() => setCategory(category)}
                  >
                    {category}
                  </Dropdown.Item>
                ))}
              </Dropdown.Menu>
            </Dropdown>
            <Button type="submit" variant="dark" style={{ margin: '0 10px' }}>
              Fitler
            </Button>
            <Button onClick={() => {
              setCategory('All');
              submitForm();
            }} variant="dark">
              Clear
            </Button>
          </Form>
        )}
      </Formik>
    </Row>
  );
};
export default NoteFilters;
