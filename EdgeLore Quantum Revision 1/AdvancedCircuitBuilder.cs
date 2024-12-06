using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedCircuitBuilder : MonoBehaviour
{
    public GameObject QubitPrefab;
    public GameObject GatePrefab;
    public Transform QubitContainer;
    public Transform GateContainer;
    public Dropdown GateDropdown;
    public Button AddQubitButton;
    public Button AddGateButton;
    public Button UndoButton;
    public Button RedoButton;
    public Button SaveCircuitButton;
    public Button LoadCircuitButton;
    public Text CircuitResultText;

    private List<GameObject> qubits = new List<GameObject>();
    private List<QuantumGate> circuitGates = new List<QuantumGate>();
    private Stack<List<QuantumGate>> undoStack = new Stack<List<QuantumGate>>();
    private Stack<List<QuantumGate>> redoStack = new Stack<List<QuantumGate>>();
    private QuantumCircuit currentCircuit; // Represents the quantum circuit being built
    private QuantumVisualizer visualizer; // Visualizer for showing gate operations
    void Start()
    {
        AddQubitButton.onClick.AddListener(AddQubit);
        AddGateButton.onClick.AddListener(AddGate);
        UndoButton.onClick.AddListener(Undo);
        RedoButton.onClick.AddListener(Redo);
        SaveCircuitButton.onClick.AddListener(SaveCircuit);
        LoadCircuitButton.onClick.AddListener(LoadCircuit);
    }

    void AddQubit()
    {
        GameObject qubit = Instantiate(QubitPrefab, new Vector3(qubits.Count * 2.0f, 0, 0), Quaternion.identity, QubitContainer);
        qubit.name = $"Qubit {qubits.Count}";
        qubits.Add(qubit);
        currentCircuit.AddQubit(); // Add a qubit to the circuit
        Debug.Log($"Added Qubit {qubits.Count - 1}");
    }


    void AddGate()
    {
        if (qubits.Count == 0)
        {
            Debug.LogWarning("No qubits available to add gates.");
            return;
        }

        string selectedGate = GateDropdown.options[GateDropdown.value].text;
        QuantumGate gate = null;

        // Create gate based on selection
        switch (selectedGate)
        {
            case "Hadamard":
                gate = QuantumGate.Hadamard(0); // Default to Qubit 0
                break;
            case "CNOT":
                if (qubits.Count > 1)
                    gate = QuantumGate.CNOT(0, 1); // Default to first two qubits
                break;
            case "RX":
                gate = QuantumGate.RX(0, 90); // Example gate with angle
                break;
        }

        if (gate != null)
        {
            currentCircuit.AddGate(gate); // Add gate to circuit
            visualizer?.VisualizeGate(gate); // Update visualizer (optional)
            Debug.Log($"Added {selectedGate} gate to the circuit.");
        }
    }


    void Undo()
    {
        if (undoStack.Count > 0)
        {
            redoStack.Push(new List<QuantumGate>(circuitGates));
            circuitGates = undoStack.Pop();
            Debug.Log("Undo action performed.");
        }
    }

    void Redo()
    {
        if (redoStack.Count > 0)
        {
            undoStack.Push(new List<QuantumGate>(circuitGates));
            circuitGates = redoStack.Pop();
            Debug.Log("Redo action performed.");
        }
    }

    private void SaveStateToUndoStack()
    {
        undoStack.Push(new List<QuantumGate>(circuitGates));
        redoStack.Clear(); // Clear redo stack whenever a new action is performed
    }

    void SaveCircuit()
    {
        string circuitData = SerializeCircuit();
        PlayerPrefs.SetString("SavedCircuit", circuitData);
        Debug.Log("Circuit saved successfully.");
    }

    void LoadCircuit()
    {
        string circuitData = PlayerPrefs.GetString("SavedCircuit", null);
        if (!string.IsNullOrEmpty(circuitData))
        {
            DeserializeCircuit(circuitData);
            Debug.Log("Circuit loaded successfully.");
        }
        else
        {
            Debug.LogWarning("No saved circuit found.");
        }
    }

    string SerializeCircuit()
    {
        // Serialize the circuit to a string (e.g., JSON or custom format)
        return JsonUtility.ToJson(new CircuitData(qubits, circuitGates));
    }

    void DeserializeCircuit(string circuitData)
    {
        // Deserialize and reconstruct the circuit
        CircuitData data = JsonUtility.FromJson<CircuitData>(circuitData);
        qubits.Clear();
        circuitGates.Clear();

        foreach (var qubitInfo in data.qubitInfos)
        {
            AddQubit(); // Add qubits back
        }
        circuitGates.AddRange(data.gates);
    }

    public void ExecuteCircuit()
    {
        // Simulate the circuit and show results (this will require a quantum simulator backend)
    QuantumSimulator simulator = new QuantumSimulator(currentCircuit, visualizer);
    QuantumState finalState = simulator.Simulate();
    CircuitResultText.text = $"Final State: {finalState}";

    }

    void SimulateCircuit()
    {
        if (currentCircuit == null || currentCircuit.Gates.Count == 0)
        {
            Debug.LogWarning("No circuit to simulate.");
            return;
        }

        QuantumSimulator simulator = new QuantumSimulator(currentCircuit, visualizer);
        QuantumState finalState = simulator.Simulate();

        // Display final state or result
        CircuitResultText.text = $"Final State: {finalState}";
        Debug.Log($"Circuit simulation result: {finalState}");
    }

}

[System.Serializable]
public class CircuitData
{
    public List<QubitInfo> qubitInfos;
    public List<QuantumGate> gates;

    public CircuitData(List<GameObject> qubits, List<QuantumGate> circuitGates)
    {
        qubitInfos = new List<QubitInfo>();
        foreach (var qubit in qubits)
        {
            qubitInfos.Add(new QubitInfo(qubit.name));
        }
        gates = circuitGates;
    }
}

[System.Serializable]
public class QubitInfo
{
    public string name;

    public QubitInfo(string name)
    {
        this.name = name;
    }
}
