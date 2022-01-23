using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public GameObject Card;
    public List<Player> players = new List<Player>();

    GenerateDeck generateDeck;
    List<PlayingCard> deck;
    List<GameObject> cardPool;

    void Start()
    {
        generateDeck = GetComponent<GenerateDeck>();
        InstPool(52);
        deck = generateDeck.Generator(cardPool);
    }

    private void InstPool(int amount)
    {
        cardPool = new List<GameObject>();
        for(int i = 0; i < amount; i++)
        {
            cardPool.Add(Instantiate(Card, new Vector3(0, 0, 0), transform.rotation));
            cardPool[i].SetActive(false);
        }
    }

    public void Dealer()
    {
        foreach (Player player in players)
            player.Name = "player" + players.IndexOf(player);

        for (int i = 0; i < 7; i++)
        {
            foreach (Player player in players)
            {
                player.cards.Add(Deal());
            }
        }

        foreach(Player player in players)
            player.cards.Sort((x, y) => x.number.CompareTo(y.number));

        //foreach (Player player in players)
        //{
        //    string jsonString = JsonUtility.ToJson(player);
        //    SaveToFile(player.Name, jsonString);
        //}
    }

    private PlayingCard Deal()
    {
        int random = Random.Range(0, deck.Count);
        PlayingCard card = deck[random];
        deck.RemoveAt(random);
        return card;
    }

    void SaveToFile(string fileName, string jsonString)
    {
        using(var stream = File.OpenWrite(fileName))
        {
            stream.SetLength(0);
            var bytes = Encoding.UTF8.GetBytes(jsonString);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}