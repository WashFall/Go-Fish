using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocalPlayMenu : MonoBehaviour
{
    public InputField playerAmountInput;
    public Button addPlayer;
    public Button removePlayer;
    public SetNamesMenu setNamesMenu;

    public delegate void SetNamesPress();
    public SetNamesPress setNamesPress;
    public static int playerAmount = 2;

    private void Start()
    {
        playerAmountInput.text = "2";
        setNamesPress += setNamesMenu.SetNames;
    }

    public void AddPlayer()
    {
        if(playerAmount < 7)
            playerAmount += 1;
        playerAmountInput.text = playerAmount.ToString();
    }
    public void RemovePlayer()
    {
        if(playerAmount > 2)
            playerAmount -= 1;
        playerAmountInput.text = playerAmount.ToString();
    }

    public void PlayLocalGame()
    {
        SceneManager.LoadScene("LocalMultiplayer");
    }

    public void SetNamesButton()
    {
        setNamesPress();
    }
}
