using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Systems))]
public class SystemsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        // fetch current values from the target
        Systems s = target as Systems;
        GameObject t = s.gameObject;

        serializedObject.Update();
        s.dialogueManager = t.GetComponent<DialogueManager>();
        if (s.dialogueManager == null) t.AddComponent<DialogueManager>();

        s.inventoryManager = t.GetComponent<InventoryHelper>();
        if (s.inventoryManager == null) t.AddComponent<InventoryHelper>();
        
        s.objectivesManager = t.GetComponent<ObjectiveManager>();
        if (s.objectivesManager == null) t.AddComponent<ObjectiveManager>();
        
        s.statusManager = t.GetComponent<StatusManager>();
        if (s.statusManager == null) t.AddComponent<StatusManager>();

        s.inputManager = t.GetComponent<InputManager>();
        if (s.inputManager == null) t.AddComponent<InputManager>();
        
        // Apply values to the target
        serializedObject.ApplyModifiedProperties();
        
        DrawDefaultInspector();
    }
}
