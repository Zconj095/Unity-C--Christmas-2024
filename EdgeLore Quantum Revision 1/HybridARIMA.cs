using System.Collections.Generic;
using UnityEngine;

public class HybridARIMA : MonoBehaviour
{
    // ARIMA parameter ranges
    public int minP = 0, maxP = 5;
    public int minD = 0, maxD = 2;
    public int minQ = 0, maxQ = 5;

    // Quantum parameter ranges
    public float minAmplitude = 0.1f, maxAmplitude = 1.0f;
    public int minIterations = 1, maxIterations = 20;

    // Time series data
    public List<float> timeSeries = new List<float> { 1f, 2f, 3f, 5f, 8f, 13f, 21f };
    public List<float> actualData; // Ground truth for evaluation

    // Best model parameters
    private int bestP, bestD, bestQ;
    private float bestAmplitude;
    private int bestIterations;
    private float bestError = float.MaxValue;

    void Start()
    {
        Debug.Log("Starting Hybrid Model Optimization...");

        // Perform search
        PerformGridSearch();
        PerformGeneticAlgorithm(20, 50); // Population size: 20, Generations: 50
        PerformRandomSearch(100); // Perform 100 random iterations


        // Output best model parameters
        Debug.Log($"Best Parameters: p={bestP}, d={bestD}, q={bestQ}, amplitude={bestAmplitude}, iterations={bestIterations}");
    }

    private void PerformGridSearch()
    {
        for (int p = minP; p <= maxP; p++)
        {
            for (int d = minD; d <= maxD; d++)
            {
                for (int q = minQ; q <= maxQ; q++)
                {
                    for (float amplitude = minAmplitude; amplitude <= maxAmplitude; amplitude += 0.1f)
                    {
                        for (int iterations = minIterations; iterations <= maxIterations; iterations++)
                        {
                            // Generate predictions
                            List<float> predictions = GeneratePredictions(p, d, q, amplitude, iterations);

                            // Evaluate predictions
                            float error = EvaluateModel(actualData, predictions);

                            // Update best model if current is better
                            if (error < bestError)
                            {
                                bestError = error;
                                bestP = p;
                                bestD = d;
                                bestQ = q;
                                bestAmplitude = amplitude;
                                bestIterations = iterations;
                            }
                        }
                    }
                }
            }
        }
    }


    private List<float> GeneratePredictions(int p, int d, int q, float amplitude, int iterations)
    {
        // Example stub: Ensure this function is implemented correctly
        List<float> data = PerformDifferencing(timeSeries, d);
        return QuantumARIMAPredict(data, p, q, amplitude, iterations);
    }

    private List<float> PerformDifferencing(List<float> data, int degree)
    {
        List<float> result = new List<float>(data);
        for (int i = 0; i < degree; i++)
        {
            for (int j = result.Count - 1; j > 0; j--)
            {
                result[j] -= result[j - 1];
            }
            result.RemoveAt(0);
        }
        return result;
    }

    private List<float> QuantumARIMAPredict(List<float> data, int arOrder, int maOrder, float amplitude, int iterations)
    {
        List<float> forecast = new List<float>();

        for (int i = 0; i < data.Count + arOrder; i++)
        {
            float arComponent = ARComponent(data, forecast, arOrder);
            float maComponent = MAComponent(data, forecast, maOrder);
            float quantumAdjustment = SimulateQuantumAdjustment(amplitude, iterations);

            forecast.Add(arComponent + maComponent + quantumAdjustment);
        }

        return forecast;
    }

    private float ARComponent(List<float> data, List<float> forecast, int order)
    {
        float result = 0f;
        for (int i = 1; i <= order; i++)
        {
            if (forecast.Count - i >= 0)
                result += 0.5f * forecast[forecast.Count - i]; // Simple AR weights
        }
        return result;
    }

    private float MAComponent(List<float> data, List<float> forecast, int order)
    {
        float result = 0f;
        for (int i = 1; i <= order; i++)
        {
            if (data.Count - i >= 0)
                result += 0.3f * data[data.Count - i]; // Simple MA weights
        }
        return result;
    }

    private float SimulateQuantumAdjustment(float amplitude, int iterations)
    {
        float result = 0f;
        for (int i = 0; i < iterations; i++)
        {
            float phase = Random.Range(0f, Mathf.PI * 2f);
            result += amplitude * Mathf.Sin(phase); // Simulated interference
        }
        return result / iterations; // Average quantum effect
    }


    private float EvaluateModel(List<float> actual, List<float> predicted)
    {
    float error = 0f;
    for (int i = 0; i < actual.Count; i++)
    {
        error += Mathf.Pow(actual[i] - predicted[i], 2); // Ensure 'actual' and 'predicted' are valid
    }
    return error / actual.Count; // Mean Squared Error
    }

