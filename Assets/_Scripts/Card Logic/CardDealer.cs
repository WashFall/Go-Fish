using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public GameObject Card;

    public List<GameObject> cardPool;

    void Start()
    {
        InstPool(52);
        GameManager.Instance.generateDeck.Generator(cardPool);
    }

    private void InstPool(int amount)
    {
        float position = 0.5f;
        Vector3 screenScale = new Vector3(Screen.width / 7 * position, Screen.height / 5, 1);
        Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
        cardPool = new List<GameObject>();
        for(int i = 0; i < amount; i++)
        {
            cardPool.Add(Instantiate(Card, new Vector3(0, cardPos.y, 0), Quaternion.Euler(0, 181, 0)));
            cardPool[i].SetActive(false);
        }
    }

    public void Dealer()
    {
        for (int i = 0; i < 7; i++)
        {
            foreach (Player player in GameManager.Instance.players)
            {
                player.Cards.Add(Deal());
            }
        }

        foreach(Player player in GameManager.Instance.players)
        {
            player.Name = "player" + GameManager.Instance.players.IndexOf(player);
            player.Cards.Sort((x, y) => x.GetComponent<PlayingCard>().number.CompareTo
            (y.GetComponent<PlayingCard>().number));
        }

    }

    public GameObject Deal()
    {
        int random = Random.Range(0, cardPool.Count);
        GameObject card = cardPool[random];
        cardPool.RemoveAt(random);
        return card;
    }
}