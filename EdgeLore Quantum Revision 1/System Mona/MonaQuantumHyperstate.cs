using UnityEngine;

public class MonaQuantumHyperstate : MonoBehaviour
{
    public Vector3 Input1;
    public Vector3 Input2;

    public Vector3 FormHyperstate()
    {
        Debug.Log($"Forming Quantum Hyperstate from Input1: {Input1}, Input2: {Input2}");
        Vector3 hyperstate = (Input1 + Input2) / 2.0f; // Average of the two inputs
        Debug.Log($"Quantum Hyperstate: {hyperstate}");
        return hyperstate;
    }
}
