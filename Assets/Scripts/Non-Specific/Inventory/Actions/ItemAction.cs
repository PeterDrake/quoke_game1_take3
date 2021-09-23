using UnityEngine;

/// <summary>
/// An action that will be triggered when an Item is used from the inventory
/// </summary>
public abstract class ItemAction : ScriptableObject
{
    [Header("What will be displayed when the player selects the item in their inventory (e.g drink water)")]
    public string ActionWord;
    public abstract bool Use(ref Item i);
}