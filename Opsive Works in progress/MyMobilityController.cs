using UnityEngine;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;

public class MyMobilityController : MonoBehaviour
{
    // --------------------------------------------------
    // MOBILITY SETTINGS
    // --------------------------------------------------
    [Header("Base Mobility & Modifiers")]
    [Tooltip("Base Mobility value (e.g., 30).")]
    public int baseMobility = 30;

    [Tooltip("Movement speed gained per 1 Mobility.")]
    public float mobilitySpeedModifier = 0.1f;

    [Tooltip("Jump force gained per 1 Mobility.")]
    public float mobilityJumpModifier = 0.05f;

    // --------------------------------------------------
    // BASE CHARACTER VALUES
    // --------------------------------------------------
    [Header("Base Character Settings")]
    [Tooltip("Base movement speed, used for calculating final speed.")]
    public float baseMovementSpeed = 6f;

    [Tooltip("Base jump force, used for calculating final jump.")]
    public float baseJumpForce = 8f;

    // --------------------------------------------------
    // CURRENT STATS
    // --------------------------------------------------
    [Header("Current Mobility & Stats")]
    public int currentMobility;
    public float currentMovementSpeed;
    public float currentJumpForce;

    // --------------------------------------------------
    // ENCHANTMENTS
    // --------------------------------------------------
    [Header("Enchantments")]
    public bool skyRunnerUnlocked = false;
    public bool nightGliderUnlocked = false;

    // --------------------------------------------------
    // OPSIVE REFERENCES
    // --------------------------------------------------
    private UltimateCharacterLocomotion characterLocomotion;
    private SpeedChange speedChangeAbility; // For adjusting run speed
    private Jump jumpAbility;               // For adjusting jump force

    // --------------------------------------------------
    // INITIALIZATION
    // --------------------------------------------------
    private void Start()
    {
        // Initialize current Mobility
        currentMobility = baseMobility;

        // Reference to Opsive's character locomotion
        characterLocomotion = GetComponent<UltimateCharacterLocomotion>();
        // Fetch relevant abilities from Opsive
        speedChangeAbility = characterLocomotion.GetAbility<SpeedChange>();
        jumpAbility = characterLocomotion.GetAbility<Jump>();

        // Calculate initial stats
        CalculateStats();

        // Check for initial enchantments
        CheckEnchantments();
    }

    // --------------------------------------------------
    // STAT CALCULATIONS
    // --------------------------------------------------
    public void CalculateStats()
    {
        // Final movement speed
        currentMovementSpeed = baseMovementSpeed + (currentMobility * mobilitySpeedModifier);

        // Final jump force
        currentJumpForce = baseJumpForce + (currentMobility * mobilityJumpModifier);

        // Update SpeedChange ability
        if (speedChangeAbility != null)
        {
            // SpeedChange multiplier is (desired speed) / (base speed)
            float speedMultiplier = currentMovementSpeed / baseMovementSpeed;
            speedChangeAbility.SpeedChangeMultiplier = speedMultiplier;
        }

        // Update Jump ability
        if (jumpAbility != null)
        {
            jumpAbility.Force = currentJumpForce;
        }

        Debug.Log($"[Mobility Stats Updated] MovementSpeed = {currentMovementSpeed}, JumpForce = {currentJumpForce}");
    }

    // --------------------------------------------------
    // ENCHANTMENT CHECKS
    // --------------------------------------------------
    public void CheckEnchantments()
    {
        // Example: unlock Sky Runner at Mobility = 50
        if (currentMobility >= 50 && !skyRunnerUnlocked)
        {
            skyRunnerUnlocked = true;
            Debug.Log("Sky Runner unlocked!");
            // Insert code granting special abilities, etc.
        }

        // Example: unlock Night Glider at Mobility = 70
        if (currentMobility >= 70 && !nightGliderUnlocked)
        {
            nightGliderUnlocked = true;
            Debug.Log("Night Glider unlocked!");
            // Insert code granting special abilities, etc.
        }
    }

    // --------------------------------------------------
    // MODIFY MOBILITY
    // --------------------------------------------------
    public void IncreaseMobility(int amount)
    {
        currentMobility += amount;
        CalculateStats();
        CheckEnchantments();
        Debug.Log($"Mobility increased by {amount}. New Mobility = {currentMobility}");
    }

    // --------------------------------------------------
    // UPDATE LOOP
    // --------------------------------------------------
    private void Update()
    {
        // Example: continuously adjust Mobility based on local velocity
        float currentSpeed = characterLocomotion.LocalVelocity.magnitude;
        currentMobility = baseMobility + Mathf.RoundToInt(currentSpeed * 3); // Example formula

        CalculateStats();
        CheckEnchantments();
    }
}
