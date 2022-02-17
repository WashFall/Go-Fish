using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadScene("LocalMultiplayer");
    }
}
