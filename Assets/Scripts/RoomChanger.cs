using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public void ChangeRoom(int ID)
    {
        if (ID == 3)
        {
            EventHandler.Instance.FromDungeon = true;
        }
        else if (ID == 4) 
        {
            EventHandler.Instance.FromDungeon = false;
        }
        EventHandler.Instance.ChangeRoomEvent?.Invoke(ID);
    }
}
