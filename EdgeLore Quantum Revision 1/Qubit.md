# Qubit Class Documentation

## Overview
The `Qubit` class represents a quantum bit (qubit) in a quantum computing context. It encapsulates the concept of superposition, where a qubit can exist in both the |0⟩ and |1⟩ states simultaneously, represented by their respective amplitudes. This class is designed to ensure that the amplitudes are normalized, which is a fundamental requirement in quantum mechanics to maintain the integrity of quantum states. The `Qubit` class fits into the larger codebase by providing a foundational building block for quantum algorithms and simulations.

## Variables

- **Amplitude0**: A `float` representing the amplitude associated with the |0⟩ quantum state. This value indicates the probability amplitude for measuring the qubit in the |0⟩ state.
  
- **Amplitude1**: A `float` representing the amplitude associated with the |1⟩ quantum state. This value indicates the probability amplitude for measuring the qubit in the |1⟩ state.

## Functions

- **Qubit(float amplitude0, float amplitude1)**: Constructor that initializes the qubit with specified amplitudes for the |0⟩ and |1⟩ states. It also calls the `Normalize()` function to ensure that the amplitudes are properly normalized upon creation.

- **Normalize()**: This method normalizes the amplitudes of the qubit. It calculates the magnitude of the amplitudes and divides each amplitude by this magnitude to ensure that the sum of the squares of the amplitudes equals 1.

- **ToString()**: Overrides the default `ToString()` method to provide a string representation of the qubit's state. It returns a formatted string that displays the amplitudes for |0⟩ and |1⟩ states, making it easier to visualize the state of the qubit.