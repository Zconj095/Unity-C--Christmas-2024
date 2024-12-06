# SVMHyperdimensionalUI

## Overview
The `SVMHyperdimensionalUI` class is a Unity MonoBehaviour that implements a Support Vector Machine (SVM) algorithm for binary classification using hyperdimensional data. This script takes input vectors and their corresponding labels, allowing users to configure various SVM parameters such as the regularization parameter, kernel type, and kernel coefficients. The main functionality of this script is encapsulated in the `RunSVM` method, which executes the SVM optimization process and computes the weight vector and bias term, which are essential for classifying new data points. Additionally, the script integrates a quantum circuit simulation to enhance the SVM optimization process.

## Variables
- `inputVectors` (double[,]): A 2D array representing the input data vectors, where each row corresponds to a sample and each column corresponds to a feature.
- `labels` (int[]): An array of binary labels (-1 or 1) corresponding to each input vector.
- `C` (double): The regularization parameter that controls the trade-off between maximizing the margin and minimizing classification error.
- `kernel` (string): The type of kernel function to be used in the SVM algorithm. Options include "linear", "poly", and "rbf".
- `gamma` (double): The kernel coefficient for the 'rbf' and 'poly' kernels, influencing the decision boundary's shape.
- `degree` (int): The degree of the polynomial kernel when using the 'poly' kernel type.
- `coef0` (double): The independent term in the kernel function, applicable for 'poly' and 'rbf' kernels.
- `weightVector` (double[]): The computed weight vector after SVM optimization, used for classifying new data points.
- `bias` (double): The computed bias term after SVM optimization, used in conjunction with the weight vector.

## Functions
- `RunSVM()`: This method is triggered by a context menu action in Unity. It checks if the input vectors and labels are set, then calls the `SVMHyperdimensionalMethod` to perform SVM optimization. It logs the completion status, weight vector, and bias term to the console.

- `SVMHyperdimensionalMethod(double[,] inputVectors, int[] labels, double C = 1.0, string kernel = "linear", double gamma = 0.5, int degree = 3, double coef0 = 0.0)`: A static method that performs the SVM optimization process. It computes the kernel matrix based on the specified kernel type, simulates a quantum circuit to optimize the SVM, and extracts the optimal weight vector and bias term from the simulation results.

### QuantumCircuitUI Class
- `QuantumCircuitUI`: A nested class representing a quantum circuit for simulating the SVM optimization process.
  - `NumQubits`: Property that returns the number of qubits in the circuit.
  - `Gates`: Property that holds the list of gates applied to the circuit.

- `QuantumCircuitUI(int numQubits)`: Constructor that initializes the quantum circuit with a specified number of qubits.

- `H(int qubit)`: Method that adds a Hadamard gate operation for the specified qubit.

- `RY(double angle, int qubit)`: Method that adds a rotation around the Y-axis gate for the specified qubit with a given angle.

- `MeasureAll()`: Method that adds a measurement operation for all qubits in the circuit.

- `Append(string gate)`: Method that appends a specified gate operation to the circuit.

- `Simulate()`: Method that simulates the quantum circuit and logs the simulation status.

- `GetCounts()`: Method that returns a dictionary of measurement outcomes from the quantum circuit simulation, simulating the counts of different states.

This documentation provides a comprehensive understanding of the `SVMHyperdimensionalUI` class and its functionality within the codebase.