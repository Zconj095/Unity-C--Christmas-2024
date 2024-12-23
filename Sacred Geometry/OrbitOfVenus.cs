using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitOfVenus : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab for circles
    public int numCircles = 5;      // Number of overlapping circles
    public float radius = 1f;       // Radius of each circle
    public float rotationAngle = 72f; // Angle between circles (360 / numCircles)

    void Start()
    {
        CreateOrbitPattern();
    }

    void CreateOrbitPattern()
    {
        for (int i = 0; i < numCircles; i++)
        {
            float angle = i * rotationAngle * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            Instantiate(circlePrefab, transform.position + position, Quaternion.identity, this.transform);
        }
    }
}
