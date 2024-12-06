using UnityEngine;

public class SystemSamHiddenMarkovModel : MonoBehaviour
{
    public Vector3 HopfieldInput;
    public Vector3 SVMInput;

    public Vector3 ComputeState()
    {
        Debug.Log("Computing Hidden Markov Model state...");
        Vector3 combinedState = (HopfieldInput + SVMInput) / 2.0f;
        Debug.Log($"HMM State: {combinedState}");
        return combinedState;
    }
}
