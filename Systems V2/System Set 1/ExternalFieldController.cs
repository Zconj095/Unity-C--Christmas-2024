using UnityEngine;

public class ExternalFieldController : MonoBehaviour
{
    [Tooltip("Strength of the external field.")]
    public float fieldStrength = 1.0f;

    [Tooltip("Direction of the external field.")]
    public Vector3 fieldDirection = Vector3.up;

    public void ApplyField(NanoparticleBehavior particle)
    {
        // Apply field force to the particle
        particle.ApplyForce(fieldDirection.normalized * fieldStrength);
    }
}
