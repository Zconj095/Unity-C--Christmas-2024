using UnityEngine;

public class SystemStateVisualizer : MonoBehaviour
{
    public TextMesh energyText;

    public void UpdateEnergy(float energy)
    {
        energyText.text = $"Energy: {energy:F2}";
    }
}
