using UnityEngine;

public class SystemSamSupportVectorMachine : MonoBehaviour
{
    public Vector3 TrainingData;
    public Vector3 InputData;

    public Vector3 Predict()
    {
        Debug.Log("Predicting using Support Vector Machine...");
        Vector3 prediction = TrainingData * Vector3.Dot(InputData, TrainingData);
        Debug.Log($"SVM Prediction: {prediction}");
        return prediction;
    }
}
