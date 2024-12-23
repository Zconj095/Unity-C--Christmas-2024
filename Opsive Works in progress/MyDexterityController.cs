using UnityEngine;
// Make sure you have these namespaces for Opsive:
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
// For the event system, check your specific version of Opsive:
using Opsive.Shared.Events;
// or: using Opsive.UltimateCharacterController.Events;

public class MyDexterityController : MonoBehaviour
{
    // ----------------------------------
    // Dexterity
    // ----------------------------------
    [Header("Dexterity Settings")]
    public int baseDexterity = 50;
    public float speedModifier = 0.1f;
    public float dodgeModifier = 0.05f;
    public float craftingModifier = 0.02f;

    // ----------------------------------
    // Current Stats
    // ----------------------------------
    [Header("Current Dexterity & Stats")]
    public int currentDexterity;
    public float movementSpeed;
    public float dodgeChance;
    public float craftingSuccessRate;

    // ----------------------------------
    // Enchantments
    // ----------------------------------
    [Header("Enchantments")]
    public bool dreamWeaverUnlocked = false;
    public bool lightbringerUnlocked = false;

    // ----------------------------------
    // Opsive References
    // ----------------------------------
    private UltimateCharacterLocomotion characterLocomotion;
    private Ability[] allAbilities; // Instead of CharacterAbilities

    // ----------------------------------
    // Initialization
    // ----------------------------------
    private void Start()
    {
        // Initialize current Dexterity
        currentDexterity = baseDexterity;

        // Get Ultimate Character Locomotion
        characterLocomotion = GetComponent<UltimateCharacterLocomotion>();

        // Get all abilities. Explicitly specify type + layer/index:
        // If your version complains, try index = 0 or a different integer.
        allAbilities = characterLocomotion.GetAbilities<Ability>(-1);

        // Calculate initial stats
        CalculateStats();

        // Check enchantments
        CheckEnchantments();

        // Register events (Opsive)
        EventHandler.RegisterEvent<Ability, bool>(gameObject, "OnCharacterAbilityActive", OnAbilityActive);
        EventHandler.RegisterEvent<float>(gameObject, "OnCharacterChangeTimeScale", OnTimeScaleChanged);
    }

    private void OnDestroy()
    {
        // Unregister events
        EventHandler.UnregisterEvent<Ability, bool>(gameObject, "OnCharacterAbilityActive", OnAbilityActive);
        EventHandler.UnregisterEvent<float>(gameObject, "OnCharacterChangeTimeScale", OnTimeScaleChanged);
    }

    // ----------------------------------
    // Stat Calculations
    // ----------------------------------
    public void CalculateStats()
    {
        // Movement Speed
        movementSpeed = 10f + (currentDexterity * speedModifier);

        // Dodge Chance
        dodgeChance = 20f + (currentDexterity * dodgeModifier);

        // Crafting Success
        craftingSuccessRate = 50f + (currentDexterity * craftingModifier);

        // Update TimeScale just as an example
        characterLocomotion.TimeScale = movementSpeed / 10f;

        Debug.Log($"[Dex Updated] Speed = {movementSpeed}, Dodge = {dodgeChance}, Craft = {craftingSuccessRate}");
    }

    // ----------------------------------
    // Enchantments
    // ----------------------------------
    public void CheckEnchantments()
    {
        if (currentDexterity >= 75 && !dreamWeaverUnlocked)
        {
            dreamWeaverUnlocked = true;
            Debug.Log("Dream Weaver unlocked!");
            // Add code for Dream Weaver here.
        }

        if (currentDexterity >= 90 && !lightbringerUnlocked)
        {
            lightbringerUnlocked = true;
            Debug.Log("Lightbringer unlocked!");
            // Add code for Lightbringer here.
        }
    }

    // ----------------------------------
    // Dexterity Modifiers
    // ----------------------------------
    public void IncreaseDexterity(int amount)
    {
        currentDexterity += amount;
        CalculateStats();
        CheckEnchantments();
        Debug.Log($"Increased Dex by {amount}. New Dex = {currentDexterity}");
    }

    // ----------------------------------
    // Event Handlers
    // ----------------------------------
    private void OnAbilityActive(Ability ability, bool active)
    {
        // Retrieve the ability name from the class name (since AbilityName doesn't exist):
        string abilityClassName = ability.GetType().Name;

        // e.g., compare to "YourAbilityName"
        if (abilityClassName == "YourAbilityName" && active)
        {
            IncreaseDexterity(10);
        }
    }

    private float m_PreviousTimeScale;

    private void OnTimeScaleChanged(float newTimeScale)
    {
        float calculatedTimeScale = (movementSpeed / 10f) * newTimeScale;

        // Avoid re-trigger if it's the same
        if (Mathf.Abs(calculatedTimeScale - m_PreviousTimeScale) > 0.0001f)
        {
            m_PreviousTimeScale = calculatedTimeScale;
            characterLocomotion.TimeScale = calculatedTimeScale;
        }
    }


    // ----------------------------------
    // Per-Frame Update
    // ----------------------------------
    private void Update()
    {
        // Example: dynamically alter Dex based on the Y position
        float height = transform.position.y;
        currentDexterity = baseDexterity + Mathf.RoundToInt(height * 2);

        CalculateStats();
        CheckEnchantments();
    }
}
