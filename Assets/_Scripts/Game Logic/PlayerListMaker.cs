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

        foreach(Player player in players)
        {
            player.Name = "Player " + (LocalGameManager.Instance.players.IndexOf(player) + 1);
        }

        if(SetNamesMenu.names.Count > 0)
        {
            for(int i = 0; i < playerCount; i++)
            {
                if(SetNamesMenu.names[i] != "")
                    players[i].Name = SetNamesMenu.names[i];
            }
        }

        LocalGameManager.Instance.activePlayer = players[0];
        return players;
    }
}
