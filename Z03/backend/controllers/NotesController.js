let Note = require('../models/Note');
let NoteRepository = require('../repositories/NoteRepository');

exports.get_notes = (req, res) => {
  const noteRepository = new NoteRepository();
  const data = noteRepository.findAll();
  res.send({ data: data });
};

exports.get_note = (req, res) => {
  const title = req.params.title;

  const noteRepository = new NoteRepository();
  const data = noteRepository.findByTitle(title);
  res.send({ data: data });
};

exports.post_note = (req, res, next) => {
  let note = new Note(
    req.body.title,
    req.body.date,
    req.body.categories,
    req.body.markdown,
    req.body.content
  );

  const noteRepository = new NoteRepository();
  try {
    noteRepository.save(note);
  } catch (err) {
    return res.send(err.message);
  }
  res.send('Success');
};

exports.delete_note = (req, res) => {
  const title = req.params.title;

  const noteRepository = new NoteRepository();
  try {
    noteRepository.delete(title);
  } catch (err) {
    return res.send(err.message);
  }
  res.send('Success');
};

exports.update_note = (req, res) => {
  const oldTitle = req.params.title;

  let newNote = new Note(
    req.body.title,
    req.body.date,
    req.body.categories,
    req.body.markdown,
    req.body.content
  );

  const noteRepository = new NoteRepository();
  try {
    noteRepository.update(oldTitle, newNote);
  } catch (err) {
    return res.send(err.message);
  }
  res.send('Success');
};
