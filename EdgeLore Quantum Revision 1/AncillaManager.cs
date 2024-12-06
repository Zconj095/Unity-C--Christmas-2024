using System.Collections.Generic;
using UnityEngine;

public class AncillaManager : MonoBehaviour
{
    [Header("Registers Configuration")]
    [Tooltip("Number of primary qubits in the circuit.")]
    public int primaryQubitCount = 4;

    [Tooltip("Number of ancilla qubits initially available.")]
    public int ancillaQubitCount = 2;

    private List<string> primaryRegisters = new List<string>();
    private List<string> ancillaRegisters = new List<string>();
    private HashSet<int> allocatedAncilla = new HashSet<int>(); // Tracks allocated ancilla indices

    private void Awake()
    {
        InitializeRegisters();
    }

    /// <summary>
    /// Initializes primary and ancilla registers.
    /// </summary>
    public void InitializeRegisters()
    {
        primaryRegisters.Clear();
        ancillaRegisters.Clear();
        allocatedAncilla.Clear();

        // Create primary registers
        for (int i = 0; i < primaryQubitCount; i++)
        {
            primaryRegisters.Add($"q{i}");
        }

        // Create ancilla registers
        for (int i = 0; i < ancillaQubitCount; i++)
        {
            ancillaRegisters.Add($"a{i}");
        }

        Debug.Log("Primary Registers: " + string.Join(", ", primaryRegisters));
        Debug.Log("Ancilla Registers: " + string.Join(", ", ancillaRegisters));
    }

    /// <summary>
    /// Allocates an ancilla register for use.
    /// </summary>
    /// <returns>The name of the allocated ancilla or null if none are available.</returns>
    public string AllocateAncilla()
    {
        for (int i = 0; i < ancillaRegisters.Count; i++)
        {
            if (!allocatedAncilla.Contains(i))
            {
                allocatedAncilla.Add(i);
                Debug.Log($"Allocated Ancilla: {ancillaRegisters[i]}");
                return ancillaRegisters[i];
            }
        }

        Debug.LogWarning("No free ancilla registers available. Consider adding more.");
        return null;
    }

    /// <summary>
    /// Releases an allocated ancilla register.
    /// </summary>
    /// <param name="ancillaName">The name of the ancilla to release.</param>
    public void ReleaseAncilla(string ancillaName)
    {
        int index = ancillaRegisters.IndexOf(ancillaName);
        if (index >= 0 && allocatedAncilla.Contains(index))
        {
            allocatedAncilla.Remove(index);
            Debug.Log($"Released Ancilla: {ancillaName}");
        }
        else
        {
            Debug.LogWarning($"Attempted to release an unallocated or non-existent ancilla: {ancillaName}");
        }
    }

    /// <summary>
    /// Adds a new ancilla register dynamically.
    /// </summary>
    public void AddAncilla()
    {
        string newAncilla = $"a{ancillaRegisters.Count}";
        ancillaRegisters.Add(newAncilla);
        Debug.Log($"Added Ancilla: {newAncilla}");
    }

    /// <summary>
    /// Removes an ancilla register if it is not allocated.
    /// </summary>
    /// <param name="index">The index of the ancilla to remove.</param>
    public void RemoveAncilla(int index)
    {
        if (index < 0 || index >= ancillaRegisters.Count)
        {
            Debug.LogError("Invalid ancilla index.");
            return;
        }

        if (allocatedAncilla.Contains(index))
        {
            Debug.LogError($"Cannot remove ancilla {ancillaRegisters[index]} because it is currently allocated.");
            return;
        }

        string removed = ancillaRegisters[index];
        ancillaRegisters.RemoveAt(index);
        Debug.Log($"Removed Ancilla: {removed}");
    }

    /// <summary>
    /// Displays the current state of primary and ancilla registers.
    /// </summary>
    public void DisplayRegisterStatus()
    {
        Debug.Log($"Primary Registers: {string.Join(", ", primaryRegisters)}");
        Debug.Log($"Ancilla Registers: {string.Join(", ", ancillaRegisters)}");

        foreach (int index in allocatedAncilla)
        {
            Debug.Log($"Allocated Ancilla: {ancillaRegisters[index]}");
        }
    }

    /// <summary>
    /// Clears all ancilla registers and reinitializes them.
    /// </summary>
    public void ResetAncilla()
    {
        ancillaRegisters.Clear();
        allocatedAncilla.Clear();
        InitializeRegisters();
        Debug.Log("Ancilla registers have been reset.");
    }

    /// <summary>
    /// Integrates ancilla registers into a quantum error correction scheme.
    /// </summary>
    /// <param name="errorSyndrome">The error syndrome to correct.</param>
    public void ApplyErrorCorrection(string errorSyndrome)
    {
        Debug.Log($"Applying error correction for syndrome: {errorSyndrome}");
        // Example placeholder logic for error correction
        if (AllocateAncilla() != null)
        {
            Debug.Log("Ancilla allocated for error correction.");
        }
        else
        {
            Debug.LogWarning("Failed to allocate ancilla for error correction.");
        }
    }

    /// <summary>
    /// Expands the primary register dynamically.
    /// </summary>
    public void AddPrimaryRegister()
    {
        string newPrimary = $"q{primaryRegisters.Count}";
        primaryRegisters.Add(newPrimary);
        Debug.Log($"Added Primary Register: {newPrimary}");
    }

    /// <summary>
    /// Simulates a quantum operation involving primary and ancilla registers.
    /// </summary>
    public void SimulateQuantumOperation()
    {
        if (primaryRegisters.Count == 0 || ancillaRegisters.Count == 0)
        {
            Debug.LogError("Insufficient registers for quantum operation.");
            return;
        }

        string primary = primaryRegisters[Random.Range(0, primaryRegisters.Count)];
        string ancilla = AllocateAncilla();

        if (ancilla != null)
        {
            Debug.Log($"Simulating operation: Controlled-NOT between {primary} (control) and {ancilla} (target).");
            ReleaseAncilla(ancilla);
        }
    }
}
