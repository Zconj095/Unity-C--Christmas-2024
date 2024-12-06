using System;
using UnityEngine;
public class ShorsAlgorithm : MonoBehaviour
{
    [Header("Shor's Algorithm Settings")]
    public int numberToFactor = 15;  // Example number to factor
    public int maxAttempts = 10;

    // Reference to the QuantumCircuit and QuantumSimulator (assign them in the inspector or via script)
    public QuantumCircuit quantumCircuit;
    public QuantumSimulator quantumSimulator;

    void Start()
    {
        // Trigger the algorithm execution in Start or on a specific event
        if (quantumCircuit != null && quantumSimulator != null)
        {
            Execute(quantumCircuit, quantumSimulator); // Pass the quantumCircuit and quantumSimulator to Execute
        }
        else
        {
            Debug.LogError("QuantumCircuit or QuantumSimulator is not assigned.");
        }
    }

    // Update Execute to accept QuantumCircuit and QuantumSimulator
    public void Execute(QuantumCircuit circuit, QuantumSimulator simulator)
    {
        Debug.Log($"Executing Shor's Algorithm to factor {numberToFactor}...");

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            int guess = UnityEngine.Random.Range(2, numberToFactor); // Random guess
            int gcd = GreatestCommonDivisor(guess, numberToFactor);

            if (gcd > 1)
            {
                Debug.Log($"Found factor: {gcd}");
                return;
            }

            // Quantum steps
            int period = FindPeriod(guess);
            if (period > 0 && period % 2 == 0)
            {
                int factor1 = (int)Math.Pow(guess, period / 2) - 1;
                int factor2 = (int)Math.Pow(guess, period / 2) + 1;

                gcd = GreatestCommonDivisor(factor1, numberToFactor);
                if (gcd > 1)
                {
                    Debug.Log($"Found factors: {gcd} and {numberToFactor / gcd}");
                    return;
                }
            }
        }

        Debug.Log("Failed to find factors.");
    }

    private int FindPeriod(int guess)
    {
        // Placeholder: simulate finding the period via QFT
        Debug.Log($"Simulating period finding for guess {guess}...");
        return UnityEngine.Random.Range(2, 10); // Random period for demonstration
    }

    private int GreatestCommonDivisor(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
