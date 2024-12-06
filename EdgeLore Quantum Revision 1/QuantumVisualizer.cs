using UnityEngine;

public class QuantumVisualizer : MonoBehaviour
{
    [Header("Prefabs")]
    [Tooltip("Prefab representing a single qubit.")]
    public GameObject QubitPrefab;

    [Tooltip("Prefab representing a quantum gate.")]
    public GameObject GatePrefab;

    [Header("Visualization Settings")]
    [Tooltip("Distance between qubits in the visualization.")]
    public float qubitSpacing = 2.0f;

    [Tooltip("Default color for qubits.")]
    public Color defaultQubitColor = Color.blue;

    [Tooltip("Color to indicate noise effects on a qubit.")]
    public Color noiseColor = Color.red;

    private GameObject[] qubits;

    /// <summary>
    /// Initializes the visualization of qubits.
    /// </summary>
    public void Initialize(int numQubits)
    {
        Debug.Log("QuantumVisualizer: Initializing visualization...");
        InitializeQubits(numQubits);
        ResetVisualization();
    }

    public void InitializeQubits(int numQubits)
    {
        qubits = new GameObject[numQubits];
        for (int i = 0; i < numQubits; i++)
        {
            Vector3 position = new Vector3(i * qubitSpacing, 0, 0);
            qubits[i] = Instantiate(QubitPrefab, position, Quaternion.identity, transform);
            qubits[i].name = $"Qubit {i}";

            // Set default appearance
            Renderer renderer = qubits[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = defaultQubitColor;
            }
        }

        Debug.Log($"QuantumVisualizer: Initialized {numQubits} qubits.");
    }

    public void VisualizePulse(double amplitude, double frequency, double duration)
    {
        Debug.Log($"Visualizing Pulse: Amplitude={amplitude}, Frequency={frequency}, Duration={duration}");

        foreach (GameObject qubit in qubits)
        {
            StartCoroutine(AnimatePulseEffect(qubit, amplitude, frequency, duration));
        }
    }

    private System.Collections.IEnumerator AnimatePulseEffect(GameObject qubit, double amplitude, double frequency, double duration)
    {
        float time = 0f;
        Vector3 originalScale = qubit.transform.localScale;

        while (time < duration)
        {
            float scaleFactor = (float)(1.0 + amplitude * Mathf.Sin(2 * Mathf.PI * (float)frequency * time));
            qubit.transform.localScale = originalScale * scaleFactor;
            time += Time.deltaTime;

            yield return null;
        }

        qubit.transform.localScale = originalScale; // Reset to original scale
    }

    public void VisualizeGate(QuantumGate gate)
    {
        if (gate.Qubits.Length == 0)
        {
            Debug.LogWarning("QuantumVisualizer: Gate has no associated qubits to visualize.");
            return;
        }

        Vector3 position = Vector3.zero;
        foreach (int qubitIndex in gate.Qubits)
        {
            if (qubitIndex < 0 || qubitIndex >= qubits.Length)
            {
                Debug.LogError($"QuantumVisualizer: Qubit index {qubitIndex} out of range for gate visualization.");
                return;
            }
            position += qubits[qubitIndex].transform.position;
        }
        position /= gate.Qubits.Length;

        GameObject gateObject = Instantiate(GatePrefab, position, Quaternion.identity, transform);
        gateObject.name = $"{gate.Name} Gate";
        Debug.Log($"Visualized Gate: {gate.Name} at position {position}");
    }

    public void ShowNoiseEffect(int qubitIndex, float intensity = 1.0f)
    {
        if (qubitIndex < 0 || qubitIndex >= qubits.Length)
        {
            Debug.LogError($"QuantumVisualizer: Invalid qubit index {qubitIndex} for noise effect.");
            return;
        }

        Renderer renderer = qubits[qubitIndex].GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.Lerp(defaultQubitColor, noiseColor, intensity);
            Debug.Log($"Noise applied to Qubit {qubitIndex} with intensity {intensity}");
        }

        StartCoroutine(ResetNoiseEffect(qubitIndex, 1.0f));
    }

    private System.Collections.IEnumerator ResetNoiseEffect(int qubitIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        Renderer renderer = qubits[qubitIndex].GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = defaultQubitColor;
        }
    }

    public void VisualizeStateEvolution(QuantumState state)
    {
        Debug.Log($"QuantumVisualizer: Visualizing state evolution: {state}");

        for (int i = 0; i < state.Amplitudes.Length; i++)
        {
            Vector3 position = qubits[i].transform.position;
            float height = (float)state.Amplitudes[i]; // Directly use the amplitude value
            qubits[i].transform.localScale = new Vector3(1, height, 1);
        }
    }

    public void ResetVisualization()
    {
        foreach (GameObject qubit in qubits)
        {
            if (qubit != null)
            {
                Renderer renderer = qubit.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = defaultQubitColor;
                }

                qubit.transform.localScale = Vector3.one;
            }
        }

        Debug.Log("QuantumVisualizer: All visualizations reset.");
    }
}
