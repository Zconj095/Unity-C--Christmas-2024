using UnityEngine;
using System.Collections.Generic;

public class SmoothBlochSphere : MonoBehaviour
{
    public GameObject SpherePrefab;
    private Dictionary<int, GameObject> qubitSpheres = new Dictionary<int, GameObject>();

    public void InitializeQubits(int numQubits)
    {
        for (int i = 0; i < numQubits; i++)
        {
            Vector3 position = new Vector3(i * 2.0f, 0, 0);
            GameObject sphere = Instantiate(SpherePrefab, position, Quaternion.identity);
            sphere.name = $"Qubit {i} Bloch Sphere";
            qubitSpheres[i] = sphere;
        }
    }

    public void UpdateState(QuantumState state)
    {
        for (int i = 0; i < state.StateVector.Length; i++)
        {
            GameObject sphere = qubitSpheres[i];
            Renderer renderer = sphere.GetComponent<Renderer>();

            // Interpolate colors smoothly
            Color targetColor = new Color(
                Mathf.Abs((float)state.StateVector[i].Real),
                Mathf.Abs((float)state.StateVector[i].Imaginary),
                1.0f - Mathf.Abs((float)state.StateVector[i].Magnitude)
            );
            renderer.material.color = Color.Lerp(renderer.material.color, targetColor, Time.deltaTime * 2.0f);
        }
    }
}
