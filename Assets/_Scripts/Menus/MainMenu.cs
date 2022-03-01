using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text username;

    private void Start()
    {
        ShowName();
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ShowName()
    {
        if (SaveUserData.data != null)
            username.text = SaveUserData.data.Name;
    }
}
