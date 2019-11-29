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

  return (
    <div>
      <NoteFilters style={{ marginTop: '20px' }} notes={notes} filteredNotes={filteredNotes} setFilteredNotes={setFilteredNotes} categories={categories} />
      <NotesList notes={filteredNotes} />
    </div>
  );
};

export default NotesContainer;
