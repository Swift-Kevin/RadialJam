using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuHolder;

    public void PlayGame()
    {
        UIManager.Instance.StartFadeIn();
        mainMenuHolder.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
