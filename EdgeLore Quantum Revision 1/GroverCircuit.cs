using UnityEngine;

public class GroverCircuit : MonoBehaviour
{
    public void ExecuteGrover(int numQubits, int markedElement)
    {
        Debug.Log($"Executing Grover's Algorithm with {numQubits} qubits...");
        
        // Initialize state
        Debug.Log("Initializing all qubits to superposition...");
        
        // Oracle
        Debug.Log($"Applying Oracle to mark element {markedElement}...");
        
        // Diffusion operator
        Debug.Log("Applying Diffusion Operator...");
        
        Debug.Log("Grover's Algorithm completed.");
    }
}
