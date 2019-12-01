import React from 'react';
import axios from 'axios';
import NoteEditForm from '../components/NoteEdit/NoteEditForm/NoteEditForm';

import { API } from '../constants';

const NoteContainer = props => {
  const [note, setNote] = React.useState();
  React.useEffect(() => {
    axios
      .get(`${API}/notes/${props.match.params.title}`)
      .then(res => res.data)
      .then(({ data }) => {
          console.log(data);
        setNote(data);
      })
      .catch(err => console.log(err.message));
  }, []);

  return (
    <div>
      <h1>Edit Note</h1>
      {note && <NoteEditForm
        mode="edit"
        title={note.title}
        content={note.content}
        categories={note.categories}
        markdown={note.markdown}
        date={note.date}
      />}
    </div>
  );
};

export default NoteContainer;
