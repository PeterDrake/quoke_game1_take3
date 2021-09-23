using UnityEngine;

public class SinkInteraction : MonoBehaviour
{
    private bool _firstInteraction = true;

    private InteractWithObject _interact;
    private void Start()
    {
        _interact = GetComponent<InteractWithObject>();
    }

    public void Interaction()
    {
        if (!_firstInteraction)
        {
            Systems.Status.AffectHydration(100);
            GameObject.Find("MeterDing").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("InventoryZip").GetComponent<AudioSource>().Play();
            _firstInteraction = false;
            _interact.SetInteractText("Press 'E' to drink from the sink");
            _interact.DeleteItems();
            //_interact = null;
        }
    }
}