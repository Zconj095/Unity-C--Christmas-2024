using System;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class SymmetryTransformation : MonoBehaviour
{
    [Header("Position Vector")]
    [SerializeField] private Vector3 originalPosition = new Vector3(1, 0, 0); // Original position vector (R)

    [Header("Rotation Matrix")]
    [SerializeField] private float angleDegrees = 45f; // Rotation angle in degrees
    [SerializeField] private Vector3 rotationAxis = new Vector3(0, 1, 0); // Rotation axis

    [Header("Results")]
    [SerializeField] private Vector3 transformedPosition; // Transformed position vector (R')

    void Start()
    {
        // Calculate the rotation matrix
        var rotationMatrix = CalculateRotationMatrix(rotationAxis, angleDegrees);

        // Transform the position vector
        transformedPosition = TransformPositionVector(originalPosition, rotationMatrix);

        // Log the result
        Debug.Log($"Original Position: {originalPosition}, Transformed Position: {transformedPosition}");
    }

    private Matrix<float> CalculateRotationMatrix(Vector3 axis, float angle)
    {
        // Normalize the rotation axis
        axis.Normalize();

        float radians = Mathf.Deg2Rad * angle;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        float oneMinusCos = 1.0f - cos;

        float x = axis.x;
        float y = axis.y;
        float z = axis.z;

        // Create the 3x3 rotation matrix
        var rotationMatrix = Matrix<float>.Build.DenseOfArray(new float[,]
        {
            { cos + x * x * oneMinusCos, x * y * oneMinusCos - z * sin, x * z * oneMinusCos + y * sin },
            { y * x * oneMinusCos + z * sin, cos + y * y * oneMinusCos, y * z * oneMinusCos - x * sin },
            { z * x * oneMinusCos - y * sin, z * y * oneMinusCos + x * sin, cos + z * z * oneMinusCos }
        });

        return rotationMatrix;
    }

    private Vector3 TransformPositionVector(Vector3 position, Matrix<float> rotationMatrix)
    {
        // Convert the position vector to a MathNet vector
        var positionVector = Vector<float>.Build.DenseOfArray(new float[] { position.x, position.y, position.z });

        // Apply the rotation matrix
        var transformedVector = rotationMatrix * positionVector;

        // Convert back to Unity Vector3
        return new Vector3(transformedVector[0], transformedVector[1], transformedVector[2]);
    }
}
