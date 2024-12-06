using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class CircuitFlowDebugger : MonoBehaviour
{
    public void HighlightGate(QuantumGate gate)
    {
        Debug.Log($"Applying gate: {gate.Name}");

        // Highlight the gate's visual representation
        GameObject gateObject = GameObject.Find(gate.Name);
        if (gateObject != null)
        {
            Renderer renderer = gateObject.GetComponent<Renderer>();
            renderer.material.color = Color.yellow;

            // Reset color after a short delay
            StartCoroutine(ResetColor(renderer));
        }
    }

    private IEnumerator ResetColor(Renderer renderer)
    {
        yield return new WaitForSeconds(1.0f);
        renderer.material.color = Color.white;
    }
}
