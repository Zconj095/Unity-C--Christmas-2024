using UnityEngine;

public class PhotonLightField : MonoBehaviour
{
    [Header("Light Field Settings")]
    [SerializeField] private int numParticles = 1000;          // Number of particles
    [SerializeField] private float fieldRadius = 5.0f;         // Radius of the field
    [SerializeField] private float luminescenceIntensity = 2.0f; // Luminescence factor
    [SerializeField] private Gradient particleColorGradient;   // Gradient for particle colors

    [Header("Prism Formation Settings")]
    [SerializeField] private float prismHeight = 3.0f;         // Height of the prism
    [SerializeField] private float prismBaseRadius = 2.0f;     // Base radius of the prism
    [SerializeField] private float prismRotationSpeed = 50.0f; // Rotation speed of the prism

    private ParticleSystem particleSystem;

    private void Start()
    {
        InitializeLightField();
    }

    private void Update()
    {
        RotatePrismFormation();
    }

    private void InitializeLightField()
    {
        particleSystem = GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.maxParticles = numParticles;

        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = fieldRadius;

        var colorOverLifetime = particleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(particleColorGradient);

        var emission = particleSystem.emission;
        emission.rateOverTime = numParticles * luminescenceIntensity;

        particleSystem.Play();
    }

    private void RotatePrismFormation()
    {
        transform.Rotate(Vector3.up, prismRotationSpeed * Time.deltaTime);
    }

    public void FormPrism()
    {
        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 15f;
        shape.radius = prismBaseRadius;

        var main = particleSystem.main;
        main.startSpeed = prismHeight;

        Debug.Log("Prism Formation Activated.");
    }
}
