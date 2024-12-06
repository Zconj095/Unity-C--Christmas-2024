using UnityEngine;

public class BlochSphereVisualizer : MonoBehaviour
{
    public GameObject SpherePrefab;

    [SerializeField] public int numQubits = 1;

    public void VisualizeState0(Vector3[] qubitStates)
    {
        if (qubitStates.Length != numQubits)
        {
            Debug.LogError("Mismatch between number of qubits and provided states.");
            return;
        }

        for (int i = 0; i < numQubits; i++)
        {
            // Create a sphere or pointer to represent the qubit state
            GameObject qubitRepresentation = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            qubitRepresentation.name = $"Qubit_{i}_State";
            qubitRepresentation.transform.position = qubitStates[i]; // Position on the Bloch sphere

            // Customize appearance (e.g., size and color)
            qubitRepresentation.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Small sphere
            qubitRepresentation.GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, (qubitStates[i].z + 1) / 2);

            Debug.Log($"Qubit {i} visualized at position: {qubitStates[i]}");
        }
    }

    public void VisualizeState1(QuantumState state)
    {
        // Create a Bloch sphere for each qubit
        for (int i = 0; i < state.StateVector.Length; i++)
        {
            Vector3 position = new Vector3(i * 2.0f, 0, 0); // Spread qubits along X-axis
            GameObject sphere = Instantiate(SpherePrefab, position, Quaternion.identity);
            sphere.name = $"Qubit {i} Bloch Sphere";

            // Example visualization logic (state vector to color mapping)
            Renderer renderer = sphere.GetComponent<Renderer>();
            renderer.material.color = new Color((float)state.StateVector[i].Real, 0, (float)state.StateVector[i].Imaginary);
        }
    }
}
