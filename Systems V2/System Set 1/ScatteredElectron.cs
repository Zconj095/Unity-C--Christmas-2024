using UnityEngine;

public class ScatteredElectron
{
    public Vector3 position;     // Current position of the electron
    public Vector3 direction;    // Scattering direction
    public float energy;         // Energy of the electron
    public float scatteringAngle; // Angle of scattering

    public ScatteredElectron(Vector3 position, Vector3 direction, float energy, float scatteringAngle)
    {
        this.position = position;
        this.direction = direction.normalized;
        this.energy = energy;
        this.scatteringAngle = scatteringAngle;
    }

    public void Scatter(Vector3 normal)
    {
        // Randomly generate a new scattering direction
        float theta = Random.Range(0, scatteringAngle * Mathf.Deg2Rad);
        float phi = Random.Range(0, 2 * Mathf.PI);

        Vector3 randomDirection = new Vector3(
            Mathf.Sin(theta) * Mathf.Cos(phi),
            Mathf.Sin(theta) * Mathf.Sin(phi),
            Mathf.Cos(theta)
        );

        // Reflect random direction relative to the surface normal
        direction = Vector3.Reflect(randomDirection, normal).normalized;
    }

    public void Move(float distance)
    {
        position += direction * distance;
    }
}
