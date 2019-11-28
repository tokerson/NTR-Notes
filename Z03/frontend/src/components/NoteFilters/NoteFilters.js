import React from 'react';
import { Formik } from 'formik';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Dropdown from 'react-bootstrap/Dropdown';
import FiltersForm from './FiltersForm';

export default function NoteFilters() {
  const [categories, setCategories] = React.useState(['All', 'Sport', 'NTR']);
  const [category, setCategory] = React.useState('All');
  const [startDate, setStartDate] = React.useState(null);
  const [endDate, setEndDate] = React.useState(null);

  const handleOnSubmit = (values, { setSubmitting }) => {
    setTimeout(() => {
      alert(JSON.stringify(values, null, 2));
      setSubmitting(false);
    }, 400);
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
          //   errors,
          //   touched,
            handleChange,
          handleSubmit,
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
            <Form.Group controlId="formNoteDate" style={{margin:"0 10px"}}>
              <Form.Label>End Date:</Form.Label>
              <Form.Control
                type="date"
                name="endDate"
                onChange={handleChange}
                value={values.endDate}
              />
            </Form.Group>
            <Button type="submit" variant="dark" style={{margin:"0 10px"}}>
              Fitler
            </Button>
            <Button type="submit" variant="dark">
              Clear
            </Button>
          </Form>
        )}
      </Formik>
    </Row>
  );
}
