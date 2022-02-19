using UnityEngine;

public class GameEnd
{
    public void WinCondition()
    {
        int scoreCheck = 0;
        foreach(PlayerData player in LocalGameManager.Instance.players)
        {
            scoreCheck += player.Points;
        }

        if(scoreCheck == 13)
        {
            LocalGameManager.Instance.UpdateGameState(GameState.WinState);
        }
    }
}
