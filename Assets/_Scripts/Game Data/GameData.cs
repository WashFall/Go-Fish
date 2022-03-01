using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameInfo
{
    public List<PlayerData> Players = new List<PlayerData>();
    public string GameID;
    public string GameName;
    public int GameTurn;
    public string Winner;
}
