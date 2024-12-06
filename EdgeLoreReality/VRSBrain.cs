using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class VRSBrain : MonoBehaviour
    {
        private class NeuralCircuit
        {
            public string CircuitID { get; private set; }
            public string Description { get; private set; }
            public bool IsActive { get; private set; }

            public NeuralCircuit(string description)
            {
                CircuitID = System.Guid.NewGuid().ToString();
                Description = description;
                IsActive = true;
            }

            public void ToggleCircuit(bool activate)
            {
                IsActive = activate;
                Debug.Log($"Circuit '{Description}' is now {(IsActive ? "Active" : "Inactive")}");
            }
        }

        private class SensoryInput
        {
            public string InputID { get; private set; }
            public string Type { get; private set; }
            public float Intensity { get; private set; }
            public bool IsProcessed { get; private set; }

            public SensoryInput(string type, float intensity)
            {
                InputID = System.Guid.NewGuid().ToString();
                Type = type;
                Intensity = Mathf.Clamp(intensity, 0f, 1f);
                IsProcessed = false;
            }

            public void ProcessInput()
            {
                IsProcessed = true;
                Debug.Log($"Sensory Input '{Type}' processed with intensity {Intensity}");
            }
        }

        private class Knowledge
        {
            public string KnowledgeID { get; private set; }
            public string Description { get; private set; }
            public bool IsUsedForDecision { get; private set; }

            public Knowledge(string description)
            {
                KnowledgeID = System.Guid.NewGuid().ToString();
                Description = description;
                IsUsedForDecision = false;
            }

            public void UseForDecision()
            {
                IsUsedForDecision = true;
                Debug.Log($"Knowledge '{Description}' used for decision-making.");
            }
        }

        private List<NeuralCircuit> neuralCircuits = new List<NeuralCircuit>();
        private List<SensoryInput> sensoryInputs = new List<SensoryInput>();
        private List<Knowledge> knowledgePackets = new List<Knowledge>();

        private void AddNeuralCircuit(string description)
        {
            NeuralCircuit circuit = new NeuralCircuit(description);
            neuralCircuits.Add(circuit);
            Debug.Log($"Neural Circuit Added: {description}");
        }

        private void ToggleRandomCircuit()
        {
            if (neuralCircuits.Count > 0)
            {
                int randomIndex = Random.Range(0, neuralCircuits.Count);
                NeuralCircuit circuit = neuralCircuits[randomIndex];
                circuit.ToggleCircuit(!circuit.IsActive);
            }
        }

        private void AddSensoryInput(string type, float intensity)
        {
            SensoryInput input = new SensoryInput(type, intensity);
            sensoryInputs.Add(input);
            Debug.Log($"Sensory Input Added: {type} with intensity {intensity}");
        }

        private void ProcessSensoryInputs()
        {
            Debug.Log("Processing all sensory inputs...");
            foreach (var input in sensoryInputs)
            {
                if (!input.IsProcessed)
                {
                    input.ProcessInput();
                }
            }
        }

        private void AddKnowledge(string description)
        {
            Knowledge knowledge = new Knowledge(description);
            knowledgePackets.Add(knowledge);
            Debug.Log($"Knowledge Added: {description}");
        }

        private void UseRandomKnowledgeForDecision()
        {
            if (knowledgePackets.Count > 0)
            {
                int randomIndex = Random.Range(0, knowledgePackets.Count);
                knowledgePackets[randomIndex].UseForDecision();
            }
        }

        private void DisplayBrainStatus()
        {
            Debug.Log("Displaying VR Species Brain Status:");

            Debug.Log("Neural Circuits:");
            foreach (var circuit in neuralCircuits)
            {
                Debug.Log($" - Circuit: {circuit.Description}, Active: {circuit.IsActive}");
            }

            Debug.Log("Sensory Inputs:");
            foreach (var input in sensoryInputs)
            {
                Debug.Log($" - Input: {input.Type}, Intensity: {input.Intensity}, Processed: {input.IsProcessed}");
            }

            Debug.Log("Knowledge Packets:");
            foreach (var knowledge in knowledgePackets)
            {
                Debug.Log($" - Knowledge: {knowledge.Description}, Used for Decision: {knowledge.IsUsedForDecision}");
            }
        }

        private void Start()
        {
            Debug.Log("VR Species Brain Initialized.");

            // Initial setup
            AddNeuralCircuit("Motor Function Circuit");
            AddNeuralCircuit("Sensory Feedback Circuit");

            AddSensoryInput("Vision", 0.8f);
            AddSensoryInput("Hearing", 0.5f);

            AddKnowledge("Survival Instincts");
            AddKnowledge("Learned Reflexes");

            // Schedule automated tasks
            InvokeRepeating(nameof(ProcessSensoryInputs), 2f, 5f); // Process sensory inputs every 5 seconds
            InvokeRepeating(nameof(ToggleRandomCircuit), 3f, 7f); // Toggle random circuit every 7 seconds
            InvokeRepeating(nameof(UseRandomKnowledgeForDecision), 4f, 6f); // Use random knowledge every 6 seconds
            InvokeRepeating(nameof(DisplayBrainStatus), 10f, 20f); // Display brain status every 20 seconds
        }
    }
}
