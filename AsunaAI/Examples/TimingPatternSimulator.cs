using UnityEngine;
using System.Collections.Generic;

public class TimingPatternSimulator : MonoBehaviour
{
    private class TimingPattern
    {
        public string PatternName;
        public float Duration;
        public float ElapsedTime;
        public bool IsActive;

        public TimingPattern(string patternName, float duration)
        {
            PatternName = patternName;
            Duration = duration;
            ElapsedTime = 0f;
            IsActive = false;
        }
    }

    [Header("Timing Pattern Settings")]
    public string[] patternNames = { "PERIODIC", "SYNCHRONIZED", "RHYTHMIC", "INTERPOLATED" }; // Example pattern constants
    public float baseDuration = 5.0f; // Base duration for each pattern
    public float durationVariance = 2.0f; // Random variance for pattern durations

    private List<TimingPattern> timingPatterns = new List<TimingPattern>();
    private float globalTimer = 0f;

    void Start()
    {
        InitializePatterns();
    }

    void Update()
    {
        globalTimer += Time.deltaTime;

        foreach (var pattern in timingPatterns)
        {
            if (pattern.IsActive)
            {
                pattern.ElapsedTime += Time.deltaTime;

                if (pattern.ElapsedTime >= pattern.Duration)
                {
                    pattern.IsActive = false;
                    pattern.ElapsedTime = 0f;
                    Debug.Log($"{pattern.PatternName} pattern completed.");
                }
            }
        }

        // Check if it's time to activate a new pattern
        ActivatePatterns();
    }

    void InitializePatterns()
    {
        foreach (var name in patternNames)
        {
            float randomizedDuration = baseDuration + Random.Range(-durationVariance, durationVariance);
            timingPatterns.Add(new TimingPattern(name, Mathf.Max(1.0f, randomizedDuration)));
        }

        Debug.Log("Timing Patterns Initialized:");
        foreach (var pattern in timingPatterns)
        {
            Debug.Log($"{pattern.PatternName}: {pattern.Duration:F2}s");
        }
    }

    void ActivatePatterns()
    {
        foreach (var pattern in timingPatterns)
        {
            if (!pattern.IsActive && Random.value > 0.5f) // 50% chance to activate a pattern
            {
                pattern.IsActive = true;
                Debug.Log($"{pattern.PatternName} pattern activated for {pattern.Duration:F2}s.");
            }
        }
    }

    void DisplayPatternStates()
    {
        Debug.Log("Current Pattern States:");
        foreach (var pattern in timingPatterns)
        {
            Debug.Log($"{pattern.PatternName}: Active={pattern.IsActive}, ElapsedTime={pattern.ElapsedTime:F2}/{pattern.Duration:F2}");
        }
    }

}
