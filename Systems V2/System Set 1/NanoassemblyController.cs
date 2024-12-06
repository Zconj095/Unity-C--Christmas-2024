using System.Collections.Generic;
using UnityEngine;

public class NanoassemblyController : MonoBehaviour
{
    [Tooltip("Particle system for nanoscale particles.")]
    public ParticleSystem particleSystem;

    [Tooltip("Initial target positions for the nanoassembly.")]
    public List<Vector3> targetPositions;

    [Tooltip("Force applied to move particles.")]
    public float assemblyForce = 1.0f;

    private ParticleSystem.Particle[] particles;
    private SpatialPartition partition;

    void Start()
    {
        if (particleSystem == null)
        {
            Debug.LogError("Particle system is not assigned to NanoassemblyController.");
            enabled = false;
            return;
        }

        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        partition = new SpatialPartition(1.0f); // Set grid cell size
    }

    void Update()
    {
        if (particles == null || targetPositions == null || targetPositions.Count == 0) return;

        partition.Clear();
        int particleCount = particleSystem.GetParticles(particles);

        // Populate the spatial partition
        for (int i = 0; i < particleCount; i++)
        {
            partition.AddParticle(particles[i].position, i);
        }

        // Assign particles to nearest targets using the partition
        for (int i = 0; i < particleCount; i++)
        {
            if (i >= targetPositions.Count) break;

            Vector3 particlePos = particles[i].position;
            Vector3 targetPos = targetPositions[i];

            // Calculate force toward the target position
            Vector3 direction = (targetPos - particlePos).normalized;
            Vector3 force = direction * assemblyForce * Time.deltaTime;

            // Move particle toward the target
            particles[i].velocity += force;

            // Snap particle to target if close enough
            if (Vector3.Distance(particlePos, targetPos) < 0.1f)
            {
                particles[i].position = targetPos;
                particles[i].velocity = Vector3.zero;
            }
        }

        particleSystem.SetParticles(particles, particleCount);
    }

    public void AddTargetPosition(Vector3 position)
    {
        targetPositions.Add(position);
    }

    public void RemoveTargetPosition(Vector3 position)
    {
        targetPositions.Remove(position);
    }

    public void ClearTargetPositions()
    {
        targetPositions.Clear();
    }

    void OnDrawGizmos()
    {
        if (targetPositions == null) return;

        // Draw target positions for visualization
        Gizmos.color = Color.green;
        foreach (Vector3 position in targetPositions)
        {
            Gizmos.DrawSphere(position, 0.05f);
        }
    }
}
