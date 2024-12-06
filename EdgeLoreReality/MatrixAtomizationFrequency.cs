using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixAtomizationFrequency : MonoBehaviour
    {
        // Represents an atom with a serial code and frequency
        private class Atom
        {
            public string SerialCode { get; private set; }
            public float Frequency { get; private set; }

            public Atom(string serialCode, float frequency)
            {
                SerialCode = serialCode;
                Frequency = frequency;
            }
        }

        // Data structure to store atoms
        private Dictionary<string, Atom> atomNetwork = new Dictionary<string, Atom>();

        // Timers for automation
        private float addAtomTimer = 0f;
        private float calibrateFluxTimer = 0f;
        private float getInfoTimer = 0f;

        // Automation intervals
        private const float addAtomInterval = 5f; // Add an atom every 5 seconds
        private const float calibrateFluxInterval = 10f; // Calibrate flux every 10 seconds
        private const float getInfoInterval = 7f; // Get info every 7 seconds

        // Generate a unique serial code
        private string GenerateSerialCode()
        {
            return System.Guid.NewGuid().ToString();
        }

        // Assign a frequency to an atom based on spatial-temporal flux calibration
        private float CalculateFrequency(string serialCode)
        {
            // Example: Simple frequency formula based on serial code hash
            int hash = serialCode.GetHashCode();
            float baseFrequency = 1.0f;
            float calibratedFrequency = baseFrequency + (hash % 100) / 10.0f;
            return Mathf.Abs(calibratedFrequency);
        }

        // Add a new atom to the network
        public void AddAtom()
        {
            string serialCode = GenerateSerialCode();
            float frequency = CalculateFrequency(serialCode);

            Atom atom = new Atom(serialCode, frequency);
            atomNetwork[serialCode] = atom;

            Debug.Log($"Added Atom - Serial Code: {serialCode}, Frequency: {frequency} Hz");
        }

        // Get atom information by serial code
        public void GetAtomInfo(string serialCode)
        {
            if (atomNetwork.TryGetValue(serialCode, out Atom atom))
            {
                Debug.Log($"Atom Info - Serial Code: {atom.SerialCode}, Frequency: {atom.Frequency} Hz");
            }
            else
            {
                Debug.LogWarning($"Atom with Serial Code '{serialCode}' not found.");
            }
        }

        // Get info for the last added atom
        public void GetLastAtomInfo()
        {
            if (atomNetwork.Count > 0)
            {
                string lastKey = "";
                foreach (var key in atomNetwork.Keys) lastKey = key; // Get last added key
                GetAtomInfo(lastKey);
            }
            else
            {
                Debug.LogWarning("No atoms in the network.");
            }
        }

        // Simulate spatial-temporal flux calibration
        public void CalibrateFlux()
        {
            Debug.Log("Calibrating spatial-temporal flux for all atoms...");
            foreach (var atom in atomNetwork.Values)
            {
                float recalibratedFrequency = atom.Frequency + Random.Range(-0.1f, 0.1f);
                Debug.Log($"Recalibrated Atom - Serial Code: {atom.SerialCode}, New Frequency: {recalibratedFrequency:F3} Hz");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Atomization Frequency System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addAtomTimer += Time.deltaTime;
            calibrateFluxTimer += Time.deltaTime;
            getInfoTimer += Time.deltaTime;

            // Automate adding atoms
            if (addAtomTimer >= addAtomInterval)
            {
                AddAtom();
                addAtomTimer = 0f;
            }

            // Automate calibrating flux
            if (calibrateFluxTimer >= calibrateFluxInterval)
            {
                CalibrateFlux();
                calibrateFluxTimer = 0f;
            }

            // Automate retrieving information for the last atom
            if (getInfoTimer >= getInfoInterval)
            {
                GetLastAtomInfo();
                getInfoTimer = 0f;
            }
        }
    }
}
