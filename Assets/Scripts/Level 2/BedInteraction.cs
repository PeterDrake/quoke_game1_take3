using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    private bool _firstInteraction = true;

    private InteractWithObject _interact;
    private void Start()
    {
        _interact = GetComponent<InteractWithObject>();
    }

    public void Interaction()
    {
        Systems.Status.AffectWarmth(50);
        GameObject.Find("MeterDing").GetComponent<AudioSource>().Play();
        _firstInteraction = false;
        //_interact.SetInteractText("Press 'E' to rest in the bed");
        _interact.DeleteItems();
        // _interact = null;
    }
}