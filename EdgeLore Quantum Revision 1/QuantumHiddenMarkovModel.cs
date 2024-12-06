using System;
using UnityEngine;

public class QuantumHiddenMarkovModel : MonoBehaviour
{
    [Header("HMM Configuration for Grover")]
    [SerializeField] private int numStates = 3;         // Number of hidden states
    [SerializeField] private int numObservations = 4;   // Number of possible observation symbols
    [SerializeField] private int numQubits = 2;         // Number of qubits for Grover's Algorithm
    [SerializeField] private int targetState = 2;       // Target state for Grover's Algorithm (0-based index)
    [SerializeField] private int groverIterations = 2;  // Number of Grover's iterations

    private float[,] transitionProbabilities;           // Transition probabilities (states x states)
    private float[,] emissionProbabilities;             // Emission probabilities (states x observations)
    private float[] initialStateProbabilities;          // Initial state probabilities

    private GroversAlgorithm grover;                    // Grover's Algorithm component

    private void Awake()
    {
        // Attempt to auto-assign Grover's Algorithm if not assigned in the Inspector
        grover = GetComponent<GroversAlgorithm>();
        if (grover == null)
        {
            Debug.LogError("Grover's Algorithm component is not found. Ensure it is attached to the same GameObject or assigned explicitly.");
        }
    }

    void Start()
    {
        // Initialize the HMM with random probabilities
        InitializeHMM();

        // Run Grover's Algorithm to adjust probabilities
        RunGroversAlgorithm();
    }

    /// <summary>
    /// Initializes the HMM parameters with normalized probabilities.
    /// </summary>
    private void InitializeHMM()
    {
        Debug.Log($"Initializing HMM with {numStates} states and {numObservations} observation symbols.");

        transitionProbabilities = new float[numStates, numStates];
        emissionProbabilities = new float[numStates, numObservations];
        initialStateProbabilities = new float[numStates];

        for (int i = 0; i < numStates; i++)
        {
            // Initialize initial state probabilities uniformly
            initialStateProbabilities[i] = 1.0f / numStates;

            // Initialize transition probabilities randomly and normalize
            float transitionSum = 0f;
            for (int j = 0; j < numStates; j++)
            {
                transitionProbabilities[i, j] = UnityEngine.Random.Range(0.1f, 1.0f);
                transitionSum += transitionProbabilities[i, j];
            }
            NormalizeProbabilities(transitionProbabilities, i, jMax: numStates);
            Debug.Log($"Transition probabilities for state {i}: {string.Join(", ", GetRow(transitionProbabilities, i))}");

            // Initialize emission probabilities randomly and normalize
            float emissionSum = 0f;
            for (int k = 0; k < numObservations; k++)
            {
                emissionProbabilities[i, k] = UnityEngine.Random.Range(0.1f, 1.0f);
                emissionSum += emissionProbabilities[i, k];
            }
            NormalizeProbabilities(emissionProbabilities, i, jMax: numObservations);
            Debug.Log($"Emission probabilities for state {i}: {string.Join(", ", GetRow(emissionProbabilities, i))}");
        }

        Debug.Log("HMM Initialization Complete.");
    }

    /// <summary>
    /// Normalizes the probabilities for a given row in a 2D array.
    /// </summary>
    /// <param name="array">2D array containing probabilities.</param>
    /// <param name="row">Row index to normalize.</param>
    /// <param name="jMax">Number of columns.</param>
    private void NormalizeProbabilities(float[,] array, int row, int jMax)
    {
        float sum = 0f;
        for (int j = 0; j < jMax; j++)
        {
            sum += array[row, j];
        }

        if (sum == 0f)
        {
            Debug.LogWarning($"Sum of probabilities for row {row} is zero. Assigning uniform probabilities.");
            float uniformProb = 1.0f / jMax;
            for (int j = 0; j < jMax; j++)
            {
                array[row, j] = uniformProb;
            }
        }
        else
        {
            for (int j = 0; j < jMax; j++)
            {
                array[row, j] /= sum;
            }
        }

        // Verify normalization
        float normalizedSum = 0f;
        for (int j = 0; j < jMax; j++)
        {
            normalizedSum += array[row, j];
        }
        Debug.Log($"Normalized probabilities for row {row}: Sum = {normalizedSum}");
    }

