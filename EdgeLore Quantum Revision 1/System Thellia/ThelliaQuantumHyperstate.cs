using UnityEngine;
using System;
using System.Collections.Generic;
public class ThelliaQuantumHyperstate : MonoBehaviour
{
    private List<Qubit> qubits = new List<Qubit>();

    public void AddQubit(Qubit qubit)
    {
        qubits.Add(qubit);
    }

    public Qubit CalculateOutput()
    {
        float amplitude0 = 0f;
        float amplitude1 = 0f;

        // Combine all qubits into a single state
        foreach (var qubit in qubits)
        {
            amplitude0 += qubit.Amplitude0;
            amplitude1 += qubit.Amplitude1;
        }

        return new Qubit(amplitude0, amplitude1);
    }

    public override string ToString()
    {
        Qubit output = CalculateOutput();
        return $"Final State: {output}";
    }

        public Vector3[] Inputs;

    public Vector3 FormHyperstate()
    {
        Vector3 hyperstate = Vector3.zero;
        Debug.Log("Forming Quantum Hyperstate...");

        foreach (var input in Inputs)
        {
            hyperstate += input; // Aggregate all inputs
        }

        hyperstate /= Inputs.Length; // Normalize hyperstate
        Debug.Log($"Quantum Hyperstate: {hyperstate}");

        return hyperstate;
    }
}
