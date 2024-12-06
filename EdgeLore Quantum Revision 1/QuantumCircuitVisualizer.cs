using UnityEngine;

public class QuantumCircuitVisualizer : MonoBehaviour
{
    [SerializeField] private int numQubits = 3;
    [SerializeField] private float gateSpacing = 1.5f; // Spacing between gates
    private GameObject[] qubitSpheres; // Visual representation of qubits

    void Start()
    {
        InitializeCircuitVisualization();
    }

    public void InitializeCircuitVisualization()
    {
        qubitSpheres = new GameObject[numQubits];

        for (int i = 0; i < numQubits; i++)
        {
            // Create qubit sphere
            qubitSpheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            qubitSpheres[i].name = $"Qubit_{i}";
            qubitSpheres[i].transform.position = new Vector3(0, i * gateSpacing, 0);
            qubitSpheres[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            qubitSpheres[i].GetComponent<Renderer>().material.color = Color.blue;
        }

        Debug.Log("Initialized quantum circuit visualization.");
    }

    public void AddGate(string gateType, int targetQubit, int controlQubit = -1)
    {
        Vector3 gatePosition = new Vector3(gateSpacing, targetQubit * gateSpacing, 0);

        if (controlQubit >= 0)
        {
            // Visualize a controlled gate with a connecting line
            GameObject controlSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            controlSphere.transform.position = new Vector3(gateSpacing, controlQubit * gateSpacing, 0);
            controlSphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            controlSphere.GetComponent<Renderer>().material.color = Color.red;

            DrawLine(controlSphere.transform.position, gatePosition, Color.yellow);
        }

        // Visualize the target gate
        GameObject gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gate.name = $"{gateType}_Gate";
        gate.transform.position = gatePosition;
        gate.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        gate.GetComponent<Renderer>().material.color = Color.green;

        Debug.Log($"Added {gateType} gate to Qubit {targetQubit}.");
    }

    private void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject lineObject = new GameObject("GateLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
