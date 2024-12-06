using UnityEngine;

public class QuantumCircuitInteraction : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject selectedObject = hit.collider.gameObject;
                Debug.Log($"Selected: {selectedObject.name}");
                DisplayQubitDetails(selectedObject);
            }
        }
    }

    private void DisplayQubitDetails(GameObject qubit)
    {
        Debug.Log($"Displaying details for {qubit.name}...");
        // Add UI or additional logic to show qubit state and associated gates
    }
}
