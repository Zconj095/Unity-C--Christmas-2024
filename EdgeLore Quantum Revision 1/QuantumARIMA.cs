using System.Collections.Generic;
using UnityEngine;

public class QuantumARIMA : MonoBehaviour
{
    // Parameters for ARIMA
    public int p = 2;  // AR order
    public int d = 1;  // Differencing degree
    public int q = 2;  // MA order

    // Time series data
    public List<float> timeSeries = new List<float> { 1f, 2f, 3f, 5f, 8f, 13f, 21f };

    // Predicted values
    private List<float> predictions = new List<float>();

    // Quantum-inspired parameters
    public float quantumAmplitude = 0.5f;  // Influence strength
    public int quantumIterations = 10;    // Simulated steps

    void Start()
    {
        Debug.Log("Starting Quantum ARIMA...");

        // Step 1: Perform Differencing
        List<float> differencedData = PerformDifferencing(timeSeries, d);

        // Step 2: Apply ARIMA with Quantum Adjustments
        predictions = QuantumARIMAPredict(differencedData, p, q);

        // Step 3: Visualize Predictions
        VisualizePredictions();
    }

    void Update()
    {
        // Use keyboard input to modify ARIMA parameters dynamically
        if (Input.GetKeyDown(KeyCode.UpArrow)) p++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) p = Mathf.Max(0, p - 1);
        if (Input.GetKeyDown(KeyCode.RightArrow)) q++;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) q = Mathf.Max(0, q - 1);

        if (Input.GetKeyDown(KeyCode.W)) quantumAmplitude += 0.1f;
        if (Input.GetKeyDown(KeyCode.S)) quantumAmplitude = Mathf.Max(0.1f, quantumAmplitude - 0.1f);

        // Recalculate and visualize predictions if parameters change
        if (Input.anyKeyDown)
        {
            predictions = QuantumARIMAPredict(PerformDifferencing(timeSeries, d), p, q);
            VisualizePredictions();
        }
    }


    private List<float> PerformDifferencing(List<float> data, int degree)
    {
        List<float> result = new List<float>(data);
        for (int i = 0; i < degree; i++)
        {
            for (int j = result.Count - 1; j > 0; j--)
            {
                result[j] -= result[j - 1];
            }
            result.RemoveAt(0);
        }
        return result;
    }

    private List<float> QuantumARIMAPredict(List<float> data, int arOrder, int maOrder)
    {
        List<float> forecast = new List<float>();

        for (int i = 0; i < data.Count + arOrder; i++)
        {
            float arComponent = ARComponent(data, forecast, arOrder);
            float maComponent = MAComponent(data, forecast, maOrder);
            float quantumAdjustment = SimulateQuantumAdjustment();

            forecast.Add(arComponent + maComponent + quantumAdjustment);
        }

        return forecast;
    }

    private float ARComponent(List<float> data, List<float> forecast, int order)
    {
        float result = 0f;
        for (int i = 1; i <= order; i++)
        {
            if (forecast.Count - i >= 0)
                result += 0.5f * forecast[forecast.Count - i]; // Simple AR weights
        }
        return result;
    }

    private float MAComponent(List<float> data, List<float> forecast, int order)
    {
        float result = 0f;
        for (int i = 1; i <= order; i++)
        {
            if (data.Count - i >= 0)
                result += 0.3f * data[data.Count - i]; // Simple MA weights
        }
        return result;
    }

    private float SimulateQuantumAdjustment()
    {
        float result = 0f;
        for (int i = 0; i < quantumIterations; i++)
        {
            float phase = Random.Range(0f, Mathf.PI * 2f);
            result += quantumAmplitude * Mathf.Sin(phase); // Simulated interference
        }
        return result / quantumIterations; // Average quantum effect
    }

    private void VisualizePredictions()
    {
        float startX = -timeSeries.Count / 2f;
        for (int i = 0; i < predictions.Count; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(startX + i, predictions[i], 0);
            sphere.transform.localScale = Vector3.one * 0.2f;
        }
    }

    private void VisualizeQuantumEffect(float adjustment, Vector3 position)
    {
        GameObject particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        particle.transform.position = position + new Vector3(0, adjustment, 0);
        particle.transform.localScale = Vector3.one * 0.1f;

        Material mat = new Material(Shader.Find("Standard"));
        mat.color = Color.blue; // Represent quantum influence
        particle.GetComponent<Renderer>().material = mat;

        Destroy(particle, 1f); // Remove after 1 second
    }

    public string filePath = "Assets/Data/timeSeries.txt";

    void LoadTimeSeries()
    {
        if (System.IO.File.Exists(filePath))
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            timeSeries.Clear();
            foreach (string line in lines)
            {
                if (float.TryParse(line, out float value))
                    timeSeries.Add(value);
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    private float SimulateQuantumSuperposition(List<float> data)
    {
        float result = 0f;
        int n = data.Count;

        for (int i = 0; i < n; i++)
        {
            float phase = Random.Range(0f, Mathf.PI * 2f);
            result += quantumAmplitude * data[i] * Mathf.Cos(phase);
        }

        return result / n; // Weighted average with quantum interference
    }

    private void DrawGraph(List<float> data)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = data.Count;

        float startX = -data.Count / 2f;
        for (int i = 0; i < data.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(startX + i, data[i], 0));
        }

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.blue;
    }

    public string exportPath = "Assets/Data/predictions.txt";

    void ExportPredictions(List<float> predictions)
    {
        System.IO.File.WriteAllLines(exportPath, predictions.ConvertAll(p => p.ToString()).ToArray());
        Debug.Log("Predictions exported to: " + exportPath);
    }
}
