using UnityEngine;
using System.Collections;
using Enviro;

public class EnviroMeteorShower : MonoBehaviour
{
    [Header("Enviro Integration (Assign in Inspector)")]
    public EnviroSkyModule enviroSky;

    [Header("Meteor Shower Settings")]
    [Tooltip("Number of meteors in a single shower sequence.")]
    public int meteorCount = 20;

    [Tooltip("Duration of the entire meteor shower in seconds.")]
    public float showerDuration = 10f;

    [Tooltip("Range of meteor sizes (x = min, y = max).")]
    public Vector2 meteorSizeRange = new Vector2(0.5f, 2f);

    [Tooltip("Base downward speed of meteors.")]
    public float meteorSpeed = 10f;

    [Header("Meteor Trail Settings")]
    [Tooltip("Base lifetime of the trail in seconds.")]
    public float baseTrailTime = 2f;

    [Tooltip("Scale factor for trail width based on meteor size.")]
    public float trailWidthScale = 0.5f;

    [Tooltip("Control the alpha of trail start and end.")]
    [Range(0f, 1f)]
    public float startAlpha = 1f;
    [Range(0f, 1f)]
    public float endAlpha = 0f;

    [Tooltip("Use a gradient to create a more realistic meteor trail color.")]
    public Gradient meteorTrailColor;

    [Header("Meteor Material Settings")]
    [Tooltip("Material for the meteor's TrailRenderer.")]
    public Material trailMaterial;

    [Header("Spawn Settings")]
    [Tooltip("Distance from camera forward direction where meteors appear.")]
    public float spawnDistance = 50f;
    [Tooltip("Radius around camera to randomize spawn positions.")]
    public float spawnRadius = 100f;
    [Tooltip("Minimum height above some reference point to spawn meteor.")]
    public float minSpawnHeight = 300f;

    [Tooltip("How long before meteors vanish if they never hit terrain (since we are in the sky).")]
    public float meteorLifetime = 20f;

    [Header("Debug")]
    [Tooltip("Enable debug logs for additional info.")]
    public bool debugLogs = false;

    public GameObject meteorPrefab; // Assign your meteor prefab in the inspector

    private bool isShowerActive = false;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        if (enviroSky == null)
        {
            Debug.LogError("EnviroSkyMgr not assigned to EnviroMeteorShower script!");
            return;
        }

