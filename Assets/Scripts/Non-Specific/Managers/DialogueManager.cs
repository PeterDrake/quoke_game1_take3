using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public delegate void NewHeadCallBack(DialogueNode newHead);
    public DialogueDisplayer displayer;

    private bool active;
    private DialogueNode activeDialogue;
    private NPC activeNPC;

    private NewHeadCallBack activeCallback;

    /// <summary> Starts the given dialogue with the given NPC </summary>
    public void StartDialogue(DialogueNode d, NPC n, NewHeadCallBack cb)
    {
        activeDialogue = d;
        activeCallback = cb;
        activeNPC = n;
        
        traverse(d);
        displayer.Load(d,n);

    }
    
    /// <summary> End the current dialogue if one is active</summary>
    /// <returns> A new head if one was reached</returns>
    public DialogueNode EndDialogue()
    {
        return activeDialogue;
    }

    private void Start()
    {
        if (displayer == null) throw new Exception("Dialogue Manager has no displayer!");
        displayer.LinkEvents(OptionOneSelected, OptionTwoSelected, Exit);
    }
    
    private string OptionOneSelected()
    {
        return traverse(activeDialogue.GetNodeOne());
    }
    
    private string OptionTwoSelected()
    {
        return traverse(activeDialogue.GetNodeTwo());
    }

    private string traverse(DialogueNode newActive)
    {
        string resp = activeDialogue.CheckRequirements();
        if (resp != "") return resp;
        
        if (newActive.GetNewHead() != null)
        {
            activeCallback.Invoke(newActive.GetNewHead());
        }
        
        activeDialogue = newActive;
        activeDialogue.DoOutcomes(ref activeNPC);
        displayer.Load(activeDialogue, activeNPC);
        return "";
    }
    

    private string Exit()
    {
        EndDialogue();
        return "";
    }
}
