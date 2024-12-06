using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public class HypervectorCircuitParallel : MonoBehaviour
{
    [SerializeField] private int dimension = 1024; // Large hypervector dimension

    public void ExecuteHypervectorCircuit()
    {
        NativeArray<float> hypervector = new NativeArray<float>(dimension, Allocator.TempJob);
        HypervectorJob job = new HypervectorJob
        {
            Hypervector = hypervector
        };

        JobHandle handle = job.Schedule(dimension, 64); // Schedule with parallelization
        handle.Complete();

        Debug.Log("Hypervector Circuit execution completed in parallel.");

        hypervector.Dispose();
    }

    struct HypervectorJob : IJobParallelFor
    {
        public NativeArray<float> Hypervector;

        public void Execute(int index)
        {
            Hypervector[index] = Mathf.Sin(index * Mathf.PI / 180.0f); // Example computation
        }
    }
}
