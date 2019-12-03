import React from 'react';
import './App.css';
import Container from 'react-bootstrap/Container';
import NotesContainer from './containers/NotesContainer';
import NoteContainer from './containers/NoteContainer';
import NoteNew from './components/NoteNew/NoteNew';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { StateProvider } from './state';

const App = () => {
  const initialState = {
    page: 1,
    category: 'Web',
    startDate: null,
    endDate: null,
  };

  const reducer = (state, action) => {
    switch (action.type) {
      case 'changePage':
        return {
          ...state,
          page: action.newPage,
        };
      case 'changeCategoryFilter':
        return {
          ...state,
          category: action.newCategory,
        };
      case 'changeStartDate':
        return {
          ...state,
          startDate: action.newStartDate,
        };
      case 'changeEndDate':
        return {
          ...state,
          endDate: action.newEndDate,
        };
      default:
        return state;
    }
  };

  return (
    <StateProvider initialState={initialState} reducer={reducer}>
      <Router>
        <Container>
          <Route exact path="/" component={NotesContainer} />
          <Route exact path="/notes/edit/:title" component={NoteContainer} />
          <Route exact path="/notes/new" component={NoteNew} />
        </Container>
      </Router>
    </StateProvider>
  );
};

export default App;
