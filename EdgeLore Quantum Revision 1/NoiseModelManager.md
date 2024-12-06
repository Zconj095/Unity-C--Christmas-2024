# AmplitudeDampingNoise

## Overview
The `AmplitudeDampingNoise` class is a representation of a noise model that applies amplitude damping to a quantum state. This type of noise simulates the loss of quantum information due to the interaction of a quantum system with its environment. The class inherits from the `NoiseModel` base class and overrides the `ApplyNoise` method to implement the specific behavior of amplitude damping. This class is part of a larger codebase that deals with quantum computing simulations, allowing users to model the effects of noise on quantum states.

## Variables
- `probability`: A `double` value representing the probability of applying the amplitude damping noise to the quantum state. This value is passed to the base class `NoiseModel` during instantiation.
- `random`: An instance of `System.Random` used to generate random numbers to determine whether to apply the noise based on the defined probability.
- `state`: A `QuantumState` object representing the quantum state on which the noise will be applied.

## Functions
- `AmplitudeDampingNoise(double probability)`: Constructor that initializes an instance of the `AmplitudeDampingNoise` class. It takes a `double` parameter `probability`, which sets the likelihood of applying noise to the quantum state, and passes this value to the base class constructor along with the name of the noise model.
  
- `ApplyNoise(QuantumState state)`: This method overrides the `ApplyNoise` method from the `NoiseModel` class. It checks if a random number generated is less than the defined probability. If so, it logs a message indicating that amplitude damping noise is being applied. It then iterates through the `StateVector` of the `QuantumState`, modifying each element by multiplying its real part by 0.9, simulating the effect of amplitude damping. The imaginary part remains unchanged.