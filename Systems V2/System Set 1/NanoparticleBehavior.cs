using UnityEngine;

public class NanoparticleBehavior : MonoBehaviour
{
    public Vector3 velocity;
    public float drag = 0.1f;

    void Update()
    {
        // Apply drag to slow down particles
        velocity *= (1.0f - drag * Time.deltaTime);

        // Move particle
        transform.position += velocity * Time.deltaTime;
    }

    public void ApplyForce(Vector3 force)
    {
        velocity += force;
    }
}
