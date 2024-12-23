using UnityEngine;
// Opsive Ultimate Character Controller
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;

// If you're using the Opsive event system in this script, uncomment the relevant namespace:
// using Opsive.Shared.Events; 
// or using Opsive.UltimateCharacterController.Events;

public class MyAgilityController : MonoBehaviour
{
    // ----------------------------------
    // Agility
    // ----------------------------------
    [Header("Agility Settings")]
    [Tooltip("Base agility value (e.g., 40).")]
    public int baseAgility = 40;

    [Tooltip("Movement speed gained per 1 agility.")]
    public float agilitySpeedModifier = 0.15f;

    [Tooltip("Jump force gained per 1 agility.")]
    public float jumpHeightModifier = 0.08f;

    // ----------------------------------
    // Base Movement/Jump (to scale against)
    // ----------------------------------
    [Header("Base Character Settings")]
    [Tooltip("Base movement speed, used in speed calculation.")]
    public float baseMovementSpeed = 6f;

    [Tooltip("Base jump force, used in jump calculation.")]
    public float baseJumpForce = 8f;

    // ----------------------------------
    // Current Stats
    // ----------------------------------
    [Header("Current Agility & Stats")]
    public int currentAgility;
    public float currentMovementSpeed;
    public float currentJumpForce;

    // ----------------------------------
    // Enchantments
    // ----------------------------------
    [Header("Enchantments")]
    public bool windDancerUnlocked = false;
    public bool harmonyWeaverUnlocked = false;

    // ----------------------------------
    // Opsive References
    // ----------------------------------
    private UltimateCharacterLocomotion characterLocomotion;
    private SpeedChange speedChangeAbility;   // For modifying run speed
    private Jump jumpAbility;                 // For modifying jump force

    // ----------------------------------
    // Initialization
    // ----------------------------------
    private void Start()
    {
        // Initialize the current agility
        currentAgility = baseAgility;

        // Get the Opsive locomotion component
        characterLocomotion = GetComponent<UltimateCharacterLocomotion>();

        // Find the SpeedChange and Jump abilities so we can modify them
        speedChangeAbility = characterLocomotion.GetAbility<SpeedChange>();
        jumpAbility = characterLocomotion.GetAbility<Jump>();

        // Calculate initial stats
        CalculateStats();

        // Check for enchantments
        CheckEnchantments();
    }

    // ----------------------------------
    // Agility Calculations
    // ----------------------------------
    public void CalculateStats()
    {
        // Calculate the new movement speed
        currentMovementSpeed = baseMovementSpeed + (currentAgility * agilitySpeedModifier);

        // Calculate the new jump force
        currentJumpForce = baseJumpForce + (currentAgility * jumpHeightModifier);

        // If we have a SpeedChange ability, update its multiplier
        if (speedChangeAbility != null)
        {
            // The multiplier is (desired speed) / (base speed)
            float newMultiplier = currentMovementSpeed / baseMovementSpeed;
            speedChangeAbility.SpeedChangeMultiplier = newMultiplier;
        }

        // If we have a Jump ability, update its Force
        if (jumpAbility != null)
        {
            jumpAbility.Force = currentJumpForce;
        }

        // Debug info
        Debug.Log($"[Agility Stats Updated] MovementSpeed = {currentMovementSpeed}, JumpForce = {currentJumpForce}");
    }

    // ----------------------------------
    // Enchantment Checks
    // ----------------------------------
    public void CheckEnchantments()
    {
        // Example: Wind Dancer unlocks at 60 Agility
        if (currentAgility >= 60 && !windDancerUnlocked)
        {
            windDancerUnlocked = true;
            Debug.Log("Wind Dancer unlocked!");
            // Add code to grant Wind Dancer abilities
        }

        // Example: Harmony Weaver unlocks at 80 Agility
        if (currentAgility >= 80 && !harmonyWeaverUnlocked)
        {
            harmonyWeaverUnlocked = true;
            Debug.Log("Harmony Weaver unlocked!");
            // Add code to grant Harmony Weaver abilities
        }
    }

    // ----------------------------------
    // Agility Modifiers
    // ----------------------------------
    public void IncreaseAgility(int amount)
    {
        currentAgility += amount;
        CalculateStats();
        CheckEnchantments();
        Debug.Log($"Agility increased by {amount}. Now: {currentAgility}");
    }

    // ----------------------------------
    // Per-Frame Update
    // ----------------------------------
    private void Update()
    {
        // Example: Continuously update agility based on the character’s local velocity.
        float currentSpeed = characterLocomotion.LocalVelocity.magnitude;
        currentAgility = baseAgility + Mathf.RoundToInt(currentSpeed * 5);

        CalculateStats();
        CheckEnchantments();
    }
}
