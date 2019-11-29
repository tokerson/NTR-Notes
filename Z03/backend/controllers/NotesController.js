exports.get_notes = (req, res) => {

  res.send({ data: "hello"});
};

exports.get_all_trainings = (req, res) => {
  Training.find({}, function(err, trainings) {
    trainings = trainings.map(training => {
        return {
          _id: training._id,
          exercises: training.exercises,
          date: training.date
        };
      });

    res.send(trainings);
  });
};

exports.get_all_exercises = (req, res) => {
   Training.distinct('exercises.name').exec((err, result)=>{
       console.log(result);
       res.send(result);
    });
};

exports.post_training = (req, res) => {
  let training = new Training({
    exercises: req.body.exercises,
    date: req.body.date || Date.now()
  });

  training.save(err => {
    if (err) {
      return next(err);
    }
    res.send("Training added successfully");
  });
};
