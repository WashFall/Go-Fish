using System.Collections.Generic;
using UnityEngine;

public class CardSteal
{
    public void StealCards(GameObject card, Player targetedPlayer)
    {
        List<int> cardIndex = new List<int>();
        foreach (GameObject c in targetedPlayer.Cards)
        {
            if (c.GetComponent<PlayingCard>().number == card.GetComponent<PlayingCard>().number)
            {
                GameManager.Instance.activePlayer.Cards.Add(c);

                GameManager.Instance.activePlayer.Cards.Sort((x, y) =>
                x.GetComponent<PlayingCard>().number.CompareTo(
                y.GetComponent<PlayingCard>().number));

                cardIndex.Add(targetedPlayer.Cards.IndexOf(c));
            }
        }

        cardIndex.Sort((a, b) => b.CompareTo(a));

        foreach (int i in cardIndex)
        {
            targetedPlayer.Cards.RemoveAt(i);
        }

        foreach (GameObject c in GameManager.Instance.activePlayer.Cards)
        {
            card.SetActive(false);
        }
    }
}
