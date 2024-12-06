using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GeneticAlgorithmManager : MonoBehaviour
{
    // Population size and genetic parameters
    public int populationSize = 100;
    public int chromosomeLength = 10;
    public float mutationRate = 0.1f;
    public float crossoverRate = 0.7f;
    public int maxGenerations = 50;

    // Feedback loop parameters
    public float diversityThreshold = 0.1f;
    public float fitnessImprovementThreshold = 0.01f;

    private List<float[]> population;
    private List<float> fitnessScores;
    private int generation;

    void Start()
    {
        InitializePopulation();
        RunEvolution();
    }

    void InitializePopulation()
    {
        population = new List<float[]>();
        for (int i = 0; i < populationSize; i++)
        {
            float[] chromosome = new float[chromosomeLength];
            for (int j = 0; j < chromosomeLength; j++)
            {
                chromosome[j] = Random.Range(-1f, 1f); // Initialize random genes
            }
            population.Add(chromosome);
        }
    }

    async void RunEvolution()
    {
        for (generation = 0; generation < maxGenerations; generation++)
        {
            // Step 1: Evaluate Fitness in Parallel
            fitnessScores = await EvaluateFitnessAsync(population);

            // Step 2: Feedback-Based Self-Organization
            AdjustParametersBasedOnFeedback();

            // Step 3: Selection and Crossover
            List<float[]> nextGeneration = PerformSelectionAndCrossover();

            // Step 4: Mutation
            PerformMutation(nextGeneration);

            // Update population
            population = nextGeneration;

            // Log current generation info
            Debug.Log($"Generation {generation}: Best Fitness = {fitnessScores.Max()}");
        }
    }

    async Task<List<float>> EvaluateFitnessAsync(List<float[]> population)
    {
        List<float> fitness = new List<float>();
        await Task.Run(() =>
        {
            foreach (var chromosome in population)
            {
                fitness.Add(FitnessFunction(chromosome));
            }
        });
        return fitness;
    }

    void AdjustParametersBasedOnFeedback()
    {
        float diversity = CalculatePopulationDiversity();
        float bestFitness = fitnessScores.Max();
        float averageFitness = fitnessScores.Average();

        if (diversity < diversityThreshold)
        {
            mutationRate += 0.01f; // Increase mutation for more exploration
            crossoverRate -= 0.01f;
        }
        else
        {
            mutationRate -= 0.01f; // Reduce mutation for refinement
            crossoverRate += 0.01f;
        }

        // Example adjustment for fitness stagnation
        if (bestFitness - averageFitness < fitnessImprovementThreshold)
        {
            mutationRate += 0.05f; // Escape stagnation
        }
    }

    List<float[]> PerformSelectionAndCrossover()
    {
        List<float[]> nextGeneration = new List<float[]>();
        while (nextGeneration.Count < populationSize)
        {
            // Select two parents
            float[] parent1 = SelectParent();
            float[] parent2 = SelectParent();

            // Perform crossover
            float[] child = Random.Range(0f, 1f) < crossoverRate ? Crossover(parent1, parent2) : parent1;
            nextGeneration.Add(child);
        }
        return nextGeneration;
    }

    void PerformMutation(List<float[]> nextGeneration)
    {
        foreach (var chromosome in nextGeneration)
        {
            for (int i = 0; i < chromosome.Length; i++)
            {
                if (Random.Range(0f, 1f) < mutationRate)
                {
                    chromosome[i] += Random.Range(-0.1f, 0.1f); // Add random noise
                }
            }
        }
    }

    float FitnessFunction(float[] chromosome)
    {
        // Example: Sum of squares (minimize the distance to zero vector)
        return chromosome.Sum(x => x * x);
    }

    float CalculatePopulationDiversity()
    {
        float[] meanVector = new float[chromosomeLength];
        foreach (var chromosome in population)
        {
            for (int i = 0; i < chromosomeLength; i++)
            {
                meanVector[i] += chromosome[i];
            }
        }
        for (int i = 0; i < meanVector.Length; i++) meanVector[i] /= population.Count;

        float diversity = 0f;
        foreach (var chromosome in population)
        {
            for (int i = 0; i < chromosomeLength; i++)
            {
                diversity += Mathf.Abs(chromosome[i] - meanVector[i]);
            }
        }
        return diversity / population.Count;
    }

    float[] SelectParent()
    {
        // Fitness-proportional selection
        float totalFitness = fitnessScores.Sum();
        float randomValue = Random.Range(0f, totalFitness);
        float cumulativeSum = 0f;
        for (int i = 0; i < population.Count; i++)
        {
            cumulativeSum += fitnessScores[i];
            if (cumulativeSum >= randomValue)
            {
                return population[i];
            }
        }
        return population[Random.Range(0, population.Count)]; // Fallback
    }

    float[] Crossover(float[] parent1, float[] parent2)
    {
        float[] child = new float[chromosomeLength];
        for (int i = 0; i < chromosomeLength; i++)
        {
            child[i] = Random.Range(0f, 1f) < 0.5f ? parent1[i] : parent2[i];
        }
        return child;
    }
}
