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
        EventHandler.Instance.ChangeRoomEvent(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
