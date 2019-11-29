module.exports = class Note {
    constructor(title, date, categories, markdown, content) {
        this.title = title || '';
        this.date = date || '';
        this.categories = categories || [];
        this.markdown = markdown || false;
        this.content = content || '';
    }
}
