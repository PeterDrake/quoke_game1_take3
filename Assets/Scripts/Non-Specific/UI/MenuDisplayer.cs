using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using UnityEngine.EventSystems;

/// <summary>
/// Manages the Menu, containing buttons such as Exit to Menu, Exit Game, Settings, and Inventory
/// </summary>
public class MenuDisplayer : UIElement
{
    public int mainMenuSceneIndex;
    public GameObject returnButton;
    
    // toggler, exitToMenu, quitGame, Settings, Inventory 

    private GameObject toggler;
    private bool othCanviOpen;
    private UIElement othCanviScript;

    private void Start()
    {
        locked = true;
        pauseOnOpen = true;
        othCanviOpen = false;
        Systems.Input.RegisterKey("escape", delegate {
        print(othCanviOpen);
            if (othCanviOpen)
            {
                //doesn't close othCanvi, open menu         <- best result
                //if locked = true is inactive in othScript, 
                    //then will toggle between menu and othCanvi, no exit
                //UIManager.Instance.ToggleActive(this);

                //doesn't open menu, still paused
                //othCanviScript.Close();
                //UIManager.Instance.ToggleActive(this);

                //doesn't open menu, still paused
                //othCanviScript.Close();

                //doesn't close othCanvi, open menu         <- best result
                //if locked = true inactive,
                //then doesn't open menu, still paused
                //if locked = true is inactive in othScript,
                //then will toggle between menu and othCanvi, no exit
                //UIManager.Instance.ToggleActive(this);
                //othCanviScript.Close();

                // BEST RESULT!!!
                UIManager.Instance.ActivatePrevious();
                //closes canvi properly! Yay!
                UIManager.Instance.ToggleActive(this);
                //opens menu! which can be closed again to play!
            }
            else
            {
                UIManager.Instance.ToggleActive(this);
            }
        });
        initialize();
        toggler.SetActive(false);
    }

    public void openedCanvi(UIElement othScript)
    {
        othCanviScript = othScript;
        othCanviOpen = true;
    }
    public void closedCanvi()
    {
        othCanviOpen = false;
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
                    child.GetComponent<Button>().onClick.AddListener(delegate {UIManager.Instance.ToggleActive(this); });
                    break;
                case "exit":
                    child.GetComponent<Button>().onClick.AddListener(gameover);
                    break;
                case "mainMenu":
                    child.GetComponent<Button>().onClick.AddListener(mainMenu);
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
        EventSystem.current.SetSelectedGameObject(null);
        toggler.SetActive(false);
    }

    private void mainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void gameover()
    {
        SceneManager.LoadScene("EndPage");
    }

    private void settings() { }
}