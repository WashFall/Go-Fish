using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    List<Player> players;

    public void PlayerCountListener(List<Player> players)
    {
        this.players = players;
    }

    public void ShowHand(Player player)
    {
        // TODO: add observer code to improve the process

        float position = 0.5f;
        foreach (GameObject card in player.Cards)
        {
            Vector3 screenScale = new Vector3(Screen.width / player.Cards.Count * position, Screen.height / 5, 0);
            Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
            cardPos.z = 0;
            card.transform.position = cardPos;
            card.SetActive(true);
            position++;
        }
    }

    public void SwitchPlayer(Player player, int turn)
    {
        turn++;
        int playerIndex = turn % players.Count;

        foreach (GameObject card in player.Cards)
        {
            card.SetActive(false);
        }

        GameManager.Instance.activePlayer = players[playerIndex];
        player = players[playerIndex];

        ShowHand(player);
        foreach(GameObject card in player.Cards)
            StartCoroutine(CardShake(card));
    }

    public void TurnCards()
    {
        foreach(Player player in players)
        {
            foreach(GameObject card in player.Cards)
            {
                CardRotate(card);
            }
        }
    }

    private IEnumerator CardShake(GameObject card)
    {
        Vector3 originalPos = card.transform.position;
        float startTime = Time.time;

        while(Time.time - startTime < 0.25f)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position,
                new Vector3(0, originalPos.y, originalPos.z), 40 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        while(Time.time - startTime < 0.50f)
        {
            Vector3 shakePos = Random.insideUnitSphere * Time.fixedDeltaTime * 5;
            shakePos.y = originalPos.y;
            shakePos.z = originalPos.z;
            card.transform.position = shakePos;
            yield return new WaitForFixedUpdate();
        }

        while (Time.time - startTime < 0.75f)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position, 
                originalPos, 40 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    void CardRotate(GameObject card)
    {
        card.transform.DORotate(new Vector3(0, 180, 0), 0.6f, RotateMode.LocalAxisAdd);
    }


}
