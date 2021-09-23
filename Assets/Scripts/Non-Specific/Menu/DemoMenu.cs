using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoMenu : MonoBehaviour

{
    private LogToServer logger;
    //loads the scene with the name you give it
    public void loadDemoScene(string sceneName)
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        logger.sendToLog("Began " + sceneName,"LEVEL");
        SceneManager.LoadScene(sceneName);
        SavedData.hints = false;
    }
}
