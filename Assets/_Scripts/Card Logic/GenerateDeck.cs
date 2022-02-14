using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck
{
    Sprite[] textures;
    private int suit = 0;
    private int number = 0;
    private int deckSize = 52;

    //public void Generator(List<GameObject> Cards)
    //{
    //    textures = Resources.LoadAll("Textures");

    //    for (int i = 0; i < deckSize; i++)
    //    {
    //        PlayingCard cardComponent = Cards[i].GetComponent<PlayingCard>();

    //        cardComponent.number = number;
    //        cardComponent.suit = suit;
    //        cardComponent.cardName = (suit + "" + number).ToString();
    //        var texture = System.Array.Find(textures, x => x.name == cardComponent.cardName);
    //        Cards[i].GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)texture;
    //        number++;
    //        if(number > 12)
    //        {
    //            number = 0;
    //            suit++;
    //        }
    //    }
    //}
    public void Generator(List<GameObject> Cards)
    {
        textures = Resources.LoadAll<Sprite>("NewTextures");
        
        for (int i = 0; i < deckSize; i++)
        {
            PlayingCard cardComponent = Cards[i].GetComponent<PlayingCard>();

            cardComponent.number = number;
            cardComponent.suit = suit;
            cardComponent.cardName = (suit + "" + number).ToString();
            var texture = System.Array.Find(textures, x => x.name == cardComponent.cardName);
            Cards[i].GetComponentInChildren<SpriteRenderer>().sprite = texture;
            number++;
            if (number > 12)
            {
                number = 0;
                suit++;
            }
        }
    }
}