    /// <summary>
    /// Runs Grover's Algorithm and adjusts HMM probabilities based on the result.
    /// </summary>
    private void RunGroversAlgorithm()
    {
        if (grover == null)
        {
            Debug.LogError("Grover's Algorithm component is not assigned.");
            return;
        }

        // Run Grover's algorithm and get the result
        Vector3 result = grover.RunGrover();

        // Extract the most probable state from the result
        int mostProbableState = Mathf.RoundToInt(result.x);

        // Clamp the mostProbableState to ensure it's within valid bounds
        mostProbableState = Mathf.Clamp(mostProbableState, 0, numStates - 1);

        Debug.Log($"Grover's Algorithm returned mostProbableState: {mostProbableState}");

        // Adjust transition and emission probabilities based on Grover's result
        AdjustProbabilitiesBasedOnGrover(mostProbableState);
    }

    /// <summary>
    /// Adjusts the transition and emission probabilities based on the most probable state from Grover's Algorithm.
    /// </summary>
    /// <param name="mostProbableState">The state determined by Grover's Algorithm.</param>
    private void AdjustProbabilitiesBasedOnGrover(int mostProbableState)
    {
        Debug.Log($"Adjusting probabilities based on Grover's result: Most probable state = {mostProbableState}");

        // Modify transition probabilities to favor the most probable state
        for (int i = 0; i < numStates; i++)
        {
            for (int j = 0; j < numStates; j++)
            {
                if (j == mostProbableState)
                {
                    transitionProbabilities[i, j] *= 2f;  // Increase the probability of transitioning to the most probable state
                }
                else
                {
                    transitionProbabilities[i, j] *= 0.5f;  // Reduce other transition probabilities
                }
            }
            // Normalize transition probabilities after adjustment
            NormalizeProbabilities(transitionProbabilities, i, jMax: numStates);
            Debug.Log($"Adjusted transition probabilities for state {i}: {string.Join(", ", GetRow(transitionProbabilities, i))}");
        }

        // Modify emission probabilities based on Grover's result
        for (int i = 0; i < numStates; i++)
        {
            if (i == mostProbableState)
            {
                for (int j = 0; j < numObservations; j++)
                {
                    emissionProbabilities[i, j] *= 1.5f;  // Increase emission probabilities for the most probable state
                }
                // Normalize emission probabilities after adjustment
                NormalizeProbabilities(emissionProbabilities, i, jMax: numObservations);
                Debug.Log($"Adjusted emission probabilities for state {i}: {string.Join(", ", GetRow(emissionProbabilities, i))}");
            }
            else
            {
                for (int j = 0; j < numObservations; j++)
                {
                    emissionProbabilities[i, j] *= 0.8f;  // Slightly reduce emission probabilities for other states
                }
                // Normalize emission probabilities after adjustment
                NormalizeProbabilities(emissionProbabilities, i, jMax: numObservations);
                Debug.Log($"Adjusted emission probabilities for state {i}: {string.Join(", ", GetRow(emissionProbabilities, i))}");
            }
        }
    }

    /// <summary>
    /// Generates a random sequence of observations.
    /// </summary>
    /// <param name="length">Length of the observation sequence.</param>
    /// <returns>An array of random observations.</returns>
    public int[] GenerateRandomObservations(int length)
    {
        if (numObservations <= 0)
        {
            Debug.LogError("Number of observations must be greater than zero.");
            return Array.Empty<int>();
        }

        int[] observations = new int[length];
        System.Random rand = new System.Random();

        for (int i = 0; i < length; i++)
        {
            observations[i] = rand.Next(numObservations); // Ensure values are within bounds [0, numObservations - 1]
            Debug.Log($"Generated observation[{i}] = {observations[i]}");
        }

        return observations;
    }

