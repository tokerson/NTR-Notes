import React from 'react';
import NotesList from '../components/NotesList/NotesList';
import NoteFilters from '../components/NoteFilters/NoteFilters';
import axios from 'axios';
import Spinner from 'react-bootstrap/Spinner';
import { API, DATE_FORMAT } from '../constants';
import { useStateValue } from '../state';

const NotesContainer = () => {
  const [notes, setNotes] = React.useState([]);
  const [categories, setCategories] = React.useState([]);
  const [pager, setPager] = React.useState({});
  const [{ page, category, startDate, endDate }, dispatch] = useStateValue();

  React.useEffect(() => {
    loadPage();
  }, [category, startDate, endDate, page]);

  const loadPage = () => {
    const params = new URLSearchParams(location.search);
    axios
      .get(
        `${API}/notes?page=${page}&category=${category ||
          'All'}&startDate=${startDate &&
          startDate.format(DATE_FORMAT)}&endDate=${endDate &&
          endDate.format(DATE_FORMAT)}`
      )
      .then(res => res.data)
      .then(({ pager, pageOfNotes, categories }) => {
        setPager(pager);
        setNotes(pageOfNotes);
        setCategories(categories);
      });
  };

  const deleteNote = title => {
    axios
      .delete(`${API}/notes/${title}`)
      .then(res => {
        if (res.data === 'Success') {
          setNotes(notes.filter(note => note.title !== title));
        }
      })
      .catch(err => {
        console.log(err);
      });
  };

  const setPage = page => {
    dispatch({
      type: 'changePage',
      newPage: page,
    });
  };

  const setFilters = (newCategory, newStartDate, newEndDate) => {
    dispatch({
      type: 'changeCategoryFilter',
      newCategory: newCategory,
    });
    dispatch({
      type: 'changeStartDate',
      newStartDate: newStartDate,
    });
    dispatch({
      type: 'changeEndDate',
      newEndDate: newEndDate,
    });
  };

  return (
    <div>
      <NoteFilters
        style={{ marginTop: '20px' }}
        notes={notes}
        categories={categories}
        setFilters={setFilters}
        category={category}
        startDate={startDate}
        endDate={endDate}
      />
      {notes ? (
        <NotesList
          pager={pager}
          setPage={setPage}
          notes={notes}
          deleteNote={deleteNote}
        />
      ) : (
        <Spinner animation="border" role="status">
          <span className="sr-only">Loading...</span>
        </Spinner>
      )}
    </div>
  );
};

export default NotesContainer;
