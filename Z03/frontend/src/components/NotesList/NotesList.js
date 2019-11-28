import React from 'react';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import { Link } from 'react-router-dom';

export default function NotesList({ notes }) {
  return (
    <Row>
      <Table striped bordered hover>
        <tbody>
        <tr>
          <th>Date</th>
          <th>Title</th>
          <th></th>
        </tr>
        {notes &&
          notes.map(({ id, noteDate, title }) => (
            <tr key={id}>
              <td>{noteDate}</td>
              <td>{title}</td>
              <td>
                <Link to={`/notes/edit/:id`}>
                  <Button variant="outline-dark">Edit</Button>
                </Link>
                <Button variant="dark">Delete</Button>
              </td>
            </tr>
          ))}
          </tbody>
      </Table>
      <Link to="/notes/new">
        <Button>New</Button>
      </Link>
    </Row>
  );
}
