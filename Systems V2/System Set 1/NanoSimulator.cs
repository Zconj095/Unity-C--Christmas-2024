using UnityEngine;

public class NanoSimulator : MonoBehaviour
{
    public float fluctuationIntensity = 0.05f;

    public Vector3 SimulateNanoFluctuation(Vector3 input)
    {
        Vector3 fluctuation = new Vector3(
            Random.Range(-fluctuationIntensity, fluctuationIntensity),
            Random.Range(-fluctuationIntensity, fluctuationIntensity),
            Random.Range(-fluctuationIntensity, fluctuationIntensity)
        );
        return input + fluctuation;
    }
}
