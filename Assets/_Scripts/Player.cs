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
