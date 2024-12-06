using UnityEngine;

public class ElectronColumn : MonoBehaviour
{
    [Tooltip("Position of the electron source.")]
    public Transform electronSource;

    [Tooltip("Position of the aperture.")]
    public Transform aperture;

    [Tooltip("Position of the target.")]
    public Transform target;

    [Tooltip("Beam width at the source.")]
    public float sourceBeamWidth = 1.0f;

    [Tooltip("Beam width at the aperture.")]
    public float apertureBeamWidth = 0.1f;

    [Tooltip("Electron intensity (number of electrons emitted per unit time).")]
    public float electronIntensity = 1000.0f;

    [Tooltip("Maximum aperture radius.")]
    public float apertureRadius = 0.5f;

    private LineRenderer beamRenderer;

    void Start()
    {
        if (electronSource == null || aperture == null || target == null)
        {
            Debug.LogError("ElectronColumn requires electronSource, aperture, and target to be assigned.");
            enabled = false;
            return;
        }

        // Initialize LineRenderer for visualizing the beam
        beamRenderer = gameObject.AddComponent<LineRenderer>();
        beamRenderer.startWidth = sourceBeamWidth;
        beamRenderer.endWidth = apertureBeamWidth;
        beamRenderer.material = new Material(Shader.Find("Unlit/Color"));
        beamRenderer.material.color = Color.cyan;
        beamRenderer.positionCount = 3;
    }

    void Update()
    {
        SimulateElectronBeam();
    }

    private void SimulateElectronBeam()
    {
        // Simulate beam passing through electron source, aperture, and to target
        Vector3[] positions = new Vector3[3];
        positions[0] = electronSource.position;
        positions[1] = aperture.position;
        positions[2] = target.position;

        // Update the line renderer positions
        beamRenderer.SetPositions(positions);

        // Check if the beam width fits the aperture radius
        float beamRadius = Vector3.Distance(electronSource.position, aperture.position) * apertureBeamWidth;
        if (beamRadius > apertureRadius)
        {
            Debug.LogWarning("Beam exceeds aperture radius. Adjust source or aperture size.");
        }
    }

    private void OnDrawGizmos()
    {
        if (electronSource != null && aperture != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(electronSource.position, sourceBeamWidth);
            Gizmos.DrawWireSphere(aperture.position, apertureRadius);
        }

        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(target.position, 0.1f);
        }
    }
}
