using UnityEngine;

public class CircuitExecutor : MonoBehaviour
{
    [SerializeField] private int numQubits = 4;

    private void Start()
    {
        ExecuteAllCircuits();
    }

    public void ExecuteAllCircuits()
    {
        // Hadamard Circuit
        var hadamard = gameObject.AddComponent<HadamardCircuit>();
        hadamard.ApplyHadamard(numQubits);

        // Shor's Algorithm
        var shor = gameObject.AddComponent<ShorCircuit>();
        shor.ExecuteShor(15); // Example: Factorize 15

        // Grover's Algorithm
        var grover = gameObject.AddComponent<GroverCircuit>();
        grover.ExecuteGrover(numQubits, 2); // Example: Find element 2

        // Radon Transform
        var radon = gameObject.AddComponent<RadonCircuit>();
        radon.ApplyRadonTransform(new float[,] { { 1, 0 }, { 0, 1 } }, Mathf.PI / 4);

        // Pauli-Lindblad Noise
        var pauliLindblad = gameObject.AddComponent<PauliLindbladCircuit>();
        pauliLindblad.ApplyPauliLindblad(new Vector3(1, 0, 0));
    }
}
