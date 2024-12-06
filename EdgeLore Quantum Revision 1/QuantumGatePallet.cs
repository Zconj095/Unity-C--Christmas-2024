using UnityEngine;

public class QuantumGatePallet : MonoBehaviour
{
    [Header("Available Gates")]
    public string[] gateTypes = { "H", "X", "Y", "Z", "RX", "RY", "RZ", "CNOT", "SWAP" };

    public string GetGate(int index)
    {
        if (index < 0 || index >= gateTypes.Length)
        {
            Debug.LogError("Invalid gate index.");
            return null;
        }
        return gateTypes[index];
    }

    public void ListAvailableGates()
    {
        Debug.Log("Available Gates: " + string.Join(", ", gateTypes));
    }
}
