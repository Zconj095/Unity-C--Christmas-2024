using System;
using System.Numerics;

public static class AddNoiseSIMD
{
    public static float[] AddNoise(float[] vector, float noiseLevel)
    {
        int length = vector.Length;
        float[] noisyVector = new float[length];
        var random = new Random();

        // Process in chunks equal to the size of the Vector<T>
        int simdLength = Vector<float>.Count;

        for (int i = 0; i < length; i += simdLength)
        {
            // Create random noise vector
            float[] noiseArray = new float[simdLength];
            for (int j = 0; j < simdLength; j++)
            {
                noiseArray[j] = (float)(random.NextDouble() * 2 - 1) * noiseLevel;
            }

            // Load the current chunk into Vector<T>
            Vector<float> dataVector = new Vector<float>(vector, i);
            Vector<float> noiseVector = new Vector<float>(noiseArray);

            // Apply noise
            Vector<float> resultVector = dataVector + noiseVector;

            // Store result back into the noisyVector array
            resultVector.CopyTo(noisyVector, i);
        }

        return noisyVector;
    }
}
