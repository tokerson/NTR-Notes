import React from 'react';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from 'react-router-dom';

export default function NotesList({ notes, deleteNote, pager, setPage }) {
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
      <Row>
        <Col>
          <Link to="/notes/new">
            <Button>New</Button>
          </Link>
        </Col>
        <Button
          variant="secondary"
          onClick={() => setPage(pager.currentPage - 1)}
          disabled={pager.currentPage === 1 || !pager.currentPage}
        >
          Prev Page
        </Button>
        {`${pager.currentPage || 1} / ${pager.endPage || 1}`}
        <Button
          variant="secondary"
          onClick={() => setPage(pager.currentPage + 1)}
          disabled={pager.currentPage === pager.endPage || !pager.currentPage}
        >
          Next Page
        </Button>
      </Row>
    </Row>
  );
}
