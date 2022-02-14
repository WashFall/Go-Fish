using System.Collections.Generic;

public class PlayerListMaker
{
    public List<Player> PlayerListCreator(int playerCount, List<Player> players)
    {
        for (int i = 0; i < playerCount; i++)
        {
            Player player = new Player();
            players.Add(player);
        }
        GameManager.Instance.activePlayer = players[0];
        return players;
    }
}
