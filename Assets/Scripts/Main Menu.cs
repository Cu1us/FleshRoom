using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive); //UI Scene
    }
    public void StartGame()
    {
        //FindAnyObjectByType<MousePointer>().enabled = true;
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive).completed += OnSceneLoad;
    }

    private void OnSceneLoad(AsyncOperation a)
    {
        SceneManager.UnloadSceneAsync(0); //main menu scene

    }

    public void Quit()
    {
        Application.Quit();
    }
}
