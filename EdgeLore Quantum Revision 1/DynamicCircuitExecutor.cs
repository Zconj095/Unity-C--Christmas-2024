using UnityEngine;

public class DynamicCircuitExecutor : MonoBehaviour
{
    private QuantumCircuitVisualizer circuitVisualizer;
    private SuperpositionAnimator superpositionAnimator;

    void Start()
    {
        circuitVisualizer = gameObject.AddComponent<QuantumCircuitVisualizer>();
        circuitVisualizer.InitializeCircuitVisualization();

        superpositionAnimator = gameObject.AddComponent<SuperpositionAnimator>();
    }

    public void ExecuteCircuitWithVisualization()
    {
        Debug.Log("Executing dynamic circuit visualization...");

        // Simulate applying gates sequentially with visualization
        StartCoroutine(ApplyGatesSequentially());
    }

    private System.Collections.IEnumerator ApplyGatesSequentially()
    {
        circuitVisualizer.AddGate("H", 0);
        superpositionAnimator.AnimateSuperposition(0, 5);
        yield return new WaitForSeconds(2);

        circuitVisualizer.AddGate("CNOT", 1, 0);
        yield return new WaitForSeconds(2);

        Debug.Log("Circuit execution completed.");
    }
}
