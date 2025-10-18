using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private int previousRoom, currentRoom;
    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(1, LoadSceneMode.Additive); //main menu scene
        SceneManager.LoadScene(1, LoadSceneMode.Additive); //UI menu scene
        previousRoom = 0;
    }

    public void ChangeRoom(int ID)
    {
        var waiting = SceneManager.LoadSceneAsync(ID, LoadSceneMode.Additive);
        waiting.completed += UnLoadCurrentScene;
        currentRoom = ID;
    }

    private void UnLoadCurrentScene(AsyncOperation loading)
    {
        SceneManager.UnloadSceneAsync(previousRoom);
        previousRoom = currentRoom;
    }
}
