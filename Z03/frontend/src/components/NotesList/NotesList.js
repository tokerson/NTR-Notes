import React from 'react';

export default function NotesList({ notes }) {
  return (
    <div>
      <table>
        <tr>
            <th>Date</th>
            <th>Title</th>
            <th></th>
        </tr>
        {notes && notes.map(note => (
            <tr>
                <td>{note.noteDate}</td>
                <td>{note.title}</td>
                <td><button>Edit</button></td>
                <td><button>Delete</button></td>
            </tr>
        ))}
      </table>
    </div>
  );
}
