using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    public GameObject panel;

    public void play()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
}

