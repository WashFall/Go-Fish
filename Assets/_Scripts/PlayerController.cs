using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int turn = 0;
    CardDealer dealer;
    bool display = true;
    Player player = new Player();

    delegate bool InvertBool(bool x);
    InvertBool invertBool = x => !x;

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
            foreach (PlayingCard card in player.Cards)
            {
                Vector3 screenScale = new Vector3(Screen.width / player.Cards.Count * position, Screen.height / 5, 0);
                Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
                cardPos.z = 0;
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

        foreach (PlayingCard card in player.Cards)
        {
            card.card.SetActive(false);
        }

        if (display)
            display = invertBool(display);
        
        if(turn % dealer.players.Count == dealer.players.IndexOf(player))
            display = invertBool(display);

        ShowHand();
        foreach(PlayingCard card in player.Cards)
            StartCoroutine(CardShake(card));
    }

    public void TurnCards()
    {
        foreach(PlayingCard card in player.Cards)
        {
            StartCoroutine(CardRotate(card));
        }
    }

    private IEnumerator CardRotate(PlayingCard card)
    {
        float startTime = Time.time;
        while(Time.time - startTime < 0.48f)
        {
            card.card.transform.Rotate(0, 180 * 2 * Time.fixedDeltaTime, 0);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CardShake(PlayingCard card)
    {
        Vector3 originalPos = card.card.transform.position;
        float startTime = Time.time;

        while(Time.time - startTime < 0.25f)
        {
            card.card.transform.position = Vector3.MoveTowards(card.card.transform.position,
                new Vector3(0, originalPos.y, originalPos.z), 40 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        while(Time.time - startTime < 0.50f)
        {
            Vector3 shakePos = Random.insideUnitSphere * Time.fixedDeltaTime * 5;
            shakePos.y = originalPos.y;
            shakePos.z = originalPos.z;
            card.card.transform.position = shakePos;
            yield return new WaitForFixedUpdate();
        }

        while (Time.time - startTime < 0.75f)
        {
            card.card.transform.position = Vector3.MoveTowards(card.card.transform.position, 
                originalPos, 40 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
