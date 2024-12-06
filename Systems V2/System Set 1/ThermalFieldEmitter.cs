using UnityEngine;

public class ThermalFieldEmitter : MonoBehaviour
{
    [Tooltip("Filament type asset containing properties like temperature and vacuum requirements.")]
    public FilamentTypeV2 filament; // Assigned ScriptableObject containing filament data

    [Tooltip("Particle system used for emission visualization.")]
    public ParticleSystem particleSystem; // Particle system for visualizing emissions

    private float currentTemperature; // Current operating temperature
    private float elapsedLifetime; // Tracks how long the filament has been active

    void Start()
    {
        if (filament == null)
        {
            Debug.LogError("Filament type not assigned to ThermalFieldEmitter! Please assign a ScriptableObject asset.");
            enabled = false;
            return;
        }

        if (particleSystem == null)
        {
            Debug.LogError("Particle system not assigned to ThermalFieldEmitter!");
            enabled = false;
            return;
        }

        // Initialize temperature and lifetime
        currentTemperature = filament.operatingTemperature * 0.5f; // Start at half the operating temperature
        elapsedLifetime = 0;

        Debug.Log($"{filament.filamentName} initialized with temperature {currentTemperature}K.");
    }


    void Update()
    {
        elapsedLifetime += Time.deltaTime;

        // Check filament lifetime
        if (elapsedLifetime >= filament.lifetime)
        {
            if (filament.regenerates)
            {
                elapsedLifetime = 0; // Reset lifetime
                Debug.Log($"{filament.filamentName} regenerated!");
            }
            else
            {
                Debug.Log($"{filament.filamentName} has burned out.");
                particleSystem.Stop();
                enabled = false;
                return;
            }
        }

        // Simulate vacuum requirements
        if (filament.requiredVacuum > 1.0f) // Example threshold for failure
        {
            Debug.LogWarning($"Vacuum requirements not met for {filament.filamentName}! Current vacuum: {filament.requiredVacuum}");
            particleSystem.Stop();
            return; // Log warning and stop further updates
        }

        // Gradually adjust the temperature toward the operating temperature
        currentTemperature = Mathf.Lerp(currentTemperature, filament.operatingTemperature, Time.deltaTime);

        // Calculate brightness based on current temperature
        float brightnessFactor = currentTemperature / filament.operatingTemperature;

        // Adjust particle emission and visuals
        AdjustEmissionBrightness(brightnessFactor);
        UpdateParticleEmission(brightnessFactor);
    }

    private void AdjustEmissionBrightness(float factor)
    {
        var main = particleSystem.main;

        // Ensure color is not black and alpha is sufficient
        Color adjustedColor = filament.filamentColor * factor;
        if (adjustedColor == Color.black) adjustedColor = Color.white * factor;
        adjustedColor.a = Mathf.Max(adjustedColor.a, 0.5f); // Ensure alpha is visible

        Debug.Log($"Adjusted brightness factor: {factor}, resulting color: {adjustedColor}");
        main.startColor = adjustedColor;
    }

    private void UpdateParticleEmission(float brightnessFactor)
    {
        // Update the emission rate of the particle system
        var emission = particleSystem.emission;
        emission.rateOverTime = brightnessFactor * filament.emissionCurrentDensity;

        Debug.Log($"Updated emission rate: {emission.rateOverTime.constant}");
    }
}
