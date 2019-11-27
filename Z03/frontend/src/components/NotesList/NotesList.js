import React from 'react';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import { Link } from 'react-router-dom';

export default function NotesList({ notes }) {
  return (
    <div>
      <Table striped bordered hover>
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
      </Table>
      <Link to="/notes/new">
        <Button>New</Button>
      </Link>
    </div>
  );
}
