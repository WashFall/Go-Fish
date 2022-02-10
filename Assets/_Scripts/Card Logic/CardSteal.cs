using System.Collections.Generic;
using UnityEngine;

public class CardSteal
{
    public GameObject StealCards(GameObject card, Player targetedPlayer)
    {
        List<int> cardIndex = new List<int>();
        GameObject returnCard = null;
        foreach (GameObject c in targetedPlayer.Cards)
        {
            if (c.GetComponent<PlayingCard>().number == card.GetComponent<PlayingCard>().number)
            {
                GameManager.Instance.activePlayer.Cards.Add(c);

                GameManager.Instance.activePlayer.Cards.Sort((x, y) =>
                x.GetComponent<PlayingCard>().number.CompareTo(
                y.GetComponent<PlayingCard>().number));

                cardIndex.Add(targetedPlayer.Cards.IndexOf(c));
                c.transform.rotation = Quaternion.Euler(0, 1, 0);
                returnCard = c;
            }
        }

        if(cardIndex.Count == 0 && GameManager.Instance.GetComponent<CardDealer>().cardPool.Count > 0)
        {
            GameObject newCard = GameManager.Instance.GetComponent<CardDealer>().Deal();
            newCard.transform.rotation = Quaternion.Euler(0, 1, 0);
            GameManager.Instance.activePlayer.Cards.Add(newCard);

            GameManager.Instance.activePlayer.Cards.Sort((x, y) => 
            x.GetComponent<PlayingCard>().number.CompareTo
            (y.GetComponent<PlayingCard>().number));

            returnCard = newCard;
            GameManager.Instance.state = GameState.RoundEnd;
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
        return returnCard;
    }
}
