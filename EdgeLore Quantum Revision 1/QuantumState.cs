using System;
using UnityEngine;
using System.Numerics;

public class QuantumState : MonoBehaviour
{
    public Complex[] StateVector { get; private set; }

    /// <summary>
    /// Initializes a quantum state with a given number of qubits.
    /// </summary>
    /// <param name="numQubits">The number of qubits in the quantum state.</param>
    public QuantumState(int numQubits)
    {
        int stateSize = (int)Math.Pow(2, numQubits);
        StateVector = new Complex[stateSize];

        // Initialize the state to |0⟩
        StateVector[0] = new Complex(1, 0);
    }

    /// <summary>
    /// Gets the amplitudes (magnitudes) of the state vector.
    /// </summary>
    public double[] Amplitudes
    {
        get
        {
            double[] amplitudes = new double[StateVector.Length];
            for (int i = 0; i < StateVector.Length; i++)
            {
                amplitudes[i] = StateVector[i].Magnitude;
            }
            return amplitudes;
        }
    }

    /// <summary>
    /// Gets the probabilities of each basis state.
    /// </summary>
    public double[] Probabilities
    {
        get
        {
            double[] probabilities = new double[StateVector.Length];
            for (int i = 0; i < StateVector.Length; i++)
            {
                probabilities[i] = Math.Pow(StateVector[i].Magnitude, 2);
            }
            return probabilities;
        }
    }

    /// <summary>
    /// Normalizes the state vector to ensure it represents a valid quantum state.
    /// </summary>
    public void Normalize()
    {
        double norm = 0;
        foreach (var amplitude in StateVector)
        {
            norm += Math.Pow(amplitude.Magnitude, 2);
        }

        norm = Math.Sqrt(norm);

        if (norm == 0)
        {
            Debug.LogWarning("QuantumState: State vector is zero; cannot normalize.");
            return;
        }

        for (int i = 0; i < StateVector.Length; i++)
        {
            StateVector[i] /= new Complex(norm, 0);
        }
    }

    /// <summary>
    /// Applies a quantum gate to the state vector.
    /// </summary>
    /// <param name="gate">The quantum gate to apply.</param>
    public void ApplyGate(QuantumGate gate)
    {
        if (gate.Operation != null)
        {
            StateVector = gate.Operation(StateVector);
            Normalize(); // Ensure normalization after applying the gate
        }
        else
        {
            Debug.LogWarning($"Operation for {gate.Name} gate is not defined.");
        }
    }

    /// <summary>
    /// Measures the quantum state, collapsing it to a specific basis state.
    /// </summary>
    /// <returns>The index of the measured basis state.</returns>
    public int Measure()
    {
        double[] probabilities = Probabilities;
        double randomValue = UnityEngine.Random.Range(0f, 1f);

        double cumulativeProbability = 0;
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                CollapseToBasisState(i);
                return i;
            }
        }

        Debug.LogWarning("QuantumState: Measurement failed due to numerical issues.");
        return -1;
    }

    /// <summary>
    /// Collapses the state to a specific basis state.
    /// </summary>
    /// <param name="index">The index of the basis state to collapse to.</param>
    private void CollapseToBasisState(int index)
    {
        for (int i = 0; i < StateVector.Length; i++)
        {
            StateVector[i] = (i == index) ? new Complex(1, 0) : new Complex(0, 0);
        }

        Debug.Log($"QuantumState: Collapsed to basis state |{index}⟩.");
    }

    /// <summary>
    /// Returns a string representation of the quantum state.
    /// </summary>
    public override string ToString()
    {
        string result = "Quantum State:\n";
        for (int i = 0; i < StateVector.Length; i++)
        {
            result += $"|{Convert.ToString(i, 2).PadLeft((int)(Math.Log(StateVector.Length) / Math.Log(2)), '0')}⟩: {StateVector[i]}\n";
        }
        return result;
    }
}
