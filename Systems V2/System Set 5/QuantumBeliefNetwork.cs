using System.Runtime.InteropServices;
using UnityEngine;
public class QuantumBeliefNetwork : MonoBehaviour
{
    [DllImport("QATF_Plugin")]
    private static extern void ForwardPropagation(float[] inputs, int inputSize, float[] outputs, int outputSize);

    void Start()
    {
        float[] inputs = new float[256];
        float[] outputs = new float[128];

        // Call CUDA forward propagation
        ForwardPropagation(inputs, inputs.Length, outputs, outputs.Length);
    }
}
