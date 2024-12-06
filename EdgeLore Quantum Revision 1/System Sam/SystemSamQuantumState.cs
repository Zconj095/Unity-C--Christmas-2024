using UnityEngine;

public class SystemSamQuantumState : MonoBehaviour
{
    public Vector3 HMMInput;
    public Vector3 SVMInput;

    public Vector3 GenerateState()
    {
        Debug.Log("Generating Quantum State...");
        Vector3 quantumState = (HMMInput + SVMInput) / 2.0f;
        Debug.Log($"Quantum State: {quantumState}");
        return quantumState;
    }
}
