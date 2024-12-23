using UnityEngine;
using System;
using System.Collections.Generic;

public class SystemRelayInteraction : MonoBehaviour
{
    [Header("System Settings")]
    [SerializeField] private float initialMainframeEnergy = 100.0f; // Initial energy
    [SerializeField] private float energyConsumptionPerLink = 10.0f;
    [SerializeField] private float calibrationFactor = 0.9f;
    [SerializeField] private float delayAdjustment = 0.5f;

    [Header("Word Actions (Editable)")]
    public List<string> wordSequence = new List<string>
    {
        "SWITCH", "BIND", "LINK", "CALIBRATE", "RELEASE", "ADJUST", "ACTIVATE", "ACTIVATE_FINAL"
    };

    [Header("Relay Values (Editable)")]
    public Dictionary<string, float> relayValues = new Dictionary<string, float>
    {
        { "Relay1", 0f },
        { "Relay2", 0f },
        { "Relay3", 0f }
    };

    private Dictionary<string, Action> wordActions;
    private float mainframeEnergy;
    private bool isSystemActivated = false;

    void Start()
    {
        mainframeEnergy = initialMainframeEnergy;
        InitializeWordActions();
        SYSTEMDEFININGFORRELAYINTERACTION();
    }

    // Entry point for the system definition process
    public void SYSTEMDEFININGFORRELAYINTERACTION()
    {
        Debug.Log("***************************************************************************************");
        Debug.Log("Starting System Defining for Relay Interaction...");
        Debug.Log("***************************************************************************************");

        foreach (var word in wordSequence)
        {
            ExecuteAction(word);
        }

        Debug.Log("***************************************************************************************");
        Debug.Log("System Defining for Relay Interaction Complete!");
        Debug.Log("***************************************************************************************");
    }

    // Maps words to specific functions
    private void InitializeWordActions()
    {
        wordActions = new Dictionary<string, Action>
        {
            { "SWITCH", SwitchRelay },
            { "BIND", BindRelay },
            { "LINK", LinkSystem },
            { "CALIBRATE", CalibrateRelay },
            { "RELEASE", ReleaseFrequency },
            { "ADJUST", AdjustDelay },
            { "ACTIVATE", ActivateUserInterface },
            { "ACTIVATE_FINAL", FinalActivation }
        };
    }

    // Execute action based on word
    private void ExecuteAction(string word)
    {
        if (wordActions.ContainsKey(word))
        {
            Debug.Log($"Executing action for word: {word}");
            wordActions[word]?.Invoke();
        }
        else
        {
            Debug.LogWarning($"No action found for word: {word}");
        }
    }

    // Action: Switch Relay
    private void SwitchRelay()
    {
        Debug.Log($"{Language_Extension_001_2.SWITCH}, {Language_Extension_000_2.FREQUENCY}, {Language_Extension_005_2.RELAY}, {Language_Extension_000_2.UNITS}");

        // Create a copy of the keys to avoid modifying the dictionary while iterating
        var keys = new List<string>(relayValues.Keys);

        foreach (var key in keys)
        {
            relayValues[key] = UnityEngine.Random.Range(1.0f, 5.0f); // Assign a random value
        }

        Debug.Log("Relays switched. Updated values:");
        foreach (var relay in relayValues)
        {
            Debug.Log($"{relay.Key}: {relay.Value:F2}");
        }
    }


    // Action: Bind Relay
    private void BindRelay()
    {
        Debug.Log($"{Language_Extension_000_2.BIND}, {Language_Extension_000_2.SYSTEM}, {Language_Extension_005_2.RELAY}, {Language_Extension_000_2.ACTIVATION}");
        isSystemActivated = relayValues.ContainsKey("Relay1") && relayValues["Relay1"] > 2.0f;
        Debug.Log(isSystemActivated ? "System successfully bound and activated." : "Binding failed. Activation threshold not met.");
    }

    // Action: Link System
    private void LinkSystem()
    {
        Debug.Log($"{Language_Extension_000_2.LINK}, {Language_Extension_000_2.SYSTEM}, {Language_Extension_000_2.VALUES}, {Language_Extension_000_2.MAINFRAME}");
        if (isSystemActivated)
        {
            mainframeEnergy -= energyConsumptionPerLink;
            Debug.Log($"Linked system values to mainframe. Remaining energy: {mainframeEnergy:F2}");
        }
        else
        {
            Debug.LogWarning("System is not activated. Linking aborted.");
        }
    }

    // Action: Calibrate Relay
    private void CalibrateRelay()
    {
        Debug.Log($"{Language_Extension_000_2.CALIBRATE}, {Language_Extension_005_2.RELAY}, {Language_Extension_000_2.FUNCTION}");

        // Create a copy of the keys to avoid modifying the dictionary while iterating
        var keys = new List<string>(relayValues.Keys);

        foreach (var key in keys)
        {
            relayValues[key] *= calibrationFactor; // Reduce each relay value
        }

        Debug.Log("Relay values after calibration:");
        foreach (var relay in relayValues)
        {
            Debug.Log($"{relay.Key}: {relay.Value:F2}");
        }
    }


    // Action: Release Frequency
    private void ReleaseFrequency()
    {
        Debug.Log($"{Language_Extension_000_2.RELEASE}, {Language_Extension_000_2.FREQUENCY}, {Language_Extension_000_2.SYSTEM}");
        float totalFrequency = 0;
        foreach (var value in relayValues.Values)
        {
            totalFrequency += value;
        }
        Debug.Log($"Total Released Frequency: {totalFrequency:F2}");
        if (totalFrequency > 10.0f)
        {
            Debug.Log("Frequency release successful.");
        }
        else
        {
            Debug.LogWarning("Frequency release insufficient.");
        }
    }

    // Action: Adjust Delay
    private void AdjustDelay()
    {
        Debug.Log($"{Language_Extension_000_2.ADJUST}, {Language_Extension_001_2.MENTAL}, {Language_Extension_001_2.DELAY}");

        // Create a copy of the keys to avoid modifying the dictionary while iterating
        var keys = new List<string>(relayValues.Keys);

        foreach (var key in keys)
        {
            relayValues[key] += delayAdjustment; // Add delay adjustment
        }

        Debug.Log("Relay values after delay adjustment:");
        foreach (var relay in relayValues)
        {
            Debug.Log($"{relay.Key}: {relay.Value:F2}");
        }
    }


    // Action: Activate User Interface
    private void ActivateUserInterface()
    {
        Debug.Log($"{Language_Extension_000_2.ACTIVATE}, {Language_Extension_000_2.USERINTERFACE}");
        if (mainframeEnergy > 50.0f)
        {
            Debug.Log("User interface successfully activated.");
        }
        else
        {
            Debug.LogWarning("Insufficient energy to activate user interface.");
        }
    }

    // Action: Final Activation
    private void FinalActivation()
    {
        Debug.Log($"{Language_Extension_000_2.ACTIVATE}, {Language_Extension_000_2.INTERFACE}");
        if (isSystemActivated)
        {
            Debug.Log("Final activation and command delegation successful.");
        }
        else
        {
            Debug.LogWarning("Final activation failed. System is not fully operational.");
        }
    }
}
