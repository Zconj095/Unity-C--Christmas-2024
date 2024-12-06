using UnityEngine;

public class EnhancedBlochSphereVisualizer : MonoBehaviour
{
    public GameObject SpherePrefab;

    public void VisualizeState(QuantumState state)
    {
        for (int i = 0; i < state.StateVector.Length; i++)
        {
            Vector3 position = new Vector3(i * 2.0f, 0, 0);
            GameObject sphere = Instantiate(SpherePrefab, position, Quaternion.identity);
            sphere.name = $"Qubit {i} Bloch Sphere";

            Renderer renderer = sphere.GetComponent<Renderer>();
            renderer.material.color = new Color(
                Mathf.Abs((float)state.StateVector[i].Real), 
                Mathf.Abs((float)state.StateVector[i].Imaginary), 
                1.0f - Mathf.Abs((float)state.StateVector[i].Magnitude)
            );
        }
    }
}
