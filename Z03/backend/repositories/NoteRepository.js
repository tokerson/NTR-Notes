const path = require('path');
const directoryPath = path.join(__dirname, '../data');
const fs = require('fs');

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
    return { categories: this.categories, notes };
  }
};
