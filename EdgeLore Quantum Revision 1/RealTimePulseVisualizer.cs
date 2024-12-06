using UnityEngine;

public class RealTimePulseVisualizer : MonoBehaviour
{
    public GameObject PulsePrefab;

    public void PlayPulse(double amplitude, double frequency, double duration)
    {
        GameObject pulse = Instantiate(PulsePrefab, Vector3.zero, Quaternion.identity);
        pulse.transform.localScale = new Vector3((float)amplitude, (float)frequency, (float)duration);

        // Animate the pulse over its duration
        StartCoroutine(AnimatePulse(pulse, (float)duration));
    }

    private System.Collections.IEnumerator AnimatePulse(GameObject pulse, float duration)
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            pulse.transform.localScale *= 1.01f; // Slight growth effect
            yield return null;
        }
        Destroy(pulse);
    }
}
