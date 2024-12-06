using UnityEngine;
public class SuperpositionVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject qubitSphere;

    public void VisualizeSuperposition(int qubitIndex, int numStates)
    {
        Debug.Log($"Visualizing superposition for Qubit {qubitIndex} into {numStates} states...");

        for (int i = 0; i < numStates; i++)
        {
            // Create a semi-transparent sphere to represent a superposed state
            GameObject superposedState = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            superposedState.transform.position = qubitSphere.transform.position + Random.insideUnitSphere * 0.3f;
            superposedState.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            superposedState.GetComponent<Renderer>().material = new Material(Shader.Find("Standard"))
            {
                color = new Color(1, 1, 0, 0.5f) // Yellow, semi-transparent
            };

            Destroy(superposedState, 2.0f); // Remove after 2 seconds for transition effect
        }
    }
}