    private void PerformRandomSearch(int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            // Randomly sample parameters
            int p = Random.Range(minP, maxP + 1);
            int d = Random.Range(minD, maxD + 1);
            int q = Random.Range(minQ, maxQ + 1);
            float amplitude = Random.Range(minAmplitude, maxAmplitude);
            int quantumIters = Random.Range(minIterations, maxIterations + 1);

            // Generate predictions and evaluate
            List<float> predictions = GeneratePredictions(p, d, q, amplitude, quantumIters);
            float error = EvaluateModel(actualData, predictions); // Line around 294


            // Update best model
            if (error < bestError)
            {
                bestError = error;
                bestP = p;
                bestD = d;
                bestQ = q;
                bestAmplitude = amplitude;
                bestIterations = quantumIters;
                Debug.Log($"New Best Found: p={p}, d={d}, q={q}, amplitude={amplitude}, iterations={quantumIters}, error={error}");
            }
        }
    }


    private struct Candidate
    {
        public int p, d, q;
        public float amplitude;
        public int iterations;
        public float error;

        public Candidate(int p, int d, int q, float amplitude, int iterations, float error)
        {
            this.p = p;
            this.d = d;
            this.q = q;
            this.amplitude = amplitude;
            this.iterations = iterations;
            this.error = error;
        }
    }

    private List<Candidate> InitializePopulation(int populationSize)
    {
        List<Candidate> population = new List<Candidate>();
        for (int i = 0; i < populationSize; i++)
        {
            int p = Random.Range(minP, maxP + 1);
            int d = Random.Range(minD, maxD + 1);
            int q = Random.Range(minQ, maxQ + 1);
            float amplitude = Random.Range(minAmplitude, maxAmplitude);
            int quantumIters = Random.Range(minIterations, maxIterations + 1);
            List<float> predictions = GeneratePredictions(p, d, q, amplitude, quantumIters);
            float error = EvaluateModel(actualData, predictions);

            population.Add(new Candidate(p, d, q, amplitude, quantumIters, error));
        }
        return population;
    }

    private Candidate Crossover(Candidate parent1, Candidate parent2)
    {
        int p = Random.value > 0.5f ? parent1.p : parent2.p;
        int d = Random.value > 0.5f ? parent1.d : parent2.d;
        int q = Random.value > 0.5f ? parent1.q : parent2.q;
        float amplitude = Random.value > 0.5f ? parent1.amplitude : parent2.amplitude;
        int quantumIters = Random.value > 0.5f ? parent1.iterations : parent2.iterations;

        List<float> predictions = GeneratePredictions(p, d, q, amplitude, quantumIters);
        float error = EvaluateModel(actualData, predictions);

        return new Candidate(p, d, q, amplitude, quantumIters, error);
    }

    private Candidate Mutate(Candidate candidate)
    {
        int p = Mathf.Clamp(candidate.p + Random.Range(-1, 2), minP, maxP);
        int d = Mathf.Clamp(candidate.d + Random.Range(-1, 2), minD, maxD);
        int q = Mathf.Clamp(candidate.q + Random.Range(-1, 2), minQ, maxQ);
        float amplitude = Mathf.Clamp(candidate.amplitude + Random.Range(-0.1f, 0.1f), minAmplitude, maxAmplitude);
        int quantumIters = Mathf.Clamp(candidate.iterations + Random.Range(-1, 2), minIterations, maxIterations);

        List<float> predictions = GeneratePredictions(p, d, q, amplitude, quantumIters);
        float error = EvaluateModel(actualData, predictions);

        return new Candidate(p, d, q, amplitude, quantumIters, error);
    }

    private void PerformGeneticAlgorithm(int populationSize, int generations)
    {
        List<Candidate> population = InitializePopulation(populationSize);

        for (int generation = 0; generation < generations; generation++)
        {
            // Sort population by error
            population.Sort((a, b) => a.error.CompareTo(b.error));

            // Select top candidates for next generation
            List<Candidate> newPopulation = new List<Candidate>();
            for (int i = 0; i < populationSize / 2; i++)
            {
                newPopulation.Add(population[i]);
            }

            // Perform crossover and mutation
            for (int i = 0; i < populationSize / 2; i++)
            {
                Candidate parent1 = population[Random.Range(0, populationSize / 2)];
                Candidate parent2 = population[Random.Range(0, populationSize / 2)];

                Candidate offspring = Crossover(parent1, parent2);
                offspring = Mutate(offspring);

                newPopulation.Add(offspring);
            }

            population = newPopulation;

            // Log best candidate of the generation
            Debug.Log($"Generation {generation}: Best Error = {population[0].error}");
        }

        // Update best model
        Candidate bestCandidate = population[0];
        bestP = bestCandidate.p;
        bestD = bestCandidate.d;
        bestQ = bestCandidate.q;
        bestAmplitude = bestCandidate.amplitude;
        bestIterations = bestCandidate.iterations;
        bestError = bestCandidate.error;

        Debug.Log($"Best Candidate: p={bestP}, d={bestD}, q={bestQ}, amplitude={bestAmplitude}, iterations={bestIterations}, error={bestError}");
    }

    private void VisualizeProgress(int generation, float bestError)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(generation, bestError, 0);
        cube.transform.localScale = Vector3.one * 0.5f;

        Material mat = new Material(Shader.Find("Standard"));
        mat.color = Color.Lerp(Color.red, Color.green, 1f - (bestError / 100f)); // Green for lower error
        cube.GetComponent<Renderer>().material = mat;
    }

    private void ExportResults()
    {
        string exportPath = "Assets/Data/best_model.txt";
        string result = $"p={bestP}, d={bestD}, q={bestQ}, amplitude={bestAmplitude}, iterations={bestIterations}, error={bestError}";
        System.IO.File.WriteAllText(exportPath, result);
        Debug.Log($"Best model exported to: {exportPath}");
    }

}
