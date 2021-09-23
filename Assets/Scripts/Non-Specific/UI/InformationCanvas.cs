using UnityEngine;
using UnityEngine.UI;

public class InformationCanvas : MonoBehaviour
{
    // changes the what the information canvas displays 'Exit the house', 'Turn off the gas' , etc.

    public bool ActiveOnStart = true;
    public Text info;
    [Header("If null, will search for a child called \"toggle\" on start")]
    [SerializeField] private GameObject toggle;

    private void Start()
    {
        if (toggle == null)
            toggle = transform.Find("toggle").gameObject;
        toggle.SetActive(ActiveOnStart);
    }

    public void ChangeText(string newInteract)
    {
        info.text = newInteract;
        ToggleVisibility(true);
    }

    public void ToggleVisibility(bool visible)
    {
        toggle.SetActive(visible && info.text.Length > 0);
    }
}
