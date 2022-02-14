using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public void WinCondition()
    {
        int scoreCheck = 0;
        foreach(Player player in GameManager.Instance.players)
        {
            scoreCheck += player.Points;
        }

        if(scoreCheck == 13)
        {
            GameManager.Instance.UpdateGameState(GameState.WinState);
        }
    }
}
