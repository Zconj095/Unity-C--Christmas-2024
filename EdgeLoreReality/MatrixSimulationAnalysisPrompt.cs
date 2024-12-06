using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSimulationAnalysisPrompt : MonoBehaviour
    {
        // Represents a dosage feed and its status
        private class DosageFeed
        {
            public string FeedID { get; private set; }
            public float FrequencyRating { get; private set; } // Frequency in Hz
            public bool IsSafe { get; private set; }

            public DosageFeed(float frequencyRating)
            {
                FeedID = System.Guid.NewGuid().ToString();
                FrequencyRating = frequencyRating;
                IsSafe = frequencyRating <= 76f; // Safe limit for frequency
            }
        }

        // List of analyzed dosage feeds
        private List<DosageFeed> dosageFeeds = new List<DosageFeed>();

        // Health protocol flags
        private bool healthProtocolActive = false;

        // Timers for automation
        private float feedAdditionTimer = 0f;
        private const float safeFeedInterval = 5f; // Interval for adding safe feeds
        private const float unsafeFeedInterval = 7f; // Interval for adding unsafe feeds

        // Add and analyze a dosage feed
        public void AddDosageFeed(float frequencyRating)
        {
            DosageFeed feed = new DosageFeed(frequencyRating);
            dosageFeeds.Add(feed);

            Debug.Log($"Dosage Feed Added - ID: {feed.FeedID}, Frequency: {frequencyRating} Hz, Safe: {feed.IsSafe}");
            AnalyzeFeed(feed);
        }

        // Analyze a single feed
        private void AnalyzeFeed(DosageFeed feed)
        {
            if (!feed.IsSafe)
            {
                Debug.LogWarning($"Unsafe dosage detected! Frequency: {feed.FrequencyRating} Hz.");
                ActivateHealthProtocol82();
            }
        }

        // Activate Health Protocol 82
        private void ActivateHealthProtocol82()
        {
            if (healthProtocolActive)
            {
                Debug.LogWarning("Health Protocol 82 is already active.");
                return;
            }

            Debug.Log("Activating Health Protocol 82...");
            healthProtocolActive = true;

            // Simulate Purification Frequency 76 Class A
            PurifySystem();
        }

        // Purify system using Purification Frequency 76
        private void PurifySystem()
        {
            Debug.Log("Initiating Purification Frequency 76 Class A...");
            dosageFeeds.Clear(); // Clear all feeds from the system
            Debug.Log("Purification complete. All dosage feeds wiped from the system for the last 72 hours.");

            // Simulate user ejection
            EjectUser();
        }

        // Eject the user from the system
        private void EjectUser()
        {
            Debug.LogWarning("User has been ejected from the system. Awaiting further notice from the HeadAdmin team.");
        }

        // Retrieve the status of all feeds
        public void RetrieveFeedStatus()
        {
            Debug.Log("Retrieving dosage feed statuses...");
            foreach (var feed in dosageFeeds)
            {
                Debug.Log($"Feed - ID: {feed.FeedID}, Frequency: {feed.FrequencyRating} Hz, Safe: {feed.IsSafe}");
            }

            if (dosageFeeds.Count == 0)
            {
                Debug.Log("No active dosage feeds in the system.");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Simulation Analysis Prompt Initialized.");
        }

        private void Update()
        {
            // Increment timers
            feedAdditionTimer += Time.deltaTime;

            // Automate safe feed addition
            if (feedAdditionTimer >= safeFeedInterval)
            {
                AddDosageFeed(Random.Range(50f, 75f)); // Safe frequency
                feedAdditionTimer = 0f; // Reset timer
            }

            // Automate unsafe feed addition (separate timer interval logic can be added if needed)
            if (feedAdditionTimer >= unsafeFeedInterval)
            {
                AddDosageFeed(Random.Range(77f, 100f)); // Unsafe frequency
                feedAdditionTimer = 0f; // Reset timer
            }
        }
    }
}
