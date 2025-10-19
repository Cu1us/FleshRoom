using UnityEngine;

public class ItemMaintainer : MonoBehaviour
{
    [SerializeField] GameObject ClosedDoor, PigFat, Key, Royal, startText, player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           if (ClosedDoor && EventHandler.Instance.DungeonDoorUnlocked) ClosedDoor.SetActive(false);
           if (PigFat && EventHandler.Instance.FatPickedUp) PigFat.SetActive(false);
           if (Key && EventHandler.Instance.KeyPickedUp) Key.SetActive(false);
           if (player && EventHandler.Instance.FromDungeon) player.transform.position += new Vector3(12,0,0);

           if (Royal && !EventHandler.Instance.HasGottenExposition) Invoke("PlayText", 1);
           EventHandler.Instance.HasGottenExposition = true;
    }

    public void FromDungeon(bool temp)
    {
        EventHandler.Instance.FromDungeon = temp;
    }
    public void OpenDoor()
    {
        ClosedDoor.SetActive(false);
        EventHandler.Instance.DungeonDoorUnlocked = true;
    }
    public void Pickup(bool key) 
    {
        if (key) EventHandler.Instance.KeyPickedUp = true;
        if (!key) EventHandler.Instance.FatPickedUp = true;
    }

    void PlayText()
    {
        startText.GetComponent<DialogueSequence>().PlaySequence();
    }
}
