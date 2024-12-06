# HybridARIMA

## Overview
The `HybridARIMA` script is designed to optimize a hybrid ARIMA (AutoRegressive Integrated Moving Average) model using various optimization techniques, including grid search, genetic algorithms, and random search. This script is part of a larger codebase focused on time series forecasting, where it aims to identify the best parameters for the ARIMA model and enhance predictions by incorporating quantum adjustments. It utilizes a list of time series data for training and evaluation against actual observed data.

## Variables

- **minP**: The minimum value for the ARIMA parameter `p`, which represents the number of lag observations included in the model.
- **maxP**: The maximum value for the ARIMA parameter `p`.
- **minD**: The minimum value for the ARIMA parameter `d`, which indicates the degree of differencing.
- **maxD**: The maximum value for the ARIMA parameter `d`.
- **minQ**: The minimum value for the ARIMA parameter `q`, representing the size of the moving average window.
- **maxQ**: The maximum value for the ARIMA parameter `q`.
- **minAmplitude**: The minimum amplitude for the quantum adjustment in predictions.
- **maxAmplitude**: The maximum amplitude for the quantum adjustment in predictions.
- **minIterations**: The minimum number of iterations for the quantum adjustment simulation.
- **maxIterations**: The maximum number of iterations for the quantum adjustment simulation.
- **timeSeries**: A list of float values representing the historical time series data used for training the model.
- **actualData**: A list of float values representing the ground truth data for model evaluation.
- **bestP**: The best value found for the ARIMA parameter `p` after optimization.
- **bestD**: The best value found for the ARIMA parameter `d` after optimization.
- **bestQ**: The best value found for the ARIMA parameter `q` after optimization.
- **bestAmplitude**: The best amplitude value for quantum adjustments after optimization.
- **bestIterations**: The best number of iterations for quantum adjustments after optimization.
- **bestError**: The lowest error value encountered during the optimization process.

## Functions

- **Start()**: Initializes the optimization process, performing grid search, genetic algorithm, and random search to find the best model parameters. Outputs the best parameters found.

- **PerformGridSearch()**: Iterates through all combinations of ARIMA parameters and quantum settings to find the configuration that yields the lowest prediction error.

- **GeneratePredictions(int p, int d, int q, float amplitude, int iterations)**: Generates predictions based on the ARIMA parameters and quantum adjustments. It first performs differencing on the time series data.

- **PerformDifferencing(List<float> data, int degree)**: Applies differencing to the input data for the specified degree, which is essential for making the data stationary.

- **QuantumARIMAPredict(List<float> data, int arOrder, int maOrder, float amplitude, int iterations)**: Combines autoregressive (AR) and moving average (MA) components with quantum adjustments to produce forecasted values.

- **ARComponent(List<float> data, List<float> forecast, int order)**: Calculates the autoregressive component of the forecast based on the previous forecasted values.

- **MAComponent(List<float> data, List<float> forecast, int order)**: Calculates the moving average component of the forecast based on the actual data.

- **SimulateQuantumAdjustment(float amplitude, int iterations)**: Simulates quantum adjustments by generating random phase values and applying a sine function to create interference effects.

- **EvaluateModel(List<float> actual, List<float> predicted)**: Computes the mean squared error between the actual data and the predicted values to assess model performance.

- **PerformRandomSearch(int iterations)**: Randomly samples parameter values and evaluates the model to find the best configuration over a specified number of iterations.

- **InitializePopulation(int populationSize)**: Creates an initial population of candidate solutions for the genetic algorithm, evaluating their performance.

- **Crossover(Candidate parent1, Candidate parent2)**: Combines two parent candidates to produce a new candidate by randomly selecting parameters from each parent.

- **Mutate(Candidate candidate)**: Introduces random changes to a candidate's parameters to explore new solutions.

- **PerformGeneticAlgorithm(int populationSize, int generations)**: Executes the genetic algorithm, evolving the population over a number of generations to find the best candidate solution.

- **VisualizeProgress(int generation, float bestError)**: Creates a visual representation of the optimization progress using Unity's primitive shapes (cubes) to indicate best errors over generations.

- **ExportResults()**: Exports the best model parameters to a text file for future reference or analysis.