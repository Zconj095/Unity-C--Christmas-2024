# EntanglementManager

## Overview
The `EntanglementManager` class is designed to handle the process of entangling states in a two-dimensional vector space. This script is part of a larger codebase that likely involves quantum mechanics or game mechanics based on quantum principles. The main function, `Entangle`, takes a superposed state and a flux value to create an entangled state, which can be used in various applications such as simulations, games, or visualizations.

## Variables
- **superposedState (Vector2)**: A two-dimensional vector representing the initial state that is to be entangled.
- **flux (float)**: A floating-point value representing the intensity of the entanglement effect, expected to range between 0 and 10.
- **normalizedFlux (float)**: A normalized version of the flux value, clamped between 0 and 1, used as a probability weight in the entanglement calculations.
- **entangledStateA (Vector2)**: The first part of the entangled pair calculated from the superposed state and normalized flux.
- **entangledStateB (Vector2)**: The second part of the entangled pair, which is the inverse of the superposed state adjusted based on the normalized flux.
- **finalEntangledState (Vector2)**: The final output of the entanglement process, which is a linear interpolation between `entangledStateA` and `entangledStateB`.

## Functions
- **Entangle(Vector2 superposedState, float flux)**: This method performs the entanglement process. It logs the initial state and flux, normalizes the flux to create a probability weight, calculates two entangled states based on the superposed state and normalized flux, and combines these states into a final entangled state. It also logs the intermediate and final results for debugging purposes.