using UnityEngine;

public class SystemTernoilli : MonoBehaviour
{
    [Header("Algorithm Components")]
    public GroversAlgorithm groversAlgorithm;
    public ShorsAlgorithm shorsAlgorithm;
    public DeutschAlgorithm deutschAlgorithm;

    [Header("Quantum Flux")]
    public QuantumFluxManager quantumFluxManager;

    [Header("Visualization and Translucency")]
    public QuantumFieldTranslucency quantumFieldTranslucency;

    [Header("Superposition and Entanglement")]
    public SuperpositionManager superpositionManager;
    public EntanglementManager entanglementManager;

    void Start()
    {
        Debug.Log("Starting Quantum System: SystemTernoilli...");

        try
        {
            // Initialize States if they are not set
            if (superpositionManager.States == null || superpositionManager.States.Length == 0)
            {
                superpositionManager.InitializeStates(5); // Example: Initialize with 5 states
            }

            RunAlgorithms();
            ManageQuantumData();
            HandleQuantumStates();
            CompleteSystemExecution();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error in SystemTernoilli: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private void RunAlgorithms()
    {
        Debug.Log("Running Algorithms...");

        if (groversAlgorithm == null)
        {
            Debug.LogError("GroversAlgorithm is not assigned.");
        }
        else
        {
            groversAlgorithm.RunGrover();
            Debug.Log("Grover's Algorithm executed successfully.");
        }

        if (shorsAlgorithm == null)
        {
            Debug.LogError("ShorsAlgorithm is not assigned.");
        }
        else
        {
            var quantumCircuit = new QuantumCircuit(5);
            var quantumVisualizer = new QuantumVisualizer();
            var quantumSimulator = new QuantumSimulator(quantumCircuit, quantumVisualizer);
            shorsAlgorithm.Execute(quantumCircuit, quantumSimulator);
            Debug.Log("Shor's Algorithm executed successfully.");
        }

        if (deutschAlgorithm == null)
        {
            Debug.LogError("DeutschAlgorithm is not assigned.");
        }
        else
        {
            deutschAlgorithm.Execute();
            Debug.Log("Deutsch Algorithm executed successfully.");
        }
    }

    private void ManageQuantumData()
    {
        Debug.Log("Managing Quantum Data...");

        if (quantumFluxManager == null)
        {
            Debug.LogError("QuantumFluxManager is not assigned.");
            return;
        }

        float fluxOutput = quantumFluxManager.ComputeFlux();
        Debug.Log($"Quantum Flux calculated: {fluxOutput}");

        if (quantumFieldTranslucency == null)
        {
            Debug.LogError("QuantumFieldTranslucency is not assigned.");
            return;
        }

        // Calculate translucency with expected size
        quantumFieldTranslucency.CalculateTranslucency(fluxOutput, 10);
        Debug.Log("Quantum Field Translucency calculated successfully.");
    }

    private void HandleQuantumStates()
    {
        Debug.Log("Handling Quantum States...");

        if (superpositionManager == null)
        {
            Debug.LogError("SuperpositionManager is not assigned.");
            return;
        }

        var superposedState = superpositionManager.CollapseToState();
        Debug.Log($"Superposition handled successfully. Superposed State: {superposedState}");

        if (entanglementManager == null)
        {
            Debug.LogError("EntanglementManager is not assigned.");
            return;
        }

        // Recompute fluxOutput to ensure it's accessible here
        if (quantumFluxManager == null)
        {
            Debug.LogError("QuantumFluxManager is not assigned.");
            return;
        }

        float fluxOutput = quantumFluxManager.ComputeFlux();
        entanglementManager.Entangle(superposedState, fluxOutput);
        Debug.Log("Entanglement handled successfully.");
    }

    private void CompleteSystemExecution()
    {
        Debug.Log("SystemTernoilli execution completed successfully.");
    }
}
