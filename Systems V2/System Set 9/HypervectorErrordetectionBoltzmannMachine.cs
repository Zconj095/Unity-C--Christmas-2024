using UnityEngine;
public class HypervectorErrordetectionBoltzmannMachine : MonoBehaviour
{
    public static float CalculateProbability(float energy)
    {
        // Boltzmann distribution: P(E) = e^(-E) / Z
        return Mathf.Exp(-energy); // Partition function Z can be normalized later
    }
}