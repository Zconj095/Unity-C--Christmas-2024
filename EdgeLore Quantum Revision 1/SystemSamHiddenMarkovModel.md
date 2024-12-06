# SystemSamHiddenMarkovModel

## Overview
The `SystemSamHiddenMarkovModel` script is a Unity MonoBehaviour that calculates the state of a Hidden Markov Model (HMM) based on two input vectors: `HopfieldInput` and `SVMInput`. This script is designed to be part of a larger codebase that likely involves machine learning or state estimation processes, where different models contribute to the overall state computation. The main function, `ComputeState`, combines the two input vectors to produce a single output vector representing the computed state.

## Variables
- `HopfieldInput`: A `Vector3` representing the input from a Hopfield network. This variable is used as one of the sources for computing the HMM state.
- `SVMInput`: A `Vector3` representing the input from a Support Vector Machine (SVM). This variable is the second source for computing the HMM state.

## Functions
- `ComputeState()`: This function computes the current state of the Hidden Markov Model by averaging the `HopfieldInput` and `SVMInput` vectors. It logs the computation process to the console for debugging purposes and returns the resulting `Vector3` that represents the computed state. The function performs the following steps:
  1. Logs a message indicating that the state computation is in progress.
  2. Averages the `HopfieldInput` and `SVMInput` vectors.
  3. Logs the computed HMM state to the console.
  4. Returns the computed state as a `Vector3`.