using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Dialogue/NPC")]
public class NPC : ScriptableObject
{
    public new string name;
    public Sprite image;
}
