using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldOptimizer : MonoBehaviour
{
    public int populationSize = 50;
    public int maxGenerations = 100;
    public float mutationRate = 0.1f;

    private List<Individual> population;
    private int currentGeneration;

    void Start()
    {
        InitializePopulation();
        StartCoroutine(OptimizeYield());
    }

    void InitializePopulation()
    {
        population = new List<Individual>();
        for (int i = 0; i < populationSize; i++)
        {
            population.Add(new Individual(Random.Range(0f, 1f), Random.Range(0f, 1f)));
        }
    }

    float EvaluateYield(Individual ind)
    {
        // Example objective function: Maximize a quadratic yield
        return -(Mathf.Pow(ind.param1 - 0.5f, 2) + Mathf.Pow(ind.param2 - 0.5f, 2)) + 1;
    }

    IEnumerator OptimizeYield()
    {
        for (currentGeneration = 0; currentGeneration < maxGenerations; currentGeneration++)
        {
            foreach (var ind in population)
            {
                ind.fitness = EvaluateYield(ind);
            }

            population.Sort((a, b) => b.fitness.CompareTo(a.fitness));
            Debug.Log($"Generation {currentGeneration}: Best Yield: {population[0].fitness}");

            List<Individual> newPopulation = new List<Individual>();

            // Select top performers
            int eliteCount = populationSize / 4;
            newPopulation.AddRange(population.GetRange(0, eliteCount));

            // Crossover
            for (int i = eliteCount; i < populationSize; i++)
            {
                Individual parent1 = population[Random.Range(0, eliteCount)];
                Individual parent2 = population[Random.Range(0, eliteCount)];

                float newParam1 = (parent1.param1 + parent2.param1) / 2f;
                float newParam2 = (parent1.param2 + parent2.param2) / 2f;

                if (Random.value < mutationRate)
                {
                    newParam1 += Random.Range(-0.05f, 0.05f);
                    newParam2 += Random.Range(-0.05f, 0.05f);
                }

                newPopulation.Add(new Individual(newParam1, newParam2));
            }

            population = newPopulation;

            yield return null;
        }
    }

    public class Individual
    {
        public float param1, param2, fitness;

        public Individual(float param1, float param2)
        {
            this.param1 = param1;
            this.param2 = param2;
            this.fitness = 0f;
        }
    }
}
