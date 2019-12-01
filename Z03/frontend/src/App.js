import React, { Component } from 'react';
import './App.css';
import Container from 'react-bootstrap/Container';
import NotesContainer from './containers/NotesContainer';
import NoteContainer from './containers/NoteContainer';
import NoteNew from './components/NoteNew/NoteNew';
import {BrowserRouter as Router, Route} from 'react-router-dom';

class App extends Component {
  render() {
    return (
      <Router>
        <Container>
          <Route exact path='/' component={NotesContainer} />
          <Route exact path='/notes/edit/:title' component={NoteContainer} />
          <Route exact path='/notes/new' component={NoteNew} />
        </Container>
      </Router>
    );
  }
}

export default App;
