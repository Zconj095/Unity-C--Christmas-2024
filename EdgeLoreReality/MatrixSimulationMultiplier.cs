using UnityEngine;

namespace edgelorereality
{
    public class MatrixSimulationMultiplier : MonoBehaviour
    {
        // Frequency simulation parameters
        private double[] frequencies = { 50.123, 76.543, 89.876, 101.234 }; // Default frequencies
        private int numericValue = 5; // Default numeric value
        private float simulationInterval = 3f; // Interval between simulations in seconds
        private float timer = 0f; // Timer to track simulation interval

        // Calculate the multiplication formula
        public double CalculateMultiplier(double frequency, int numericValue)
        {
            // Step-by-step calculations
            double step1 = frequency + numericValue;
            double step2 = step1 * 0.66781;
            double step3 = step2 / 72.8391;
            double step4 = Mathf.Pow((float)step3, 2); // Square
            double step5 = Mathf.Pow((float)step4, 3); // Cube of the square
            double step6 = System.Math.Round(step5, 1); // Round to 10th of a decimal
            double step7 = (step6 * 100) - 1; // Decimal shift and subtract 1
            double finalResult = step7 + 62; // Add 62 for final result

            // Debugging output for traceability
            Debug.Log($"Multiplier Calculation for Frequency {frequency}:");
            Debug.Log($"Step 1 (Add): {step1}");
            Debug.Log($"Step 2 (Multiply by 0.66781): {step2}");
            Debug.Log($"Step 3 (Divide by 72.8391): {step3}");
            Debug.Log($"Step 4 (Square): {step4}");
            Debug.Log($"Step 5 (Cube): {step5}");
            Debug.Log($"Step 6 (Round): {step6}");
            Debug.Log($"Step 7 (Decimal Shift and Subtract 1): {step7}");
            Debug.Log($"Final Result (Add 62): {finalResult}");

            return finalResult;
        }

        // Simulate multiple frequency calculations
        public void SimulateFrequencies()
        {
            Debug.Log("Starting Simulation for Multiple Frequencies...");
            foreach (var frequency in frequencies)
            {
                double result = CalculateMultiplier(frequency, numericValue);
                Debug.Log($"Frequency: {frequency}, Result: {result}");
            }
        }

        // Update numeric value dynamically
        private void UpdateNumericValue()
        {
            numericValue = Random.Range(1, 10);
            Debug.Log($"Numeric Value Dynamically Updated: {numericValue}");
        }

        // Update frequencies dynamically
        private void UpdateFrequencies()
        {
            frequencies = new double[]
            {
                Random.Range(40f, 120f), 
                Random.Range(40f, 120f), 
                Random.Range(40f, 120f), 
                Random.Range(40f, 120f)
            };

            Debug.Log("Frequencies Dynamically Updated:");
            foreach (var freq in frequencies)
            {
                Debug.Log($"Frequency: {freq}");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Simulation Multiplier Initialized.");
            SimulateFrequencies(); // Initial simulation on start
        }

        private void Update()
        {
            // Automated simulation at intervals
            timer += Time.deltaTime;
            if (timer >= simulationInterval)
            {
                Debug.Log($"Automated Simulation Triggered at Interval: {simulationInterval}s");

                // Dynamically update numeric value and frequencies
                UpdateNumericValue();
                UpdateFrequencies();

                // Perform the simulation with updated values
                SimulateFrequencies();
                timer = 0f; // Reset timer
            }
        }
    }
}
