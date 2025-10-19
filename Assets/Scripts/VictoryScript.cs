using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public void Victory()
    {
        Invoke("LoadMainMenu", 5);
    }
    void LoadMainMenu()
    {
        EventHandler.Instance.ChangeRoomEvent?.Invoke(0);
    }
}
