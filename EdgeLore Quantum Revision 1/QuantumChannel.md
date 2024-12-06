# QuantumChannel

## Overview
The `QuantumChannel` class is a Unity script that simulates a quantum depolarizing channel. This channel applies a transformation to a given input state, representing the effect of noise in quantum systems. The main function of this script, `ApplyChannel`, takes a three-dimensional vector representing the input state and returns a modified output state that reflects the depolarization effect. This script can be integrated into a larger codebase that deals with quantum mechanics simulations or visualizations in Unity.

## Variables

- **depolarizingProbability**: A `double` that represents the probability of depolarization occurring. It is serialized to allow for easy adjustment in the Unity Inspector. The default value is set to `0.1`, meaning there is a 10% chance that the input state will be altered.

## Functions

- **ApplyChannel(Vector3 inputState)**: 
  - This function takes a `Vector3` parameter called `inputState`, which represents the state of a quantum system. 
  - It calculates the output state by reducing the input state based on the `depolarizingProbability`. Specifically, it multiplies the input state by `(1 - probability)`, where `probability` is the float cast of `depolarizingProbability`.
  - The function logs the output state to the console for debugging purposes and returns the modified `Vector3` output state.