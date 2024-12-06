using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;

[BurstCompile]
public struct AddNoiseJobtoBoot : IJobParallelFor
{
    [ReadOnly] public NativeArray<float> OriginalValues;
    [WriteOnly] public NativeArray<float> NoisyValues;
    public float NoiseLevel;

    public void Execute(int index)
    {
        NoisyValues[index] = OriginalValues[index] + UnityEngine.Random.Range(-NoiseLevel, NoiseLevel);
    }
}
