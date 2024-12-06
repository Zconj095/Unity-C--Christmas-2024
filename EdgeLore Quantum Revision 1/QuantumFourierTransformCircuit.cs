using System;
using UnityEngine;
using System.Numerics;
using System.Collections.Generic;

public class QuantumFourierTransformCircuit : MonoBehaviour
{
    private List<QuantumGate> gates = new List<QuantumGate>();

    public QuantumState ApplyQFT(int numQubits)
    {
        Debug.Log($"QuantumFourierTransformCircuit: Applying QFT to {numQubits} qubits...");

        QuantumState state = InitializeState(numQubits);

        for (int i = 0; i < numQubits; i++)
        {
            ApplyHadamard(i, state);

            for (int j = i + 1; j < numQubits; j++)
            {
                double angle = Math.PI / Math.Pow(2, j - i);
                ApplyControlledRotation(i, j, angle, state);
            }
        }

        ApplySwapGate(numQubits, state);

        Debug.Log("QuantumFourierTransformCircuit: QFT completed.");
        return state;
    }

    private void ApplyHadamard(int qubitIndex, QuantumState state)
    {
        Debug.Log($"QuantumFourierTransformCircuit: Applying H Gate to Qubit {qubitIndex}...");
        state.ApplyGate(new QuantumGate($"H[{qubitIndex}]", 1, new int[] { qubitIndex }, hadamardOperation));
    }

    private void ApplyControlledRotation(int controlQubit, int targetQubit, double angle, QuantumState state)
    {
        Debug.Log($"QuantumFourierTransformCircuit: Applying Controlled R({angle}) Gate between Qubit {controlQubit} and Qubit {targetQubit}...");
        state.ApplyGate(new QuantumGate($"CR({angle})[{controlQubit},{targetQubit}]", 2, new int[] { controlQubit, targetQubit }, rotationOperation(angle)));
    }

    private void ApplySwapGate(int numQubits, QuantumState state)
    {
        for (int i = 0; i < numQubits / 2; i++)
        {
            Debug.Log($"QuantumFourierTransformCircuit: Swapping Qubit {i} and Qubit {numQubits - i - 1}...");
            state.ApplyGate(new QuantumGate($"SWAP[{i},{numQubits - i - 1}]", 2, new int[] { i, numQubits - i - 1 }, swapOperation));
        }
    }

    private QuantumState InitializeState(int numQubits)
    {
        int dimension = 1 << numQubits;
        QuantumState state = new QuantumState(dimension);
        Debug.Log($"QuantumFourierTransformCircuit: Initialized state with dimension {dimension}.");
        return state;
    }

    private Func<Complex[], Complex[]> hadamardOperation = state =>
    {
        int dim = state.Length;
        Complex[] newState = new Complex[dim];
        Complex norm = new Complex(1 / Math.Sqrt(2), 0);

        for (int i = 0; i < dim; i++)
        {
            int bit = i & 1;
            newState[i ^ 1] += state[i] * norm;
            newState[i] += state[i] * norm;
        }
        return newState;
    };

    private Func<Complex[], Complex[]> rotationOperation(double angle)
    {
        return state =>
        {
            int dim = state.Length;
            Complex[] newState = new Complex[dim];
            Complex rotationFactor = new Complex(Math.Cos(angle), Math.Sin(angle));

            for (int i = 0; i < dim; i++)
            {
                if ((i & 1) == 1)
                {
                    newState[i] = state[i] * rotationFactor;
                }
                else
                {
                    newState[i] = state[i];
                }
            }
            return newState;
        };
    }

    private Func<Complex[], Complex[]> swapOperation = state =>
    {
        int dim = state.Length;
        Complex[] newState = (Complex[])state.Clone();

        for (int i = 0; i < dim / 2; i++)
        {
            int swappedIndex = (i & ~1) | (~i & 1);
            newState[swappedIndex] = state[i];
        }
        return newState;
    };
}
