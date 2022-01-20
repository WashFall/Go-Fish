using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck : MonoBehaviour
{
    private int suit = 0;
    private int number = 0;
    private int deckSize = 52;

    public List<PlayingCard> Generator(GameObject Card)
    {
        List<PlayingCard> deck = new List<PlayingCard>();
        for (int i = 0; i < deckSize; i++)
        {
            PlayingCard card = new PlayingCard(number, suit, Card);
            deck.Add(card);

            number++;
            if(number > 12)
            {
                number = 0;
                suit++;
            }
        }
        return deck;
    }
}
