using UnityEngine;

public class ParticleStateCircuit : MonoBehaviour
{
    public void CreateParticleState(int numParticles, Vector3 initialPosition)
    {
        Debug.Log($"Simulating {numParticles} particle states...");

        for (int i = 0; i < numParticles; i++)
        {
            Vector3 particlePosition = initialPosition + new Vector3(i, i * 0.1f, i * 0.2f);
            Debug.Log($"Particle {i} initialized at position: {particlePosition}");
        }

        Debug.Log("Particle states created.");
    }
}
