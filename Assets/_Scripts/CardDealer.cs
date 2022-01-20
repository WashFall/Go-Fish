using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public GameObject Card;
    public List<Player> players = new List<Player>();

    GenerateDeck generateDeck;
    List<PlayingCard> deck;

    void Start()
    {
        generateDeck = GetComponent<GenerateDeck>();
        deck = generateDeck.Generator(Card);
    }

    public void Dealer()
    {
        for (int i = 0; i < 7; i++)
        {
            foreach (Player player in players)
            {
                player.cards.Add(Deal());
            }
        }
        Debug.Log(players.Count);
    }

    private PlayingCard Deal()
    {
        int random = Random.Range(0, deck.Count);
        PlayingCard card = deck[random];
        deck.RemoveAt(random);
        return card;
    }
}
