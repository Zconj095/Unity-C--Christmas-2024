using UnityEngine;

[CreateAssetMenu(fileName = "NewFilamentV1Type", menuName = "Nanotech/FilamentTypeV1")]
public class FilamentTypeV1 : ScriptableObject
{
    public string filamentName;
    public float tensileStrength;
    public float elasticity;
    public Color filamentColor;
}
