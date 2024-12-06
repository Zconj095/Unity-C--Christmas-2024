using UnityEngine;

namespace edgelorereality
{
    public class MatrixSimulationSystem : MonoBehaviour
    {
        // Represents a user's state in the simulation
        private class UserState
        {
            public string UserID { get; private set; }
            public bool IsInSimulation { get; private set; }
            public float FrequencyChannel { get; private set; }

            public UserState(string userId)
            {
                UserID = userId;
                IsInSimulation = false;
                FrequencyChannel = 0f;
            }

            public void EnterSimulation(float frequency)
            {
                IsInSimulation = true;
                FrequencyChannel = frequency;
                Debug.Log($"User '{UserID}' entered simulation at frequency {frequency} Hz.");
            }

            public void ExitSimulation()
            {
                IsInSimulation = false;
                FrequencyChannel = 0f;
                Debug.Log($"User '{UserID}' exited the simulation.");
            }
        }

        // Simulates sound wave effects
        private void SimulateSoundWaveEffects(float frequency)
        {
            Debug.Log($"Simulating effects for frequency {frequency} Hz...");
            float chemicalEffect = CalculateChemicalEffect(frequency);
            Debug.Log($"Chemical effect level: {chemicalEffect}");
        }

        // Calculate chemical effects based on frequency
        private float CalculateChemicalEffect(float frequency)
        {
            // Example formula to determine effects
            return Mathf.Sin(frequency * Mathf.PI / 180f) * 100f;
        }

        // Countermeasures for unsafe frequencies
        private void ApplyCountermeasure(UserState user, float frequency)
        {
            if (frequency > 200f || frequency < 20f) // Example unsafe thresholds
            {
                Debug.LogWarning($"Unsafe frequency detected for User '{user.UserID}': {frequency} Hz. Applying countermeasure.");
                user.ExitSimulation();
            }
        }

        // User management
        private UserState userState;

        // Automation settings
        private float simulationInterval = 5f; // Time in seconds between each automatic update
        private float timer = 0f; // Timer to track intervals

        private void Start()
        {
            Debug.Log("Matrix Simulation System Initialized.");

            // Initialize user state
            userState = new UserState("User1");
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= simulationInterval)
            {
                timer = 0f; // Reset timer for the next interval

                if (!userState.IsInSimulation)
                {
                    // Automatically enter simulation with a random frequency
                    float frequency = Random.Range(15f, 250f); // Random frequency within range
                    userState.EnterSimulation(frequency);
                    SimulateSoundWaveEffects(frequency);
                    ApplyCountermeasure(userState, frequency);
                }
                else
                {
                    // Automatically exit simulation after processing
                    userState.ExitSimulation();
                }
            }
        }
    }
}
