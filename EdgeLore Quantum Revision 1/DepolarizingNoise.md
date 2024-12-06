# DepolarizingNoise

## Overview
The `DepolarizingNoise` class is a specific implementation of a noise model that simulates depolarizing noise on a quantum state. It extends from the `NoiseModel` class and overrides the `ApplyNoise` method to introduce noise based on a specified probability. This class is part of a larger quantum computing simulation framework, where it helps to model the effects of noise on quantum states, which is crucial for understanding and mitigating errors in quantum computations.

## Variables

- **probability**: A double value inherited from the base `NoiseModel` class representing the likelihood that depolarizing noise will be applied to the quantum state.

## Functions

- **DepolarizingNoise(double probability)**: Constructor that initializes a new instance of the `DepolarizingNoise` class. It takes a `probability` parameter which sets the likelihood of applying noise to the quantum state and passes it to the base class constructor along with the string "Depolarizing Noise".

- **void ApplyNoise(QuantumState state)**: This method applies depolarizing noise to the provided `QuantumState` object. It generates a random number and checks if it is less than the specified probability. If so, it reduces the real and imaginary components of each element in the `state.StateVector` by 1% (multiplying by 0.99), simulating the effect of depolarizing noise. The method also logs a message indicating that noise is being applied.