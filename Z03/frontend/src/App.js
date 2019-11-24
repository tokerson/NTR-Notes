import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import Container from 'react-bootstrap/Container';
import NotesContainer from './containers/NotesContainer';

class App extends Component {
  render() {
    return (
      <Container>
        <NotesContainer />
      </Container>
    );
  }
}

export default App;
