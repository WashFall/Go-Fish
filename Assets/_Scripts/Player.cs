using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public string Name;
    public List<PlayingCard> Cards = new List<PlayingCard>();
}
