using UnityEngine;

public class MonaQuantumSystemOrchestrator : MonoBehaviour
{
    public QuantumVector quantumVector;
    public QuantumFieldGenerator fieldGenerator1;
    public QuantumFieldGenerator fieldGenerator2;
    public MonaQuantumPhaseShift phaseShift;
    public MonaQuantumPhaseEstimation phaseEstimation1;
    public MonaQuantumPhaseEstimation phaseEstimation2;
    public MonaQuantumHyperstate hyperstate;

    void Start()
    {
        Debug.Log("Starting Quantum System...");

        // Step 1: Generate Quantum Vector
        var quantumState = quantumVector.Generate();

        // Step 2: Generate Quantum Fields
        fieldGenerator1.InputVector = quantumState;
        var field1 = fieldGenerator1.GenerateField();

        fieldGenerator2.InputVector = quantumState;
        var field2 = fieldGenerator2.GenerateField();

        // Step 3: Apply Quantum Phase Shift
        phaseShift.QuantumState = quantumState;
        var phaseShiftedState = phaseShift.ApplyPhaseShift(Mathf.PI / 4);

        // Step 4: Phase Estimation
        phaseEstimation1.QuantumState = phaseShiftedState + field1; // Combine shifted state and field
        var phase1 = phaseEstimation1.EstimatePhase();

        phaseEstimation2.QuantumState = phaseShiftedState + field2; // Combine shifted state and field
        var phase2 = phaseEstimation2.EstimatePhase();

        // Step 5: Form Quantum Hyperstate
        hyperstate.Input1 = phase1;
        hyperstate.Input2 = phase2;
        var finalHyperstate = hyperstate.FormHyperstate();

        Debug.Log($"Final Quantum Hyperstate: {finalHyperstate}");
    }
}
