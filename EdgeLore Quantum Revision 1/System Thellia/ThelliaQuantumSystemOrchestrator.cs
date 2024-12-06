using UnityEngine;

public class ThelliaQuantumSystemOrchestrator : MonoBehaviour
{
    public ToffoliGate leftToffoli;
    public ToffoliGate rightToffoli;
    public ThelliaSuperpositionManager leftSuperposition;
    public ThelliaSuperpositionManager rightSuperposition;
    public ThelliaQuantumTeleportation teleportation;
    public ThelliaQuantumHyperstate hyperstate;

    void Start()
    {
        Debug.Log("Starting Quantum System...");

        // Step 1: Process left Toffoli gate and feed into superposition
        var leftToffoliOutput = leftToffoli.Operate();
        var leftSuperpositionOutput = leftSuperposition.CollapseToSuperposition();

        // Step 2: Process right Toffoli gate and feed into superposition
        var rightToffoliOutput = rightToffoli.Operate();
        var rightSuperpositionOutput = rightSuperposition.CollapseToSuperposition();

        // Step 3: Teleport state to hyperstate
        var teleportationOutput = teleportation.TeleportToHyperstate();

        // Step 4: Aggregate inputs into hyperstate
        hyperstate.Inputs = new[] { leftSuperpositionOutput, rightSuperpositionOutput, teleportationOutput };
        var finalHyperstate = hyperstate.FormHyperstate();

        Debug.Log($"Final Quantum Hyperstate: {finalHyperstate}");
    }
}
