using UnityEngine;

public class HadamardGate : MonoBehaviour
{
    public Vector3 InputState;

    public (Vector3 PhotonOutput, Vector3 ElectronOutput) ApplyHadamard()
    {
        Debug.Log($"Applying Hadamard Gate to state: {InputState}");

        Vector3 photonOutput = new Vector3(
            (InputState.x + InputState.y) / Mathf.Sqrt(2),
            (InputState.x - InputState.y) / Mathf.Sqrt(2),
            InputState.z
        );

        Vector3 electronOutput = new Vector3(
            (InputState.x - InputState.z) / Mathf.Sqrt(2),
            (InputState.y + InputState.z) / Mathf.Sqrt(2),
            InputState.z
        );

        Debug.Log($"Photon Output: {photonOutput}, Electron Output: {electronOutput}");
        return (photonOutput, electronOutput);
    }
}
