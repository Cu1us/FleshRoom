using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private int previousRoom, currentRoom;
    [SerializeField] AudioClip menu, main;
    [SerializeField] AudioSource source;
    void Start()
    {
        previousRoom = 0;
        EventHandler.Instance.ChangeRoomEvent += ChangeRoom;
    }

    public void ChangeRoom(int ID)
    {
        if (currentRoom == 0) source.clip = main; source.Play();
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