    /// <summary>
    /// Viterbi Algorithm: Decodes the most likely state sequence for a given observation sequence.
    /// </summary>
    /// <param name="observations">Array of observations.</param>
    /// <returns>The most likely state sequence.</returns>
    public int[] Viterbi(int[] observations)
    {
        Debug.Log($"Starting Viterbi Algorithm with {observations.Length} observations.");
        Debug.Log($"Number of States: {numStates}, Number of Observations: {numObservations}");

        int sequenceLength = observations.Length;

        if (sequenceLength == 0)
        {
            Debug.LogError("Viterbi received an empty observations array.");
            return Array.Empty<int>();
        }

        // Validate observations are within bounds
        for (int t = 0; t < sequenceLength; t++)
        {
            if (observations[t] < 0 || observations[t] >= numObservations)
            {
                Debug.LogError($"Observation at index {t} is out of bounds: {observations[t]} (Valid Range: 0 to {numObservations - 1})");
                throw new IndexOutOfRangeException($"Observation at index {t} is out of bounds: {observations[t]}");
            }
        }

        // Initialize tables
        float[,] dp = new float[numStates, sequenceLength];
        int[,] backpointer = new int[numStates, sequenceLength];

        // Initialization step
        for (int state = 0; state < numStates; state++)
        {
            dp[state, 0] = initialStateProbabilities[state] * emissionProbabilities[state, observations[0]];
            backpointer[state, 0] = 0;
            Debug.Log($"dp[{state}, 0] initialized to {dp[state, 0]}");
        }

        // Recursion step
        for (int t = 1; t < sequenceLength; t++)
        {
            for (int currentState = 0; currentState < numStates; currentState++)
            {
                float maxProb = float.NegativeInfinity;
                int bestPrevState = -1;

                for (int prevState = 0; prevState < numStates; prevState++)
                {
                    float prob = dp[prevState, t - 1] * transitionProbabilities[prevState, currentState] * emissionProbabilities[currentState, observations[t]];
                    if (prob > maxProb)
                    {
                        maxProb = prob;
                        bestPrevState = prevState;
                    }
                }

                if (bestPrevState == -1)
                {
                    Debug.LogError($"Failed to find a valid previous state for state {currentState} at time {t}.");
                    throw new InvalidOperationException("Invalid state transition during recursion step.");
                }

                dp[currentState, t] = maxProb;
                backpointer[currentState, t] = bestPrevState;

                Debug.Log($"dp[{currentState}, {t}] set to {dp[currentState, t]} with backpointer[{currentState}, {t}] = {bestPrevState}");
            }
        }

        // Backtracking step
        int[] mostLikelyStates = new int[sequenceLength];
        int lastState = -1;
        float maxFinalProb = float.NegativeInfinity;

        // Find the final state with the maximum probability
        for (int state = 0; state < numStates; state++)
        {
            if (dp[state, sequenceLength - 1] > maxFinalProb)
            {
                maxFinalProb = dp[state, sequenceLength - 1];
                lastState = state;
            }
        }

        if (lastState == -1)
        {
            Debug.LogError("Failed to find a valid last state during backtracking.");
            throw new InvalidOperationException("Invalid final state during backtracking.");
        }

        mostLikelyStates[sequenceLength - 1] = lastState;

        // Backtrack through the backpointer table
        for (int t = sequenceLength - 2; t >= 0; t--)
        {
            int nextState = mostLikelyStates[t + 1];
            Debug.Log($"Backtracking at time {t + 1}: nextState = {nextState}");

            if (nextState < 0 || nextState >= numStates)
            {
                Debug.LogError($"Invalid state index during backtracking at time {t + 1}: {nextState}");
                throw new IndexOutOfRangeException($"Invalid state index during backtracking at time {t + 1}: {nextState}");
            }

            mostLikelyStates[t] = backpointer[nextState, t + 1];
        }

        Debug.Log($"Most Likely States: {string.Join(", ", mostLikelyStates)}");
        return mostLikelyStates;
    }


    /// <summary>
    /// Finds the most probable state at the last time step.
    /// </summary>
    /// <param name="dp">DP table containing probabilities.</param>
    /// <param name="sequenceLength">Length of the observation sequence.</param>
    /// <returns>The state index with the highest probability.</returns>
    private int FindMostProbableLastState(float[,] dp, int sequenceLength)
    {
        int lastState = -1;
        float maxProb = -1f;

        for (int state = 0; state < numStates; state++)
        {
            if (dp[state, sequenceLength - 1] > maxProb)
            {
                maxProb = dp[state, sequenceLength - 1];
                lastState = state;
            }
        }

        Debug.Log($"Most probable last state: {lastState} with probability {maxProb}");
        return lastState;
    }

    /// <summary>
    /// Helper method to retrieve a row from a 2D float array.
    /// </summary>
    private float[] GetRow(float[,] array, int row)
    {
        int columns = array.GetLength(1);
        float[] rowData = new float[columns];
        for (int j = 0; j < columns; j++)
        {
            rowData[j] = array[row, j];
        }
        return rowData;
    }

    /// <summary>
    /// Safely accesses a 2D array element with bounds checking.
    /// </summary>
    /// <param name="array">The 2D array.</param>
    /// <param name="row">Row index.</param>
    /// <param name="col">Column index.</param>
    /// <returns>The array element if indices are valid; otherwise, throws an exception.</returns>
    private float GetArrayElement(float[,] array, int row, int col)
    {
        if (row < 0 || row >= array.GetLength(0) || col < 0 || col >= array.GetLength(1))
        {
            throw new IndexOutOfRangeException($"Attempted to access array[{row}, {col}], but dimensions are [{array.GetLength(0)}, {array.GetLength(1)}].");
        }
        return array[row, col];
    }
}
