using UnityEngine;

public class CommandFlow : MonoBehaviour
{
    public GameObject[] commandObjects; // Array of command objects
    public LineRenderer flowLine;

    void Start()
    {
        flowLine.positionCount = commandObjects.Length;
        for (int i = 0; i < commandObjects.Length; i++)
        {
            flowLine.SetPosition(i, commandObjects[i].transform.position);
        }
    }
}
