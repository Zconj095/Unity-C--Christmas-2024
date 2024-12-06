using UnityEngine;

public class ToffoliGate : MonoBehaviour
{
    public Vector3 ControlQubit;
    public Vector3 TargetQubit;

    public Vector3 Operate()
    {
        // Perform a conditional operation on the target qubit based on control qubits
        Debug.Log($"Applying Toffoli Gate with Control: {ControlQubit}, Target: {TargetQubit}");

        // Example: Conditional flipping of target qubit if all control qubits are 1
        if (AreQubitsValid(ControlQubit) && AreQubitsValid(TargetQubit))
        {
            if (ControlQubit.x == 1 && ControlQubit.y == 1 && ControlQubit.z == 1)
            {
                TargetQubit = new Vector3(1 - TargetQubit.x, 1 - TargetQubit.y, 1 - TargetQubit.z);
            }
            Debug.Log($"Resultant Qubit: {TargetQubit}");
        }
        else
        {
            Debug.LogError("Invalid qubit states detected. Aborting Toffoli gate operation.");
        }

        return TargetQubit;
    }

    private bool AreQubitsValid(Vector3 qubit)
    {
        // Ensure qubit components are either 0 or 1
        return (qubit.x == 0 || qubit.x == 1) &&
               (qubit.y == 0 || qubit.y == 1) &&
               (qubit.z == 0 || qubit.z == 1);
    }
}
