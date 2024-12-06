# LayeredNoiseModel

## Overview
The `LayeredNoiseModel` class is designed to manage and apply multiple layers of noise to a quantum state. It allows for the addition of different noise models, which can be applied sequentially to a given quantum state. This is particularly useful in quantum computing simulations where various types of noise can affect the performance and accuracy of quantum algorithms. The class serves as a container for these noise models and orchestrates their application.

## Variables
- `noiseLayers`: A private list that stores instances of `NoiseModel`. This list holds all the noise models that will be applied to the quantum state.

## Functions
- `AddNoiseLayer(NoiseModel noise)`: This public method allows the user to add a new `NoiseModel` instance to the `noiseLayers` list. It takes a single parameter, `noise`, which is the instance of the `NoiseModel` to be added.

- `ApplyNoise(QuantumState state)`: This public method applies each noise model in the `noiseLayers` list to the provided `QuantumState`. It iterates through all the noise models and calls their `ApplyNoise` method, passing in the `state` parameter, effectively layering the noise effects on the quantum state.