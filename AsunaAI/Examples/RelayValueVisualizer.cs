using System.Collections.Generic;
using UnityEngine;

public class RelayValueVisualizer : MonoBehaviour
{
    [Header("Relay Visualization")]
    public Transform[] relayBars; // Assign objects for each relay value

    public void UpdateRelayValues(Dictionary<string, float> relayValues)
    {
        int index = 0;
        foreach (var relay in relayValues)
        {
            if (index < relayBars.Length)
            {
                Vector3 scale = relayBars[index].localScale;
                scale.y = relay.Value; // Scale based on relay value
                relayBars[index].localScale = scale;

                index++;
            }
        }
    }
}
