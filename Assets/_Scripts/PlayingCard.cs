using UnityEngine;

public class PlayingCard
{
    public int suit;
    public int number;
    public string cardName;
    public GameObject card;

    public PlayingCard(int number, int suit, GameObject card)
    {
        this.number = number;
        this.suit = suit;
        this.card = card;

        cardName = (suit + "" + number).ToString();
    }
}
