# AerJob

## Overview
The `AerJob` class represents a job that can be executed using an `AerSimulator` in a Unity environment. It encapsulates the details of a quantum circuit, including its job ID, the circuit itself, and the number of qubits involved. The primary function of this class is to execute the job, simulate the quantum circuit, and retrieve results, which can then be displayed or processed further. This class fits into a larger codebase that likely involves quantum computing simulations, providing a structured way to manage and execute jobs.

## Variables
- **JobId**: A string that uniquely identifies the job.
- **Circuit**: A list of strings representing the quantum circuit to be simulated.
- **NumQubits**: An integer that specifies the number of qubits being used in the simulation.
- **Result**: A dictionary that stores the results of the simulation, where the key is a string representing the quantum state and the value is a float representing the probability of that state.

## Functions
- **AerJob(string jobId, List<string> circuit, int numQubits)**: Constructor that initializes the `AerJob` instance with a unique job ID, a circuit, and the number of qubits.

- **async Task Execute(AerSimulator simulator)**: This asynchronous method executes the job using the provided `AerSimulator`. It checks if the simulator is initialized, runs the simulation in a separate task, and stores the results. If the simulator is null, it logs an error and sets the result to null.

- **private void DisplayJobResult(AerJob job)**: This private method displays the results of the specified job. It checks if the results are null, logs an error if they are, and if valid results exist, it logs each state and its corresponding probability.