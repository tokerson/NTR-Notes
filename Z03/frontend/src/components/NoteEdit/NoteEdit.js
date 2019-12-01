import React from 'react'

import NoteEditForm from './NoteEditForm/NoteEditForm';

export default function NoteEdit() {
    return (
        <div>
            <h1>Edit Note</h1>
            <NoteEditForm mode="edit"/>
        </div>
    )
}
