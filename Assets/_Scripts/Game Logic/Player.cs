using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public string Name;
    public List<GameObject> Cards = new List<GameObject>();
    public int Points = 0;
}

[Serializable]
public class UserInfo
{
    public string UserEmail;
    public string UserPassword;
    public string UserName;
}

[Serializable]
public class GameInfo
{
    public List<Player> Players;
    public string GameID;
    public int GameTurn;
}
