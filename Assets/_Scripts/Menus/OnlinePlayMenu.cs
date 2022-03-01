using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlinePlayMenu : MonoBehaviour
{
    public InputField playerAmountInput;
    public Button addPlayer;
    public Button removePlayer;

    public static int onlinePlayerAmount = 2;


    void Start()
    {
        playerAmountInput.text = "2";

    }

    public void AddPlayer()
    {
        if (onlinePlayerAmount < 7)
            onlinePlayerAmount += 1;
        playerAmountInput.text = onlinePlayerAmount.ToString();
    }

    public void RemovePlayer()
    {
        if (onlinePlayerAmount > 2)
            onlinePlayerAmount -= 1;
        playerAmountInput.text = onlinePlayerAmount.ToString();
    }

    public void PlayLocalGame()
    {
        SceneManager.LoadScene("OnlineWaitForPlayers");
    }

    public void SaveGameSettings()
    {
        GameInfo newGame = new GameInfo();
        newGame.GameName = SaveUserData.data.Name + "'s Game";
        newGame.GameTurn = 0;

        string key = SaveManager.Instance.GetKey("games/");
        newGame.GameID = key;

        PlayerData thisPlayer = new PlayerData();
        thisPlayer.User = SaveUserData.data;
        thisPlayer.Name = SaveUserData.data.Name;
        newGame.Players.Add(thisPlayer);

        string gamePath = "games/" + newGame.GameID;

        var data = JsonUtility.ToJson(newGame);

        SaveManager.Instance.SaveData(gamePath, data, PlayLocalGame);
    }
}
