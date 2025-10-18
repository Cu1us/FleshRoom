using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        FindAnyObjectByType<MousePointer>().enabled = true;
        FindAnyObjectByType<PlayerController>().enabled = true;
        SceneManager.UnloadSceneAsync(0); //main menu scene
    }

    public void Quit()
    {
        Application.Quit();
    }
}
