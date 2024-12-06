using UnityEngine;

public class NanoParticle : MonoBehaviour
{
    public float materialDensity = 1.0f; // Density of the material
    public Color particleColor = Color.gray; // Appearance for SEM visualization

    void Start()
    {
        // Assign visual representation
        GetComponent<Renderer>().material.color = particleColor;
    }
}
