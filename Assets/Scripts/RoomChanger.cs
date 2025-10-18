using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public void ChangeRoom(int ID)
    {
        EventHandler.Instance.ChangeRoomEvent?.Invoke(ID);
    }
}
