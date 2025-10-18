using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        FindAnyObjectByType<MousePointer>().enabled = true;
        FindAnyObjectByType<PlayerController>().enabled = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
