import React from 'react';
import axios from 'axios';
import NoteEditForm from '../components/NoteEdit/NoteEditForm/NoteEditForm';

import { API } from '../constants';

const NoteContainer = props => {
  const [note, setNote] = React.useState();
  React.useEffect(() => {
    axios
      .get(`${API}/notes/${props.match.params.id}`)
      .then(res => res.data)
      .then(({ data }) => {
        setNote(data);
      })
      .catch(err => console.log(err.message));
  }, []);

  return (
    <div>
      <h1>Edit Note</h1>
      {note && (
        <NoteEditForm
          mode="edit"
          idnote={note.idnote}
          timestamp={note.timestamp}
          title={note.title}
          content={note.description}
          categories={note.categories}
          markdown={note.isMarkdown}
          date={note.date}
        />
      )}
    </div>
  );
};

export default NoteContainer;