        // Start the meteor shower immediately if desired
        StartMeteorShower();
    }

    public void StartMeteorShower()
    {
        if (!isShowerActive && meteorPrefab != null)
        {
            isShowerActive = true;
            StartCoroutine(SpawnMeteors());
        }
        else if (meteorPrefab == null)
        {
            Debug.LogError("No meteorPrefab assigned!");
        }
    }

    private IEnumerator SpawnMeteors()
    {
        float delay = showerDuration / meteorCount;
        for (int i = 0; i < meteorCount; i++)
        {
            SpawnMeteor();
            yield return new WaitForSeconds(delay);
        }
        isShowerActive = false;
    }

    private void SpawnMeteor()
    {
        // Randomly select meteor size
        float meteorSize = Random.Range(meteorSizeRange.x, meteorSizeRange.y);

        // Compute spawn position:
        // We position meteors far above ground in the sky, away from terrain.
        // They appear around the camera, forward and at a high altitude.
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius
                                + mainCam.transform.position
                                + mainCam.transform.forward * spawnDistance;
        spawnPosition.y = Mathf.Max(spawnPosition.y, minSpawnHeight);

        // Create meteor instance
        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        meteor.transform.localScale = Vector3.one * meteorSize;

        // Add a Rigidbody with downward velocity to simulate falling from sky
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        if (rb == null) rb = meteor.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.linearVelocity = Vector3.down * meteorSpeed;

        // Add TrailRenderer with advanced settings
        TrailRenderer trail = meteor.GetComponent<TrailRenderer>();
        if (trail == null) trail = meteor.AddComponent<TrailRenderer>();

        trail.time = baseTrailTime;
        trail.startWidth = meteorSize * trailWidthScale;
        trail.endWidth = 0f;
        trail.material = (trailMaterial != null) ? trailMaterial : new Material(Shader.Find("Sprites/Default"));

        // If no gradient assigned, create a default fiery gradient
        if (meteorTrailColor == null || meteorTrailColor.colorKeys.Length == 0)
        {
            GradientColorKey[] colorKeys = new GradientColorKey[3];
            colorKeys[0].color = new Color(1f, 0.9f, 0.6f, startAlpha);
            colorKeys[0].time = 0f;
            colorKeys[1].color = new Color(1f, 0.5f, 0.0f, Mathf.Lerp(startAlpha, endAlpha, 0.5f));
            colorKeys[1].time = 0.5f;
            colorKeys[2].color = new Color(0.6f, 0.0f, 0.0f, endAlpha);
            colorKeys[2].time = 1f;

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[3];
            alphaKeys[0].alpha = startAlpha;
            alphaKeys[0].time = 0f;
            alphaKeys[1].alpha = (startAlpha + endAlpha) * 0.5f;
            alphaKeys[1].time = 0.5f;
            alphaKeys[2].alpha = endAlpha;
            alphaKeys[2].time = 1f;

            meteorTrailColor = new Gradient();
            meteorTrailColor.SetKeys(colorKeys, alphaKeys);
        }

        trail.colorGradient = meteorTrailColor;

        // Debug info
        if (debugLogs)
        {
            float luminousIntensity = ComputeLuminousIntensity(meteorSpeed, meteorSize);
            Debug.Log("Spawned sky meteor with size: " + meteorSize
                      + ", velocity: " + meteorSpeed
                      + ", luminous intensity: " + luminousIntensity);
        }

        // Add script to handle meteor lifetime and vanish in the sky
        MeteorVanish vanishScript = meteor.AddComponent<MeteorVanish>();
        vanishScript.meteorLifetime = meteorLifetime;
    }

    // Example luminous intensity function
    // L = L0 * (v^2 / v0^2) * (s / s0)
    private float ComputeLuminousIntensity(float velocity, float size)
    {
        float L0 = 1000f; // arbitrary reference luminous intensity
        float v0 = 10f;   // reference velocity
        float s0 = 1f;    // reference size
        float L = L0 * Mathf.Pow(velocity / v0, 2f) * (size / s0);
        return L;
    }
}

public class MeteorVanish : MonoBehaviour
{
    [Tooltip("How long in seconds before the meteor vanishes if not destroyed earlier.")]
    public float meteorLifetime = 20f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= meteorLifetime)
        {
            // Meteor simply vanishes in the sky
            Debug.Log("Meteor vanished in the sky after lifetime: " + meteorLifetime + "s");
            Destroy(gameObject);
        }
    }
}

/*
--------------------------------------------
Mathematical Equations Used:

1. Spawn Delay:
   Spawn Delay = showerDuration / meteorCount

2. Meteor Velocity (Downward):
   v = meteorSpeed * (0, -1, 0)
   The meteor falls straight down from a high point in the sky.

3. Luminous Intensity (Conceptual Model):
   L = L0 * (v^2 / v0^2) * (s / s0)
   Where:
     L0 = Reference luminous intensity (constant)
     v0 = Reference velocity (constant)
     s0 = Reference size (constant)
     v  = Actual meteor velocity magnitude
     s  = Actual meteor size

4. Random Spawning Region:
   spawnPosition = CameraPosition + CameraForward * spawnDistance 
                   + RandomInsideUnitSphere * spawnRadius
   With the constraint: spawnPosition.y >= minSpawnHeight
   Ensures meteor appears high in the sky.

5. Meteor Lifetime:
   If meteor hasn't collided or isn't destroyed by other means:
   Meteor is destroyed after meteorLifetime seconds.

6. Trail Renderer Gradient:
   The gradient defines color transition from bright (start) to darker (end):
   g(0) = bright warm color at startAlpha,
   g(0.5) = intermediate orange at mid alpha,
   g(1) = darker red at endAlpha.

No Canvas is used. Code is exposed in the inspector for adjustments. 
Console output is provided for debug purposes.
All parameters can be changed at runtime, ensuring dynamic updating code.
--------------------------------------------
*/
