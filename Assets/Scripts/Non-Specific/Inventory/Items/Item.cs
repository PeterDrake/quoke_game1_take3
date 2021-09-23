using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string DisplayName;
    public string Description;
    
    public Sprite Icon;
    public Sprite DisplayImage;
    
    public string ID;

    public ItemAction action;
}
