import React from 'react';
import NotesList from '../components/NotesList/NotesList';
import NoteFilters from '../components/NoteFilters/NoteFilters';
import axios from 'axios';
import { API } from '../constants';

const NotesContainer = () => {
  const [notes, setNotes] = React.useState([]);
  const [filteredNotes, setFilteredNotes] = React.useState([]);
  const [categories, setCategories] = React.useState([]);

  React.useEffect(() => {
    axios
      .get(`${API}/notes`)
      .then(res => res.data)
      .then(({ data }) => {
        setCategories(data.categories);
        setFilteredNotes(data.notes);
        setNotes(data.notes);
      });
  }, []);

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
      <NotesList notes={filteredNotes} deleteNote={deleteNote} />
    </div>
  );
};

export default NotesContainer;
