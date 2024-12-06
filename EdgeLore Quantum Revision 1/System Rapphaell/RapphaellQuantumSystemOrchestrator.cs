using UnityEngine;

public class RapphaellQuantumSystemOrchestrator : MonoBehaviour
{
    public QuantumFourierTransform qft;
    public QuantumPhaseEstimation phaseEstimation;
    public GroversAlgorithm groversAlgorithm;
    public QuantumPhaseShift phaseShift;
    public ShorsAlgorithm shorsAlgorithm;
    public RadonTransform radonTransform;
    public HadamardGate hadamardGate;

    [Header("Quantum Circuit and Simulator")]
    public QuantumCircuit circuit;
    public QuantumSimulator simulator;

    void Start()
    {
        Debug.Log("Starting Quantum System: RapphaellQuantumSystemOrchestrator...");

        try
        {
            // Step 1: Apply Quantum Fourier Transform
            Debug.Log("Applying Quantum Fourier Transform...");
            if (qft == null) throw new System.NullReferenceException("QuantumFourierTransform is not assigned.");
            var qftState = qft.ApplyQFT();
            Debug.Log($"QFT State: {qftState}");

            // Step 2: Process Left Path
            Debug.Log("Estimating Phase...");
            if (phaseEstimation == null) throw new System.NullReferenceException("QuantumPhaseEstimation is not assigned.");
            var phaseEstimatedState = phaseEstimation.EstimatePhase();
            Debug.Log($"Phase Estimated State: {phaseEstimatedState}");

            Debug.Log("Running Grover's Algorithm...");
            if (groversAlgorithm == null) throw new System.NullReferenceException("GroversAlgorithm is not assigned.");
            var groverState = groversAlgorithm.RunGrover();
            Debug.Log($"Grover State: {groverState}");

            radonTransform.LeftState = (phaseEstimatedState + groverState) / 2.0f;

            // Step 3: Process Right Path
            Debug.Log("Running Shor's Algorithm...");
            if (shorsAlgorithm == null) throw new System.NullReferenceException("ShorsAlgorithm is not assigned.");
            if (circuit == null || simulator == null) throw new System.NullReferenceException("QuantumCircuit or QuantumSimulator is not assigned.");
            shorsAlgorithm.Execute(circuit, simulator);

            Debug.Log("Applying QFT to Right Path...");
            var updatedQftState = qft.ApplyQFT();
            radonTransform.RightState = (phaseEstimatedState + updatedQftState) / 2.0f;

            // Step 4: Compute Radon Transform
            Debug.Log("Computing Radon Transform...");
            if (radonTransform == null) throw new System.NullReferenceException("RadonTransform is not assigned.");
            var radonState = radonTransform.ComputeRadonTransform();
            Debug.Log($"Radon Transform State: {radonState}");

            // Step 5: Apply Phase Shift
            Debug.Log("Applying Phase Shift...");
            if (phaseShift == null) throw new System.NullReferenceException("QuantumPhaseShift is not assigned.");
            var phaseShiftedState = phaseShift.ApplyPhaseShift(Mathf.PI / 4);
            Debug.Log($"Phase Shifted State: {phaseShiftedState}");

            // Step 6: Combine with Hadamard Gate
            Debug.Log("Combining with Hadamard Gate...");
            if (hadamardGate == null) throw new System.NullReferenceException("HadamardGate is not assigned.");
            hadamardGate.InputState = (radonState + phaseShiftedState) / 2.0f;
            var (photonOutput, electronOutput) = hadamardGate.ApplyHadamard();
            Debug.Log($"Hadamard Gate Outputs - Photon: {photonOutput}, Electron: {electronOutput}");

            // Step 7: Handle Feedback Loops
            Debug.Log("Processing Feedback Loops...");
            qft.QuantumState = photonOutput; // Feedback loop 1
            qft.ApplyQFT();

            qft.QuantumState = electronOutput; // Feedback loop 2
            qft.ApplyQFT();

            Debug.Log("RapphaellQuantumSystemOrchestrator execution completed successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error in RapphaellQuantumSystemOrchestrator: {ex.Message}");
        }
    }
}
