using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public GameObject Card;

    GenerateDeck generateDeck;
    List<GameObject> cardPool;
    List<Player> players;

    void Start()
    {
        generateDeck = GetComponent<GenerateDeck>();
        InstPool(52);
        generateDeck.Generator(cardPool);
    }
    public void PlayerCountListener(List<Player> players)
    {
        this.players = players;
    }

    private void InstPool(int amount)
    {
        cardPool = new List<GameObject>();
        for(int i = 0; i < amount; i++)
        {
            cardPool.Add(Instantiate(Card, new Vector3(-0.00680274004f, -3, 0), transform.rotation));
            cardPool[i].SetActive(false);
        }
    }

    public void Dealer()
    {
        for (int i = 0; i < 7; i++)
        {
            foreach (Player player in players)
            {
                player.Cards.Add(Deal());
            }
        }

        foreach(Player player in players)
        {
            player.Name = "player" + players.IndexOf(player); // TODO: move this line 
            player.Cards.Sort((x, y) => x.GetComponent<PlayingCard>().number.CompareTo
            (y.GetComponent<PlayingCard>().number));
        }

        //foreach (Player player in players)
        //{
        //    string jsonString = JsonUtility.ToJson(player);
        //    SaveToFile(player.Name, jsonString);
        //}
    }

    private GameObject Deal()
    {
        int random = Random.Range(0, cardPool.Count);
        GameObject card = cardPool[random];
        cardPool.RemoveAt(random);
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