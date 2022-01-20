using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<PlayingCard> cards = new List<PlayingCard>();
    List<GameObject> displayedCards;

    private CardDealer dealer;
    private bool display = true;

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
        dealer.players.Add(this);
    }

    public void ShowHand()
    {
        float position = 0.5f;

        displayedCards = new List<GameObject>();
        if (display)
        {
            foreach (PlayingCard card in cards)
            {
                Vector3 screenScale = new Vector3(Screen.width / 7 * position, Screen.height / 5, 0);
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
        
        if(display == true)
        {
            display = false;
        }
        else if(display == false)
        {
            display = true;
        }
        
        ShowHand();
    }
}
