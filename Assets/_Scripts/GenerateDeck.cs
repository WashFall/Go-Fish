using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck : MonoBehaviour
{
    Object[] textures;
    private int suit = 0;
    private int number = 0;
    private int deckSize = 52;

    public List<PlayingCard> Generator(List<GameObject> Cards)
    {
        textures = Resources.LoadAll("Textures");

        List<PlayingCard> deck = new List<PlayingCard>();
        for (int i = 0; i < deckSize; i++)
        {
            PlayingCard card = new PlayingCard(number, suit, Cards[i]);
            var texture = System.Array.Find(textures, x => x.name == card.cardName);
            card.card.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)texture;
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
