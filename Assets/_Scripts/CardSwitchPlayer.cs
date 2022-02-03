using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSwitchPlayer
{
    public void SwitchPlayer(Player player, int turn, List<Player> players)
    {
        turn++;
        int playerIndex = turn % players.Count;

        foreach (GameObject card in player.Cards)
        {
            card.SetActive(false);
        }

        GameManager.Instance.activePlayer = players[playerIndex];
    }
}
