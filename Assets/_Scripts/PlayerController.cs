using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CardDealer dealer;
    [SerializeField]
    bool display = true;
    Player player = new Player();
    delegate bool InvertBool(bool x);
    InvertBool invertbool = x => !x;
    int turn = 0;
    int flip = 180;

    private void Start()
    {
        dealer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CardDealer>();
        if (gameObject.CompareTag("Player"))
        {
            display = false;
        }
    }

    public void Ready()
    {
        // TODO: add observer code to improve the process
        dealer.players.Add(player);
    }

    public void ShowHand()
    {
        // TODO: add observer code to improve the process

        float position = 0.5f;
        if (display)
        {
            foreach (PlayingCard card in player.cards)
            {
                Vector3 screenScale = new Vector3(Screen.width / player.cards.Count * position, Screen.height / 5, 0);
                Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
                cardPos.z = 0;
                //Sprite cardFace = Resources.Load<Sprite>(card.cardName);
                //card.card.GetComponent<SpriteRenderer>().sprite = cardFace;
                card.card.transform.position = cardPos;
                card.card.SetActive(true);
                position++;
            }
        }
    }

    public void SwitchHand()
    {
        // TODO: add observer code to improve the process
        turn++;

        foreach (PlayingCard card in player.cards)
        {
            card.card.SetActive(false);
        }

        if (display)
            display = invertbool(display);
        
        if(turn % dealer.players.Count == dealer.players.IndexOf(player))
            display = invertbool(display);

        ShowHand();
    }

    public void TurnCards()
    {
        foreach(PlayingCard card in player.cards)
        {
            card.card.transform.rotation = Quaternion.Euler(0, flip, 0);
        }
        if (flip == 180)
            flip = 0;
        else if (flip == 0)
            flip = 180;
    }
}
