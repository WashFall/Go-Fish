using UnityEngine;
using System.Collections.Generic;

public class PointGain
{
    public int CheckIfFourCards(GameObject newCard)
    {
        List<GameObject> fourCards = new List<GameObject>();
        int getPoint = 0;
        foreach(GameObject card in GameManager.Instance.activePlayer.Cards)
        {
            if(card.GetComponent<PlayingCard>().number == newCard.GetComponent<PlayingCard>().number)
            {
                getPoint++;
                fourCards.Add(card);
            }
        }

        if (getPoint == 4)
        {
            foreach(GameObject card in fourCards)
            {
                GameManager.Instance.activePlayer.Cards.Remove(card);
                card.SetActive(false);
            }
            return 1;
        }
        else
            return 0;
    }
}
