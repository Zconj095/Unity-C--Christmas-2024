using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GateDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    public QuantumCircuit circuit; // Assume this is assigned elsewhere in the script

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Started dragging gate...");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform != null && canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        string gateName = gameObject.name; // Assume the GameObject's name is the gate name
        GameObject qubitObject = DetectQubit(eventData);
        if (qubitObject != null)
        {
            int qubitIndex = GetQubitIndex(qubitObject);
            if (qubitIndex >= 0)
            {
                AttachGateToCircuit(gateName, qubitIndex);
            }
            else
            {
                Debug.LogWarning("Invalid qubit index detected.");
            }
        }
        else
        {
            Debug.LogError("No qubit detected under drop location.");
        }
    }

    private GameObject DetectQubit(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Qubit")) // Ensure qubits have the "Qubit" tag
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    private int GetQubitIndex(GameObject qubit)
    {
        // Assuming qubit GameObject names are "Qubit X" where X is the index
        string[] parts = qubit.name.Split(' ');
        if (parts.Length == 2 && int.TryParse(parts[1], out int index))
        {
            return index;
        }
        Debug.LogError("Could not parse qubit index from object name.");
        return -1;
    }

    private void AttachGateToCircuit(string gateName, int qubitIndex)
    {
        QuantumGate gate = null;

        // Create the appropriate QuantumGate based on the name
        switch (gateName)
        {
            case "Hadamard":
                gate = QuantumGate.Hadamard(qubitIndex);
                break;

            case "CNOT":
                if (circuit.NumQubits > 1) // Ensure there's at least 2 qubits
                {
                    gate = QuantumGate.CNOT(0, qubitIndex); // Default control qubit is 0
                }
                break;

            case "Pauli-X":
                gate = QuantumGate.PauliX(qubitIndex);
                break;

            case "Pauli-Y":
                gate = QuantumGate.PauliY(qubitIndex);
                break;

            case "Pauli-Z":
                gate = QuantumGate.PauliZ(qubitIndex);
                break;

            case "RX":
                gate = QuantumGate.RX(qubitIndex, Math.PI / 2); // Rotate around X-axis by π/2
                break;

            case "RY":
                gate = QuantumGate.RY(qubitIndex, Math.PI / 2); // Rotate around Y-axis by π/2
                break;

            case "RZ":
                gate = QuantumGate.RZ(qubitIndex, Math.PI / 2); // Rotate around Z-axis by π/2
                break;

            case "Phase":
                gate = QuantumGate.Phase(qubitIndex, Math.PI / 4); // Phase gate with π/4 as default
                break;

            case "SWAP":
                if (circuit.NumQubits > 1) // Ensure there's at least 2 qubits
                {
                    gate = QuantumGate.SWAP(0, qubitIndex); // Default to swapping with qubit 0
                }
                break;

            case "Toffoli":
                if (circuit.NumQubits > 2) // Ensure there are at least 3 qubits
                {
                    gate = QuantumGate.Toffoli(0, 1, qubitIndex); // Default control qubits are 0 and 1
                }
                break;

            case "Fredkin":
                if (circuit.NumQubits > 2) // Ensure there are at least 3 qubits
                {
                    gate = QuantumGate.Fredkin(0, qubitIndex, 1); // Default control is 0, target is qubitIndex, and swap is 1
                }
                break;

            default:
                Debug.LogError($"Unknown gate name: {gateName}");
                break;
        }

        if (gate != null)
        {
            circuit.AddGate(gate);
            Debug.Log($"{gateName} gate added to qubit {qubitIndex}");
        }
        else
        {
            Debug.LogWarning($"Invalid gate: {gateName} or incompatible qubit index.");
        }
    }
}
