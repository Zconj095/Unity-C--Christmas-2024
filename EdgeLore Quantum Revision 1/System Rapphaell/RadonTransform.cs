using UnityEngine;

public class RadonTransform : MonoBehaviour
{
    public Vector3 LeftState;
    public Vector3 RightState;

    public Vector3 ComputeRadonTransform()
    {
        Debug.Log($"Computing Radon Transform with Left: {LeftState}, Right: {RightState}");
        Vector3 radonState = (LeftState + RightState) / 2.0f;
        Debug.Log($"Radon Transform Result: {radonState}");
        return radonState;
    }

}
