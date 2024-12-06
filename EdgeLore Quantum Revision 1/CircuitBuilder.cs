using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CircuitBuilder : MonoBehaviour
{
    public GameObject QubitPrefab;
    public Dropdown GateDropdown;
    public Button AddQubitButton;
    public Button AddGateButton;
    public Button SimulateButton;
    public Transform QubitContainer;

    private List<GameObject> qubits = new List<GameObject>();
    private QuantumCircuit circuit;

    void Start()
    {
        circuit = new QuantumCircuit(0);
        
        AddQubitButton.onClick.AddListener(AddQubit);
        AddGateButton.onClick.AddListener(AddGate);
        SimulateButton.onClick.AddListener(SimulateCircuit);
    }

    void AddQubit()
    {
        GameObject qubit = Instantiate(QubitPrefab, new Vector3(qubits.Count * 2.0f, 0, 0), Quaternion.identity, QubitContainer);
        qubit.name = $"Qubit {qubits.Count}";
        qubits.Add(qubit);
        circuit.NumQubits++;
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
        switch (selectedGate)
        {
            case "Hadamard":
                circuit.AddGate(QuantumGate.Hadamard(0)); // Default to Qubit 0 for simplicity
                break;
            case "CNOT":
                if (qubits.Count > 1)
                    circuit.AddGate(QuantumGate.CNOT(0, 1)); // Default to first two qubits
                break;
            // Add cases for more gates
        }
        Debug.Log($"Added {selectedGate} gate to the circuit.");
    }

    void SimulateCircuit()
    {
        QuantumVisualizer visualizer = FindObjectOfType<QuantumVisualizer>();
        QuantumSimulator simulator = new QuantumSimulator(circuit, visualizer);
        simulator.Simulate();
    }
}
