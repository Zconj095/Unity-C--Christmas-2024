using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixCoreSystem : MonoBehaviour
    {
        // Represents a subsystem linked to the core
        private class Subsystem
        {
            public string Name { get; private set; }
            public bool IsActive { get; private set; }

            public Subsystem(string name, bool isActive)
            {
                Name = name;
                IsActive = isActive;
            }

            public void ToggleActivation()
            {
                IsActive = !IsActive;
                Debug.Log($"Subsystem '{Name}' is now {(IsActive ? "Active" : "Inactive")}.");
            }
        }

        // Stores linked subsystems
        private Dictionary<string, Subsystem> linkedSubsystems = new Dictionary<string, Subsystem>();

        // Sensory input network (terasight)
        private Dictionary<string, float> sensoryInputs = new Dictionary<string, float>
        {
            { "Visual", 0f },
            { "Auditory", 0f },
            { "Tactile", 0f },
            { "Olfactory", 0f },
            { "Gustatory", 0f }
        };

        // Central formulas for frequency chamber operations
        private List<string> formulas = new List<string>();

        // Timers for automation
        private float addSubsystemTimer = 0f;
        private float toggleSubsystemTimer = 0f;
        private float updateSensoryInputTimer = 0f;
        private float addFormulaTimer = 0f;
        private float executeFormulasTimer = 0f;

        // Automation intervals
        private const float addSubsystemInterval = 5f; // Add a subsystem every 5 seconds
        private const float toggleSubsystemInterval = 7f; // Toggle a subsystem every 7 seconds
        private const float updateSensoryInputInterval = 3f; // Update sensory inputs every 3 seconds
        private const float addFormulaInterval = 6f; // Add a formula every 6 seconds
        private const float executeFormulasInterval = 10f; // Execute formulas every 10 seconds

        // Add a subsystem
        public void AddSubsystem(string name)
        {
            if (!linkedSubsystems.ContainsKey(name))
            {
                linkedSubsystems[name] = new Subsystem(name, false);
                Debug.Log($"Subsystem '{name}' linked to the Matrix Core.");
            }
            else
            {
                Debug.LogWarning($"Subsystem '{name}' is already linked.");
            }
        }

        // Toggle a subsystem's activation
        public void ToggleSubsystem(string name)
        {
            if (linkedSubsystems.TryGetValue(name, out Subsystem subsystem))
            {
                subsystem.ToggleActivation();
            }
            else
            {
                Debug.LogWarning($"Subsystem '{name}' not found.");
            }
        }

        // Update sensory input
        public void UpdateSensoryInput(string inputType, float value)
        {
            if (sensoryInputs.ContainsKey(inputType))
            {
                sensoryInputs[inputType] = Mathf.Clamp(value, 0f, 100f);
                Debug.Log($"Sensory input '{inputType}' updated to {value}%.");
            }
            else
            {
                Debug.LogWarning($"Invalid sensory input type: '{inputType}'.");
            }
        }

        // Add a formula to the frequency chamber
        public void AddFormula(string formula)
        {
            formulas.Add(formula);
            Debug.Log($"Formula added to frequency chamber: {formula}");
        }

        // Execute formulas
        public void ExecuteFormulas()
        {
            Debug.Log("Executing formulas in the frequency chamber...");
            foreach (var formula in formulas)
            {
                Debug.Log($"Executing Formula: {formula}");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Core System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addSubsystemTimer += Time.deltaTime;
            toggleSubsystemTimer += Time.deltaTime;
            updateSensoryInputTimer += Time.deltaTime;
            addFormulaTimer += Time.deltaTime;
            executeFormulasTimer += Time.deltaTime;

            // Automate adding subsystems
            if (addSubsystemTimer >= addSubsystemInterval)
            {
                AddSubsystem($"Subsystem_{linkedSubsystems.Count + 1}");
                addSubsystemTimer = 0f;
            }

            // Automate toggling subsystems
            if (toggleSubsystemTimer >= toggleSubsystemInterval && linkedSubsystems.Count > 0)
            {
                foreach (var subsystem in linkedSubsystems.Values)
                {
                    subsystem.ToggleActivation();
                    break; // Toggle one subsystem at a time
                }
                toggleSubsystemTimer = 0f;
            }

            // Automate updating sensory inputs
            if (updateSensoryInputTimer >= updateSensoryInputInterval)
            {
                string[] inputTypes = { "Visual", "Auditory", "Tactile", "Olfactory", "Gustatory" };
                string randomInput = inputTypes[Random.Range(0, inputTypes.Length)];
                UpdateSensoryInput(randomInput, Random.Range(10f, 100f));
                updateSensoryInputTimer = 0f;
            }

            // Automate adding formulas
            if (addFormulaTimer >= addFormulaInterval)
            {
                AddFormula($"Formula_{formulas.Count + 1}: RandomOperation");
                addFormulaTimer = 0f;
            }

            // Automate executing formulas
            if (executeFormulasTimer >= executeFormulasInterval)
            {
                ExecuteFormulas();
                executeFormulasTimer = 0f;
            }
        }
    }
}
