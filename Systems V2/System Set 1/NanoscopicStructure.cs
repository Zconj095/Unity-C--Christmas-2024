using UnityEngine;

[System.Serializable]
public class NanoscopicStructure : MonoBehaviour
{
    public string Name;
    public Vector3 Position;
    public float Charge; // Represents the electric/magnetic charge
    public float InteractionRadius;
    public float FieldIntensity;

    public NanoscopicStructure(string name, Vector3 position, float charge, float radius, float intensity)
    {
        Name = name;
        Position = position;
        Charge = charge;
        InteractionRadius = radius;
        FieldIntensity = intensity;
    }

    public Vector3 CalculateField(Vector3 point)
    {
        Vector3 direction = point - Position;
        float distance = direction.magnitude;

        if (distance > InteractionRadius)
            return Vector3.zero;

        float fieldStrength = FieldIntensity / Mathf.Pow(distance, 2); // Inverse-square law
        return direction.normalized * fieldStrength * Charge;
    }
}
