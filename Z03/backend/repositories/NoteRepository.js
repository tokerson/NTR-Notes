const path = require('path');
const directoryPath = path.join(__dirname, '../data');
const fs = require('fs');
const moment = require('moment');

let Note = require('../models/Note');

module.exports = class NoteRepository {
  constructor() {
    this.categories = [];
    this.extensions = ['txt', 'md'];
  }

  findAll() {
    const files = fs.readdirSync(directoryPath);

    const notes = files.map(file => {
      const note = new Note();
      const [title, extension] = file.split('.');
      note.title = title;
      if (extension === 'md') {
        note.markdown = true;
      }
      const fileContent = fs.readFileSync(directoryPath + '/' + file, 'utf-8');
      const lines = fileContent.split('\n');

      lines[0]
        .split(':')[1]
        .split(',')
        .forEach(category => {
          category = category.trim();
          if (category.length === 0) return;

          if (!this.categories.includes(category)) {
            this.categories.push(category);
          }
          note.categories.push(category);
        });

      note.date = lines[1].split(':')[1].trim();
      return note;
    });
    return {
      categories: this.categories.map(category => ({
        title: category,
      })),
      notes,
    };
  }

  findByTitle(title) {
    let note = new Note(title);
    if (!this.noteExists(note)) {
      throw Error(`Note with title='${title}' doesn't exist`);
    }
    if (fs.existsSync(`${directoryPath}/${title}.md`)) {
      note.markdown = true;
    }

    const fileContent = fs.readFileSync(
      directoryPath + '/' + title + (note.markdown ? '.md' : '.txt'),
      'utf-8'
    );
    const lines = fileContent.split('\n');

    lines[0]
      .split(':')[1]
      .split(',')
      .forEach(category => {
        category = category.trim();
        if (category.length === 0) return;
        note.categories.push({title: category});
      });

    note.date = lines[1].split(':')[1].trim();
    for (let i = 2; i < lines.length; i++) {
      note.content += lines[i];
    }
    return note;
  }

  save(note) {
    if (this.noteExists(note)) {
      throw new Error('File with this title already exists');
    }

    const extension = note.markdown ? '.md' : '.txt';
    let fileContent = 'category: ';
    note.categories.forEach(({ title }) => (fileContent += `${title}, `));
    fileContent += '\ndate: ';
    fileContent += moment(note.date).format('YYYY/MM/DD') + '\n';
    fileContent += note.content;
    fs.writeFile(
      `${directoryPath}/${note.title}${extension}`,
      fileContent,
      err => {
        if (err) throw err;
      }
    );
  }

  noteExists(note) {
    return (
      fs.existsSync(`${directoryPath}/${note.title}.txt`) ||
      fs.existsSync(`${directoryPath}/${note.title}.md`)
    );
  }

  delete(title) {
    if (fs.existsSync(`${directoryPath}/${title}.txt`)) {
      fs.unlinkSync(`${directoryPath}/${title}.txt`);
    } else if (fs.existsSync(`${directoryPath}/${title}.md`)) {
      fs.unlinkSync(`${directoryPath}/${title}.md`);
    } else throw Error(`Error deleting note ${title}`);
  }

  update(oldTitle, newNote) {
    if (oldTitle !== newNote.title && this.noteExists(newNote)) {
      throw Error(`Note with title='${title}' already exists`);
    }

    this.delete(oldTitle);
    this.save(newNote);
  }
};
