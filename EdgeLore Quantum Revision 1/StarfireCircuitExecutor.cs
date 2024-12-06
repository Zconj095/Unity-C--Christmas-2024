using UnityEngine;

public class StarfireCircuitExecutor : MonoBehaviour
{
    [SerializeField] private int numQubits = 5;
    [SerializeField] private int markedState = 2;
    [SerializeField] private float boltzmannFactor = 0.5f;
    [SerializeField] private string hardwareTarget = "IBM-Q";

    private BlochSphereVisualizer blochSphereVisualizer;

    void Start()
    {
        blochSphereVisualizer = gameObject.GetComponent<BlochSphereVisualizer>();
        if (blochSphereVisualizer == null)
        {
            Debug.Log("BlochSphereVisualizer not found. Adding component...");
            blochSphereVisualizer = gameObject.AddComponent<BlochSphereVisualizer>();
        }
        blochSphereVisualizer.numQubits = numQubits;

        ExecuteCircuitsWithVisualization();
    }

    public void ExecuteAdvancedCircuits()
    {
        var boltzmannGrover = gameObject.AddComponent<QuantumFuzedBoltzmannGroverCircuit>();
        boltzmannGrover.ExecuteFuzedBoltzmannGrover(numQubits, markedState, boltzmannFactor);

        var fusionState = gameObject.AddComponent<QuantumFusionStateCircuit>();
        fusionState.CreateFusionState(numQubits);

        var barebonesKernel = gameObject.AddComponent<QuantumBarebonesKernelCircuit>();
        barebonesKernel.ExecuteKernelCircuit(numQubits);

        var hypervector = gameObject.AddComponent<QuantumHypervectorCircuit>();
        hypervector.CreateHypervectorCircuit(numQubits * 2);

        var transpiler = gameObject.AddComponent<QuantumQubitTranspilerCircuit>();
        transpiler.TranspileCircuit(hardwareTarget, numQubits);
    }

    public void ExecuteCircuitsWithVisualization()
    {
        Vector3[] initialStates = InitializeStates(numQubits);

        blochSphereVisualizer.VisualizeState0(initialStates);

        Debug.Log("Executing Fuzed Boltzmann-Grover Circuit...");
        Vector3[] boltzmannGroverStates = ApplyCircuitTransform(initialStates, "Boltzmann-Grover");
        blochSphereVisualizer.VisualizeState0(boltzmannGroverStates);

        Debug.Log("Executing Fusion State Circuit...");
        Vector3[] fusionStates = ApplyCircuitTransform(boltzmannGroverStates, "Fusion");
        blochSphereVisualizer.VisualizeState0(fusionStates);

        Debug.Log("Executing Chaos State Circuit...");
        Vector3[] chaosStates = ApplyCircuitTransform(fusionStates, "Chaos");
        blochSphereVisualizer.VisualizeState0(chaosStates);

        Debug.Log("All circuits executed with visualization.");
    }

    private Vector3[] ApplyCircuitTransform(Vector3[] states, string circuitType)
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i] = new Vector3(states[i].x * 0.9f, states[i].y * 0.9f, states[i].z * 0.9f);
        }
        return states;
    }

    private Vector3[] InitializeStates(int qubits)
    {
        Vector3[] states = new Vector3[qubits];
        for (int i = 0; i < qubits; i++)
        {
            states[i] = new Vector3(0, 0, 1);
        }
        return states;
    }

    public void ExecuteCircuitsWithQFT()
    {
        Debug.Log("Executing circuits with QFT...");

        ExecuteAdvancedCircuits();

        var qftCircuit = gameObject.AddComponent<QuantumFourierTransformCircuit>();
        qftCircuit.ApplyQFT(numQubits);
    }
}
