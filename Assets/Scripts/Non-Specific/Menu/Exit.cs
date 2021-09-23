using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToPlay()
    {
        SceneManager.LoadScene("Menu");
    }

    
}
