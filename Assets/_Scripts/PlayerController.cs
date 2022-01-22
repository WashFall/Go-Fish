using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CardDealer dealer;
    bool display = true;
    Player player = new Player();
    List<GameObject> displayedCards;

    delegate bool InvertBool(bool x);
    InvertBool invertbool = x => !x;

    private void Start()
    {
        dealer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CardDealer>();
        if(gameObject.CompareTag("Player"))
        {
            display = false;
        }
    }

    public void Ready()
    {
        dealer.players.Add(player);
    }

    public void ShowHand()
    {
        float position = 0.5f;
        Debug.Log(player.Name);
        displayedCards = new List<GameObject>();
        if (display)
        {
            foreach (PlayingCard card in player.cards)
            {
                Vector3 screenScale = new Vector3(Screen.width / player.cards.Count * position, Screen.height / 5, 0);
                Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
                cardPos.z = 0;
                Sprite cardFace = Resources.Load<Sprite>(card.cardName);
                card.card.GetComponent<SpriteRenderer>().sprite = cardFace;
                displayedCards.Add(Instantiate(card.card, cardPos, transform.rotation));
                position++;
            }
        }
    }

    public void SwitchHand()
    {
        foreach(GameObject card in displayedCards)
        {
            Destroy(card);
        }

        display = invertbool(display);

        ShowHand();
    }
}
