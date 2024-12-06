using UnityEngine;

[CreateAssetMenu(fileName = "NewFilamentV2Type", menuName = "Nanotech/FilamentTypeV2")]
public class FilamentTypeV2 : ScriptableObject
{
    public string filamentName;
    public Color filamentColor; // Visual representation
    public float operatingTemperature; // Kelvin
    public float brightness; // Lumens
    public float requiredVacuum; // Torr
    public float emissionCurrentDensity; // A/in²
    public float lifetime; // Seconds
    public bool regenerates; // Can filament regenerate?
}
