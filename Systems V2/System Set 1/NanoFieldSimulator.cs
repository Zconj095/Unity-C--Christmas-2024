using UnityEngine;

public class NanoFieldSimulator : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public NanoscopicStructure[] nanostructures;

    private ParticleSystem.Particle[] particles;

    void Start()
    {
        if (particleSystem == null)
        {
            Debug.LogError("Particle system is not assigned.");
            enabled = false; // Prevent further execution
            return;
        }

        // Ensure the particles array is initialized with max capacity
        int maxParticles = particleSystem.main.maxParticles;
        if (maxParticles <= 0)
        {
            Debug.LogError("The particle system has maxParticles set to 0. Set it to a positive value in the Inspector.");
            enabled = false; // Disable script to prevent errors
            return;
        }

        particles = new ParticleSystem.Particle[maxParticles];
    }


    void Update()
    {
        if (particles == null)
        {
            Debug.LogError("Particles array is null. Ensure it is initialized properly in Start().");
            return;
        }

        // Get current particles from the particle system
        int particleCount = particleSystem.GetParticles(particles);

        // Update each particle's velocity based on field effects
        for (int i = 0; i < particleCount; i++)
        {
            Vector3 totalField = Vector3.zero;

            foreach (NanoscopicStructure structure in nanostructures)
            {
                if (structure != null)
                {
                    totalField += structure.CalculateField(particles[i].position);
                }
            }

            particles[i].velocity += totalField * Time.deltaTime;
        }

        particleSystem.SetParticles(particles, particleCount);
    }


    void OnDrawGizmos()
    {
        if (nanostructures == null) return;

        foreach (var structure in nanostructures)
        {
            if (structure != null)
            {
                Gizmos.color = new Color(1.0f, 0.5f, 0.0f, 0.3f);
                Gizmos.DrawSphere(structure.Position, structure.InteractionRadius);
            }
        }
    }

    
}
