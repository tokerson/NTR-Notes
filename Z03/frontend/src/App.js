import React, { Component } from 'react';
import './App.css';
import Container from 'react-bootstrap/Container';
import NotesContainer from './containers/NotesContainer';
import NoteEdit from './components/NoteEdit/NoteEdit';
import {BrowserRouter as Router, Route} from 'react-router-dom';

class App extends Component {
  render() {
    return (
      <Router>
        <Container>
          <Route exact path='/' component={NotesContainer} />
          <Route exact path='/notes/:id' component={NoteEdit} />
        </Container>
      </Router>
    );
  }
}

export default App;
