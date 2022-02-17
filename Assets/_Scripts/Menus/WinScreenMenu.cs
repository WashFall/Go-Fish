using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Destroy(LocalGameManager.Instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        Destroy(LocalGameManager.Instance.gameObject);
        SceneManager.LoadScene("LocalMultiplayer");
    }
}
