using UnityEngine;

public class QuantumFieldGenerator : MonoBehaviour
{
    public Vector3 InputVector;

    public Vector3 GenerateField()
    {
        Debug.Log($"Generating Quantum Field from Input Vector: {InputVector}");
        Vector3 field = new Vector3(
            Mathf.Sin(InputVector.x),
            Mathf.Cos(InputVector.y),
            Mathf.Tan(InputVector.z)
        );
        Debug.Log($"Generated Quantum Field: {field}");
        return field;
    }
}
