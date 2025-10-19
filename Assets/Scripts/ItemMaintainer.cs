using UnityEngine;

public class ItemMaintainer : MonoBehaviour
{
    [SerializeField] GameObject ClosedDoor, PigFat, Key, Royal, player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           if (ClosedDoor && EventHandler.Instance.DungeonDoorUnlocked) ClosedDoor.SetActive(false);
           if (PigFat && EventHandler.Instance.FatPickedUp) PigFat.SetActive(false);
           if (Key && EventHandler.Instance.KeyPickedUp) Key.SetActive(false);
           if (player && EventHandler.Instance.FromDungeon) player.transform.position += new Vector3(12,0,0);

           if (Royal && EventHandler.Instance.HasGottenExposition) Royal.GetComponentInChildren<DialogueSequence>().PlaySequence();
           EventHandler.Instance.HasGottenExposition = true;
    }

    public void FromDungeon(bool temp)
    {
        EventHandler.Instance.FromDungeon = temp;
    }
}
