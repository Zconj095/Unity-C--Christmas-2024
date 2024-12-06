using System.Collections.Generic;
using UnityEngine;

public class ParticleSimulationWithGrid : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float cellSize = 1.0f;
    public float interactionRadius = 1.5f;

    private ParticleSystem.Particle[] particles;
    private SpatialGridHashing spatialGrid;

    void Start()
    {
        if (particleSystem == null)
        {
            Debug.LogError("Particle system is not assigned.");
            enabled = false;
            return;
        }

        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        spatialGrid = new SpatialGridHashing(cellSize);
    }

    void Update()
    {
        spatialGrid.Clear();
        int particleCount = particleSystem.GetParticles(particles);

        // Populate the spatial grid with particle positions
        for (int i = 0; i < particleCount; i++)
        {
            spatialGrid.Add(particles[i].position, i);
        }

        // Apply interactions between particles
        for (int i = 0; i < particleCount; i++)
        {
            List<int> neighbors = spatialGrid.GetNeighbors(particles[i].position);

            foreach (int neighborIndex in neighbors)
            {
                if (neighborIndex == i) continue;

                Vector3 direction = particles[neighborIndex].position - particles[i].position;
                float distance = direction.magnitude;

                if (distance < interactionRadius)
                {
                    // Apply a repulsive force as an example
                    Vector3 repulsiveForce = -direction.normalized * (interactionRadius - distance) * Time.deltaTime;
                    particles[i].velocity += repulsiveForce;
                }
            }
        }

        particleSystem.SetParticles(particles, particleCount);
    }

    void OnDrawGizmos()
    {
        if (particles == null) return;

        // Draw interaction radius for particles
        Gizmos.color = Color.yellow;
        foreach (var particle in particles)
        {
            Gizmos.DrawWireSphere(particle.position, interactionRadius);
        }
    }
}
