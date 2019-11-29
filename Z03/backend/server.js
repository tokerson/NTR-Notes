const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
const notesController = require('./controllers/NotesController');

const router = express.Router();
const app = express();
app.use(cors());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
const PORT = 8000;

router.get('/notes', notesController.get_notes);

app.use('/api', router);

app.listen(PORT, () => {
    console.log("Node server listening at port " + PORT);
});
