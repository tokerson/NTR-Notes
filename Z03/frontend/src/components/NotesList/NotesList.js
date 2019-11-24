import React from 'react';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';

export default function NotesList({ notes }) {
  return (
    <Table striped bordered hover>
      <tr>
        <th>Date</th>
        <th>Title</th>
        <th></th>
      </tr>
      {notes &&
        notes.map(note => (
          <tr>
            <td>{note.noteDate}</td>
            <td>{note.title}</td>
            <td>
              <Button variant="outline-dark">Edit</Button>
              <Button variant="dark">Delete</Button>
            </td>
          </tr>
        ))}
    </Table>
  );
}
