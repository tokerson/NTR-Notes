import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import NotesContainer from './containers/NotesContainer';

class App extends Component {
  render() {
    return (
      <div className="App">
        <NotesContainer />
      </div>
    );
  }
}

export default App;
