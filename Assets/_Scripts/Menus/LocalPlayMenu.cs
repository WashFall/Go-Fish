using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocalPlayMenu : MonoBehaviour
{
    public InputField playerAmountInput;
    public Button addPlayer;
    public Button removePlayer;

    public static int playerAmount = 2;

    private void Start()
    {
        playerAmountInput.text = "2";
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
}
