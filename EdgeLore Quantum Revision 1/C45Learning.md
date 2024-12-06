# C45Learning Script Documentation

## Overview
The `C45Learning` class implements the C4.5 learning algorithm for constructing decision trees within the Unity environment. This algorithm processes input data to learn a model that can predict output labels by recursively splitting data based on the best attributes. It integrates with the `DecisionTreeV3` class, which represents the structure of the decision tree itself.

## Variables
- `tree`: An instance of `DecisionTreeV3` that represents the decision tree being constructed.
- `thresholds`: A two-dimensional array of doubles that stores the calculated thresholds for continuous attributes used for splitting the data.
- `splitStep`: An integer that defines the step size at which continuous attributes are split. The default value is 1.

## Properties
- `SplitStep`: 
  - **Type**: `int`
  - **Description**: Gets or sets the split step for continuous attributes. It must be greater than zero; otherwise, an `ArgumentOutOfRangeException` is thrown.

## Functions
- `C45Learning(DecisionTreeV3 tree)`: 
  - **Description**: Constructor that initializes a new instance of the C4.5 learning algorithm. Throws an `ArgumentNullException` if the provided `tree` is null.

- `DecisionTreeV3 Learn(double[][] inputs, int[] outputs)`: 
  - **Description**: Learns a model that maps the provided input data to output labels using the C4.5 algorithm. It first calculates thresholds and then recursively splits the decision tree nodes. Throws an `InvalidOperationException` if the tree is not initialized.

- `private double[][] CalculateThresholds(double[][] inputs, int[] outputs)`: 
  - **Description**: Calculates the split thresholds for continuous attributes based on the provided input data and output labels.

- `private void Split(DecisionNodeV3 node, double[][] inputs, int[] outputs, int depth)`: 
  - **Description**: Recursively splits the decision tree nodes based on the best attribute and threshold. It assigns the output for leaf nodes and handles depth limitations.

- `private List<int>[] Partition(double[][] inputs, int[] outputs, int attribute, double threshold)`: 
  - **Description**: Partitions the input data into two groups based on the specified attribute and threshold.

- `private double CalculateInformationGain(int[] outputs, List<int>[] partitions)`: 
  - **Description**: Computes the information gain for a potential split, which helps determine the quality of the split.

- `private double CalculateEntropy(int[] outputs)`: 
  - **Description**: Calculates the entropy of a set of outputs, which is a measure of the uncertainty in the output labels.

## Additional Classes
- `DecisionNodeV3`: Represents a node in the decision tree, containing properties for the split attribute, threshold, output, and child nodes (left and right).
- `DecisionTreeV3`: Represents the decision tree itself, holding the root node and the maximum depth of the tree.
- `ArrayExtensions`: Provides extension methods for manipulating arrays, including methods for getting subsets of arrays based on specified indices.