using UnityEngine;

public class NanoParticleV2 : MonoBehaviour
{
    public float materialDensity = 1.0f; // Density of the material
    public float absorptionCoefficient = 0.8f; // Probability of electron absorption (0 to 1)
    public float conductionEfficiency = 0.6f; // Probability of electron conduction (0 to 1)
    public Color particleColor = Color.gray; // Appearance for SEM visualization

    void Start()
    {
        // Assign visual representation
        GetComponent<Renderer>().material.color = particleColor;
    }

    public (float, float) InteractWithElectron(Vector3 impactDirection)
    {
        // Simulate electron interaction
        float absorbed = absorptionCoefficient * materialDensity;
        float conducted = conductionEfficiency * (1.0f - absorbed);

        return (conducted, absorbed);
    }
}
