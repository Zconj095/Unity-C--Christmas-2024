using System.Collections.Generic;
using UnityEngine;

public class HypervectorandQubitQuantumManager : MonoBehaviour
{
    public List<float[]> Hypervectors { get; private set; } // Public property
    public QuantumOperationQuBit QuantumOperationQuBit { get; private set; }

    void Start()
    {
        InitializeHypervectors();
        InitializeQuantumOperationQuBit();
    }

    private void InitializeQuantumOperationQuBit()
    {
        QuantumOperationQuBit = new QuantumOperationQuBit();
    }

    private void InitializeHypervectors()
    {
        Hypervectors = new List<float[]>();
        for (int i = 0; i < 100; i++) // Example: Initialize 100 hypervectors
        {
            Hypervectors.Add(new float[1024]); // Example: 1024-dimensional hypervectors
        }
    }
}
