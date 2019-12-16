import React from 'react';
import { Formik } from 'formik';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Alert from 'react-bootstrap/Alert';
import InputGroup from 'react-bootstrap/InputGroup';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import moment from 'moment';
import axios from 'axios';
import { Link, withRouter } from 'react-router-dom';
import { API, DATE_FORMAT } from '../../../constants';

import CategoryList from './CategoryList';

const NoteEditForm = props => {
  const title = props.title || '';
  const content = props.content || '';
  const markdown = props.markdown || false;
  const date = moment(props.date).format(DATE_FORMAT) || moment().format(DATE_FORMAT);
  const category = '';
  const [chosenCategory, setChosenCategory] = React.useState('');
  const [removeEnabled, setRemoveEnabled] = React.useState(false);
  const [categories, setCategories] = React.useState(props.categories || []);
  const [errorMessage, setErrorMessage] = React.useState('');

  const validate = values => {
    const errors = {};
    if (!values.title) {
      errors.title = 'Title is Required';
    }
    return errors;
  };

  const handleOnSubmit = (values, { setSubmitting }) => {
    const date = moment(values.date).isValid ? moment(values.date) : moment();
    if (props.mode === 'new') {
      axios
        .post(`${API}/notes`, {
          title: values.title,
          content: values.content,
          markdown: values.markdown,
          date: date,
          categories: categories,
        })
        .then(res => {
          setSubmitting(false);
          if (res.data !== 'Success') {
            setErrorMessage(res.data);
          } else {
            props.history.push('/');
          }
        })
        .catch(err => {
          console.log(err);
        });
    } else if (props.mode === 'edit') {
      axios
        .put(`${API}/notes/${props.title}`, {
          title: values.title,
          content: values.content,
          markdown: values.markdown,
          date: date,
          categories: categories,
        })
        .then(res => {
          setSubmitting(false);
          if (res.data !== 'Success') {
            setErrorMessage(res.data);
          } else {
            props.history.push('/');
          }
        })
        .catch(err => {
          console.log(err);
        });
    }
  };

  const handleAddCategory = category => {
    if (category !== '') {
      if (!categoryExists(category)) {
        setCategories([...categories, { title: category }]);
      }
    }
  };

  const categoryExists = category => {
    let found = false;
    categories.forEach(({ title }) => {
      if (title === category) {
        found = true;
      }
    });
    return found;
  };

  const handleRemoveCategory = () => {
    setCategories(categories.filter(({ title }) => title !== chosenCategory));
  };

  const handleChooseCategory = category => {
    setChosenCategory(category);
    setRemoveEnabled(true);
  };

  return (
    <div>
      {errorMessage && <Alert variant="danger">{errorMessage}</Alert>}
      {console.log(date)}
      <Formik
        initialValues={{ title, content, categories, date, category, markdown }}
        validate={validate}
        onSubmit={handleOnSubmit}
      >
        {({
          values,
          errors,
          touched,
          handleChange,
          handleSubmit,
          isSubmitting,
        }) => (
          <Form onSubmit={handleSubmit}>
            <Form.Group controlId="formNoteTitle">
              <Form.Label>Note's Title:</Form.Label>
              <Form.Control
                type="text"
                name="title"
                onChange={handleChange}
                placeholder="Notes title"
                value={values.title}
              />
              {errors.title && touched.title && errors.title}
            </Form.Group>
            <Form.Check controlId="formNoteMarkdown">
              <Form.Check.Input
                type="checkbox"
                name="markdown"
                checked={values.markdown}
                onChange={handleChange}
                value={values.markdown}
              />
              {errors.title && touched.title && errors.title}
              <Form.Label>Markdown</Form.Label>
            </Form.Check>
            <Form.Group controlId="formNoteContent">
              <Form.Label>Note's Content:</Form.Label>
              <Form.Control
                type="text"
                name="content"
                onChange={handleChange}
                as="textarea"
                placeholder="Content of the note"
                value={values.content}
              />
            </Form.Group>
            <Form.Group controlId="formNoteDate">
              <Form.Label>Note's Date:</Form.Label>
              <Form.Control
                type="date"
                name="date"
                onChange={handleChange}
                value={values.date}
              />
            </Form.Group>
            <Form.Group>
              <Form.Label>Note's Categories:</Form.Label>
              <Row>
                <Col>
                  <CategoryList
                    categories={categories}
                    chooseCategory={handleChooseCategory}
                  />
                </Col>
                <Col>
                  <Form.Control
                    type="text"
                    name="category"
                    onChange={handleChange}
                    value={values.category}
                  />
                </Col>
                <Col>
                  <Row>
                    <Button
                      variant="outline-secondary"
                      onClick={() => {
                        handleAddCategory(values.category);
                        values.category = '';
                      }}
                    >
                      Add
                    </Button>
                    <Button
                      variant="secondary"
                      onClick={handleRemoveCategory}
                      disabled={!removeEnabled}
                    >
                      Remove
                    </Button>
                  </Row>
                </Col>
              </Row>
            </Form.Group>
            <Button type="submit" disabled={isSubmitting}>
              Submit
            </Button>
          </Form>
        )}
      </Formik>
      <Link to="/">
        <Button style={{ marginTop: '10px' }} variant="light">
          Back to Notes List
        </Button>
      </Link>
    </div>
  );
};

export default withRouter(NoteEditForm);
