using UnityEngine;

public class USBDevice : MonoBehaviour
{
    public float connectionRadius = 10f; // Connection radius in units
    public USBDevice otherDevice; // Reference to the other USB device
    public bool isConnected;

    void Update()
    {
        CheckConnection();
    }

    void CheckConnection()
    {
        if (otherDevice == null) return;

        float distance = Vector3.Distance(transform.position, otherDevice.transform.position);

        if (distance <= connectionRadius)
        {
            isConnected = true;
            Debug.Log("Connected to device");
        }
        else
        {
            isConnected = false;
            Debug.Log("Disconnected from device");
        }
    }
}
