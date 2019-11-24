import React from 'react';
import NotesList from '../components/NotesList/NotesList';
import NoteFilters from '../components/NoteFilters/NoteFilters';

const notes = [
    {
        "noteDate": "2018-09-04",
        "title": "Hello World",
    },{
        "noteDate": "2018-09-04",
        "title": "Hello Wisza",
    },
]

class NotesContainer extends React.Component {
  render() {
    return (
      <div>
        <h1>NotesContainer</h1>
        <NoteFilters />
        <NotesList notes={notes}/>
      </div>
    );
  }
}

export default NotesContainer;
