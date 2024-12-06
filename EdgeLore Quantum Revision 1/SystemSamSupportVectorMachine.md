# SystemSamSupportVectorMachine

## Overview
The `SystemSamSupportVectorMachine` script is a Unity MonoBehaviour that implements a basic prediction mechanism using a Support Vector Machine (SVM) approach. It takes in training data and input data, and computes a prediction based on the relationship between these two sets of data. This script is part of a larger codebase that likely involves machine learning or data processing within a Unity application.

## Variables
- `TrainingData`: A `Vector3` representing the data used to train the Support Vector Machine. This variable holds the foundational data that the model uses to learn and make predictions.
- `InputData`: A `Vector3` representing the new data input for which the prediction is to be made. This variable holds the current data point that the model will evaluate against the training data.

## Functions
- `Predict()`: This function computes a prediction based on the `TrainingData` and `InputData`. It logs a message indicating that a prediction is being made and calculates the prediction by multiplying the `TrainingData` with the dot product of `InputData` and `TrainingData`. The result is logged and returned as a `Vector3` prediction.