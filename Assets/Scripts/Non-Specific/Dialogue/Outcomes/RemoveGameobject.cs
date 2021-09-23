using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameObject Removal", menuName = "Dialogue/Outcomes/RemoveGameobject")]
public class RemoveGameobject: DialogueOutcome
{
    public string nameOfGameObject;
    
    // Need a static reference to Inventory available before this can be implemented
    public override void DoOutcome(ref NPC n)
    {
        Destroy(GameObject.Find(nameOfGameObject));

        Debug.Log("Got rid of" + nameOfGameObject);
    }
}
