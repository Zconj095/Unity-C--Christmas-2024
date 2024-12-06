using UnityEngine;

public class BlackCometCircuitExecutor : MonoBehaviour
{
    [SerializeField] private int numQubits = 4;
    [SerializeField] private Vector3 imposedField = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Vector3 initialPosition = new Vector3(0, 0, 0);

    void Start()
    {
        ExecuteNewCircuits();
    }

    public void ExecuteNewCircuits()
    {
        // Entangled State Circuit
        var entangledStateCircuit = gameObject.AddComponent<EntangledStateCircuit>();
        entangledStateCircuit.CreateEntangledState(numQubits);

        // Superpositioned Imposed Field Circuit
        var superpositionedFieldCircuit = gameObject.AddComponent<SuperpositionedImposedFieldCircuit>();
        superpositionedFieldCircuit.ApplyFieldToSuperposition(numQubits, imposedField);

        // Entangled Chaos Circuit
        var entangledChaosCircuit = gameObject.AddComponent<EntangledChaosCircuit>();
        entangledChaosCircuit.CreateEntangledChaos(numQubits);

        // Chaos State Circuit
        var chaosStateCircuit = gameObject.AddComponent<ChaosStateCircuit>();
        chaosStateCircuit.CreateChaosState(numQubits);

        // Particle State Circuit
        var particleStateCircuit = gameObject.AddComponent<ParticleStateCircuit>();
        particleStateCircuit.CreateParticleState(numQubits, initialPosition);
    }
}
