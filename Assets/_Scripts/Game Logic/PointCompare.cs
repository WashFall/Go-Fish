using System.Collections.Generic;
using UnityEngine;

public class PointCompare
{
    public void GetWinner(List<Player> players)
    {
        List<Player> pointOrder = players;
        
        pointOrder.Sort((y, x) => x.Points.CompareTo
        (y.Points));

        LocalGameManager.Instance.activePlayer = pointOrder[0];
    }
}
