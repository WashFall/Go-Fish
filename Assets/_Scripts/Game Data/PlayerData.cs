using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string Name;
    public List<GameObject> Cards = new List<GameObject>();
    public int Points = 0;
}