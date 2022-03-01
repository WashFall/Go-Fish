using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public UserData User;
    public string Name;
    public List<GameObject> Cards = new List<GameObject>();
    public int Points = 0;
}