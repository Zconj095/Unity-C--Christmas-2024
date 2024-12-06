using UnityEngine;

/// <summary>
/// Marker component for identifying pre-allocated objects in the scene.
/// </summary>
public class PreAllocated : MonoBehaviour
{
    [Tooltip("Optional information about the allocation")]
    public string allocationInfo = "Default Allocation";
}
