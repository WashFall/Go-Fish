using System.Collections.Generic;
using UnityEngine;

public class CardSteal
{
    public GameObject StealCards(GameObject card, PlayerData targetedPlayer)
    {
        List<int> cardIndex = new List<int>();
        GameObject returnCard = null;
        foreach (GameObject c in targetedPlayer.Cards)
        {
            if (c.GetComponent<PlayingCard>().number == card.GetComponent<PlayingCard>().number)
            {
                LocalGameManager.Instance.activePlayer.Cards.Add(c);

                LocalGameManager.Instance.activePlayer.Cards.Sort((x, y) =>
                x.GetComponent<PlayingCard>().number.CompareTo(
                y.GetComponent<PlayingCard>().number));

                cardIndex.Add(targetedPlayer.Cards.IndexOf(c));
                c.transform.rotation = Quaternion.Euler(0, 1, 0);
                returnCard = c;
            }
        }

        if(cardIndex.Count == 0 && LocalGameManager.Instance.GetComponent<CardDealer>().cardPool.Count > 0)
        {
            GameObject newCard = LocalGameManager.Instance.GetComponent<CardDealer>().Deal();
            newCard.transform.rotation = Quaternion.Euler(0, 1, 0);
            LocalGameManager.Instance.activePlayer.Cards.Add(newCard);

            LocalGameManager.Instance.activePlayer.Cards.Sort((x, y) => 
            x.GetComponent<PlayingCard>().number.CompareTo
            (y.GetComponent<PlayingCard>().number));

            returnCard = newCard;
            LocalGameManager.Instance.state = GameState.RoundEnd;
        }
        else if (cardIndex.Count == 0 && LocalGameManager.Instance.GetComponent<CardDealer>().cardPool.Count == 0)
        {
            returnCard = null;
            LocalGameManager.Instance.state = GameState.RoundEnd;
        }

        cardIndex.Sort((a, b) => b.CompareTo(a));

        foreach (int i in cardIndex)
        {
            targetedPlayer.Cards.RemoveAt(i);
        }

        foreach (GameObject c in LocalGameManager.Instance.activePlayer.Cards)
        {
            card.SetActive(false);
        }
        return returnCard;
    }
}
