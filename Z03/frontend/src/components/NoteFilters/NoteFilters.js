import React from 'react';
import { Formik } from 'formik';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Dropdown from 'react-bootstrap/Dropdown';
import moment from 'moment';
import { Link, withRouter } from 'react-router-dom';

const NoteFilters = props => {
  const [category, setCategory] = React.useState(props.category);
  const [startDate, setStartDate] = React.useState(props.startDate || '');
  const [endDate, setEndDate] = React.useState(props.endDate || '');
  const categories = ['All', ...props.categories.map(({ title }) => title)];

  const handleOnSubmit = values => {
    const { setFilters } = props;
    const start = moment(values.startDate);
    const end = moment(values.endDate);
    setFilters(category, start, end);
  };

  return (
    <Row style={{ margin: '20px 0' }}>
      <Formik
        enableReinitialize
        initialValues={{ startDate, endDate, category }}
        onSubmit={handleOnSubmit}
      >
        {({
          values,
          handleChange,
          handleSubmit,
          submitForm,
          setFieldValue,
        }) => (
          <Form onSubmit={handleSubmit} inline>
            <Form.Group controlId="formNoteDate">
              <Form.Label>Start Date:</Form.Label>
              <Form.Control
                type="date"
                name="startDate"
                onChange={e => {
                  handleChange(e);
                  setStartDate(e.target.value);
                }}
                value={values.startDate || ''}
              />
            </Form.Group>
            <Form.Group controlId="formNoteDate" style={{ margin: '0 10px' }}>
              <Form.Label>End Date:</Form.Label>
              <Form.Control
                type="date"
                name="endDate"
                onChange={e => {
                  handleChange(e);
                  setEndDate(e.target.value);
                }}
                value={values.endDate || ''}
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
                    onClick={() => {
                      setCategory(category);
                    }}
                  >
                    {category}
                  </Dropdown.Item>
                ))}
              </Dropdown.Menu>
            </Dropdown>
            <Button type="submit" variant="dark" style={{ margin: '0 10px' }}>
              Fitler
            </Button>
            <Button
              onClick={async () => {
                setCategory('All');
                setStartDate('');
                setEndDate('');
                await setFieldValue('startDate', '');
                await setFieldValue('endDate', '');
                submitForm();
              }}
              variant="dark"
            >
              Clear
            </Button>
          </Form>
        )}
      </Formik>
    </Row>
  );
};
export default withRouter(NoteFilters);
