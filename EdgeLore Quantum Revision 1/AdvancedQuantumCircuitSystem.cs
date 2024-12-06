using UnityEngine;
public class AdvancedQuantumCircuitSystem : MonoBehaviour
{
    private DynamicCircuitExecutor circuitExecutor;
    private QuantumCircuitInteraction circuitInteraction;

    void Start()
    {
        circuitExecutor = gameObject.AddComponent<DynamicCircuitExecutor>();
        circuitInteraction = gameObject.AddComponent<QuantumCircuitInteraction>();

        circuitExecutor.ExecuteCircuitWithVisualization();
    }
}
