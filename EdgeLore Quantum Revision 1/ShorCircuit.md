# ShorCircuit

## Overview
The `ShorCircuit` script is designed to implement a simplified version of Shor's Algorithm, which is a quantum algorithm used for factoring integers. The main function, `ExecuteShor`, takes an integer as input and simulates the steps of the algorithm by logging messages to the console. This script serves as an educational tool within the codebase, illustrating the fundamental processes involved in Shor's Algorithm and providing a framework for further development in quantum computing simulations.

## Variables
- **numberToFactor**: An integer input parameter for the `ExecuteShor` function that specifies the number to be factored using Shor's Algorithm.

## Functions
- **ExecuteShor(int numberToFactor)**: This public method initiates the execution of Shor's Algorithm for the specified integer. It logs the process of applying the Quantum Fourier Transform, performing modular exponentiation, and measuring the result, culminating with a completion message. This function serves as the main entry point for executing the algorithm within the Unity environment.