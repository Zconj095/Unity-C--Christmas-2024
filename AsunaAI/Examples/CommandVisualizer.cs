using UnityEngine;

public class CommandVisualizer : MonoBehaviour
{
    [Header("Pipeline Settings")]
    public GameObject commandPrefab; // Prefab for command objects (e.g., Cube)
    public float spacing = 2.0f; // Spacing between objects

    [Header("Command Sequence")]
    public string[] wordSequence = { "SWITCH", "BIND", "CALIBRATE", "RELEASE" };

    private GameObject[] commandObjects;

    void Start()
    {
        VisualizeCommands();
    }

    void VisualizeCommands()
    {
        commandObjects = new GameObject[wordSequence.Length];

        for (int i = 0; i < wordSequence.Length; i++)
        {
            // Instantiate a command object
            GameObject commandObject = Instantiate(commandPrefab, transform);
            commandObject.transform.position = new Vector3(i * spacing, 0, 0);
            commandObject.name = wordSequence[i];

            // Assign a label to the object
            TextMesh text = commandObject.GetComponentInChildren<TextMesh>();
            if (text != null)
                text.text = wordSequence[i];

            commandObjects[i] = commandObject;
        }
    }

    public void HighlightCommand(int index, Color color)
    {
        if (index >= 0 && index < commandObjects.Length)
        {
            Renderer renderer = commandObjects[index].GetComponent<Renderer>();
            if (renderer != null)
                renderer.material.color = color;
        }
    }
}
