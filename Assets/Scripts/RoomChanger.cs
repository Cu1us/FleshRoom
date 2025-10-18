using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public void ChangeRoom(int ID)
    {
        Debug.Log("triggers event");
        EventHandler.Instance.ChangeRoomEvent?.Invoke(ID);
    }
}
