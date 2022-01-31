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
        foreach (GameObject card in player.Cards)
            CardShake(card);
    }

    public void ShowHand(Player player)
    {
        float position = 0.5f;
        foreach (GameObject card in player.Cards)
        {
            Vector3 screenScale = new Vector3(Screen.width / player.Cards.Count * position, Screen.height / 5, 0);
            Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
            cardPos.z = 0;
            card.transform.DOMove(cardPos, 0.4f).SetEase(Ease.InQuad);
            card.SetActive(true);
            position++;
        }
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

    void CardRotate(GameObject card)
    {
        card.transform.DORotate(new Vector3(0, 180, 0), 0.6f, RotateMode.LocalAxisAdd);
    }

    async void CardShake(GameObject card)
    {
        Vector3 originalPos = card.transform.position;

        var tasks = new List<Task>();
        tasks.Add(card.transform.DOMoveX(0, 0.6f).SetEase(Ease.InQuad).AsyncWaitForCompletion());

        await Task.WhenAll(tasks);

        tasks.Add(card.transform.DOShakePosition(0.1f, 0.4f).AsyncWaitForCompletion());

        await Task.WhenAll(tasks);

        tasks.Add(card.transform.DOMoveX(originalPos.x, 0.4f).SetEase(Ease.InQuad).AsyncWaitForCompletion());
    }
}
