class AddABitOfNoise
{
    public static void AddNoiseInPlace(float[] vector, float noiseLevel)
    {
        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] += UnityEngine.Random.Range(-noiseLevel, noiseLevel);
        }
    }
}