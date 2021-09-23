using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using UnityEngine.EventSystems;

public class MainPauseMenuDisplayer : UIElement
{
    public GameObject returnButton;
    private GameObject toggler;
    private bool open;

    // Start is called before the first frame update
    void Start()
    {
        locked = true;
        pauseOnOpen = true;
        
        Systems.Input.RegisterKey("escape", delegate
        {
            print("UIManager = " + UIManager.Instance);
            UIManager.Instance.ToggleActive(this);
        });
        initialize();
        toggler.SetActive(false);
        open = false;
    }

    private void initialize()
    {
        toggler = transform.Find("toggle").gameObject;
        Transform buttons = toggler.transform.Find("buttons");

        foreach (Transform child in buttons)
        {
            switch (child.name)
            {
                case "close":
                    child.GetComponent<Button>().onClick.AddListener(delegate { UIManager.Instance.ToggleActive(this); });
                    break;
                case "exit":
                    child.GetComponent<Button>().onClick.AddListener(gameOver);
                    break;
                case "settings":
                    child.GetComponent<Button>().onClick.AddListener(settings);
                    break;
            }
        }
    }

    public override void Open()
    {
        toggler.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(returnButton);
        
    }

    public override void Close()
    {
        toggler.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void gameOver()
    {
        SceneManager.LoadScene("EndPage");
    }

    private void settings() { }

}
