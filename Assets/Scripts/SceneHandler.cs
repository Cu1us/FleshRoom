using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private int previousRoom, currentRoom;
    void Start()
    {
        previousRoom = 2;
        EventHandler.Instance.ChangeRoomEvent += ChangeRoom;
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
