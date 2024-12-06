using UnityEngine;

namespace edgelorereality
{
    public class MatrixSimulator : MonoBehaviour
    {
        // Represents a user's state within the simulator
        private class SimulationUserState
        {
            public string UserID { get; private set; }
            public bool IsSimulated { get; private set; }
            public float SleepStage { get; private set; }
            public bool IsHallucinogenicControl { get; private set; }

            public SimulationUserState(string userId)
            {
                UserID = userId;
                IsSimulated = false;
                SleepStage = 0f;
                IsHallucinogenicControl = false;
            }

            public void EnterSimulation(float sleepStage)
            {
                IsSimulated = true;
                SleepStage = sleepStage;
                IsHallucinogenicControl = sleepStage >= 3f; // Activate hallucinogens in stage 3+
                Debug.Log($"User '{UserID}' entered simulation at sleep stage {sleepStage}.");
            }

            public void ExitSimulation()
            {
                IsSimulated = false;
                SleepStage = 0f;
                IsHallucinogenicControl = false;
                Debug.Log($"User '{UserID}' exited the simulation.");
            }
        }

        // Handles sensory feed simulation
        private void SimulateSensoryFeed(SimulationUserState user)
        {
            if (!user.IsSimulated)
            {
                Debug.LogWarning($"User '{user.UserID}' is not in simulation. Sensory feed skipped.");
                return;
            }

            Debug.Log($"Simulating sensory feed for User '{user.UserID}'...");
            if (user.IsHallucinogenicControl)
            {
                Debug.Log($"User '{user.UserID}' is under hallucinogenic control. Sensory feed intensified.");
            }
            else
            {
                Debug.Log($"User '{user.UserID}' is in normal sensory feed simulation.");
            }
        }

        // Integrate with Matrix Emulator for enhanced effects
        private void IntegrateWithEmulator(SimulationUserState user)
        {
            if (user.IsSimulated)
            {
                Debug.Log($"Integrating simulation effects with Matrix Emulator for User '{user.UserID}'...");
                float enhancedEffects = EnhanceEffects(user.SleepStage);
                Debug.Log($"Enhanced effects level: {enhancedEffects}");
            }
        }

        // Enhance effects based on sleep stage
        private float EnhanceEffects(float sleepStage)
        {
            return sleepStage * 1.5f; // Example enhancement factor
        }

        // User state
        private SimulationUserState userState;

        private void Start()
        {
            Debug.Log("Matrix Simulator Initialized.");

            // Initialize user state
            userState = new SimulationUserState("User1");

            // Automate simulation cycles
            InvokeRepeating(nameof(SimulateEntry), 1f, 10f); // Every 10 seconds, enter simulation
            InvokeRepeating(nameof(SimulateExit), 6f, 10f);  // Every 10 seconds, exit simulation (delayed to avoid overlap)
        }

        // Simulate user entry into the simulation
        private void SimulateEntry()
        {
            float sleepStage = Random.Range(1f, 4f); // Random sleep stage (1 to 4)
            userState.EnterSimulation(sleepStage);
            SimulateSensoryFeed(userState);
            IntegrateWithEmulator(userState);
        }

        // Simulate user exit from the simulation
        private void SimulateExit()
        {
            if (userState.IsSimulated)
            {
                userState.ExitSimulation();
            }
        }
    }
}
