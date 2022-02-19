using System.Collections.Generic;
using UnityEngine;

public class CardSwitchPlayer
{
    public void SwitchPlayer(PlayerData player, int turn, List<PlayerData> players)
    {
        turn++;
        int playerIndex = turn % players.Count;

        foreach (GameObject card in player.Cards)
        {
            card.SetActive(false);
        }

        LocalGameManager.Instance.activePlayer = players[playerIndex];
    }
}
