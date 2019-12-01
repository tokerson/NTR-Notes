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
router.get('/notes/:title', notesController.get_note);
router.post('/notes', notesController.post_note);
router.put('/notes/:title', notesController.update_note);
router.delete('/notes/:title', notesController.delete_note);

app.use('/api', router);

app.listen(PORT, () => {
    console.log("Node server listening at port " + PORT);
});
