import React from 'react';
import NotesList from '../components/NotesList/NotesList';
import NoteFilters from '../components/NoteFilters/NoteFilters';
import axios from 'axios';
import { API } from '../constants';

const NotesContainer = () => {
  const [notes, setNotes] = React.useState([]);
  const [filteredNotes, setFilteredNotes] = React.useState([]);
  const [categories, setCategories] = React.useState([]);
  const [pager, setPager] = React.useState({});

  React.useEffect(() => {
    loadPage();
  });

  const loadPage = () => {
    const params = new URLSearchParams(location.search);
    const page = parseInt(params.get('page')) || 1;
    if(page !== pager.currentPage) {
      axios.get(`${API}/notes?page=${page}`).then(res => res.data).then(({ pager, pageOfNotes, categories }) => {
        setPager(pager);
        setNotes(pageOfNotes);
        setFilteredNotes(pageOfNotes);
        setCategories(categories);
      })
    }
  }

  const deleteNote = title => {
    axios
      .delete(`${API}/notes/${title}`)
      .then(res => {
        if (res.data === 'Success') {
          setNotes(notes.filter(note => note.title !== title));
          setFilteredNotes(filteredNotes.filter(note => note.title !== title));
        }
      })
      .catch(err => {
        console.log(err);
      });
  };



  return (
    <div>
      <NoteFilters
        style={{ marginTop: '20px' }}
        notes={notes}
        setFilteredNotes={setFilteredNotes}
        categories={categories}
      />
      <NotesList pager={pager} notes={filteredNotes} deleteNote={deleteNote} />
    </div>
  );
};

export default NotesContainer;
