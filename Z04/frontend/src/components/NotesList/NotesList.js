import React from 'react';
import moment from 'moment';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from 'react-router-dom';
import { DATE_FORMAT } from '../../constants';

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
            notes.map(({ idnote, date, title }) => (
              <tr key={title}>
                <td>{moment(date).format(DATE_FORMAT)}</td>
                <td>{title}</td>
                <td>
                  <Link to={`/notes/edit/${idnote}`}>
                    <Button variant="outline-dark">Edit</Button>
                  </Link>
                  <Button variant="dark" onClick={() => deleteNote(idnote)}>
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
