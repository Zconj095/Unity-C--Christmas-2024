using UnityEngine;
public class Qubit : MonoBehaviour
{
    public float Amplitude0; // Amplitude for state |0⟩
    public float Amplitude1; // Amplitude for state |1⟩

    public Qubit(float amplitude0, float amplitude1)
    {
        Amplitude0 = amplitude0;
        Amplitude1 = amplitude1;
        Normalize();
    }

    // Ensure the amplitudes are normalized
    public void Normalize()
    {
        float magnitude = Mathf.Sqrt(Amplitude0 * Amplitude0 + Amplitude1 * Amplitude1);
        Amplitude0 /= magnitude;
        Amplitude1 /= magnitude;
    }

    public override string ToString()
    {
        return $"|0⟩: {Amplitude0}, |1⟩: {Amplitude1}";
    }
}
