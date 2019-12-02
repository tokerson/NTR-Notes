import React from 'react';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import { Link } from 'react-router-dom';

export default function NotesList({ notes, deleteNote, pager }) {
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
            notes.map(({ date, title }) => (
              <tr key={title}>
                <td>{date}</td>
                <td>{title}</td>
                <td>
                  <Link to={`/notes/edit/${title}`}>
                    <Button variant="outline-dark">Edit</Button>
                  </Link>
                  <Button variant="dark" onClick={() => deleteNote(title)}>
                    Delete
                  </Button>
                </td>
              </tr>
            ))}
        </tbody>
      </Table>
      <Link to="/notes/new">
        <Button>New</Button>
      </Link>
      <Link to={`/?page=${pager.currentPage - 1}`}>
        <Button disabled={pager.currentPage === 1}>Prev Page</Button>
      </Link>
      {`${pager.currentPage} / ${pager.endPage}`}
      <Link to={`/?page=${pager.currentPage + 1}`}>
        <Button disabled={pager.currentPage === pager.endPage}>Next Page</Button>
      </Link>
    </Row>
  );
}
