import React from 'react';
import NotesList from '../components/NotesList/NotesList';
import NoteFilters from '../components/NoteFilters/NoteFilters';

const notes = [
    {
        "id": 1,
        "noteDate": "2018-09-04",
        "title": "Hello World",
    },{
        "id": 2,
        "noteDate": "2018-09-04",
        "title": "Hello Wisza",
    },
]

class NotesContainer extends React.Component {
  render() {
    return (
      <div>
        <NoteFilters />
        <NotesList notes={notes}/>
      </div>
    );
  }
}

export default NotesContainer;
