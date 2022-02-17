using UnityEngine.UI;

public class ScoreDisplay
{
    public void DisplayScore(Text textObject)
    {
        textObject.text = string.Format("{0} Wins!\nScore: {1}",
            GameManager.Instance.activePlayer.Name,
            GameManager.Instance.activePlayer.Points);
    }
}
