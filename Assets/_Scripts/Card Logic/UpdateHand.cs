using UnityEngine;

public class UpdateHand
{
    public void ShowUpdatedHand(PlayerData activePlayer)
    {
        float position = 0.5f;
        int zPos = activePlayer.Cards.Count;
        foreach (GameObject card in activePlayer.Cards)
        {
            Vector3 screenScale = new Vector3(Screen.width / activePlayer.Cards.Count * position, Screen.height / 5, 0);
            Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
            cardPos.z = zPos;
            card.SetActive(true);
            if(card == LocalGameManager.Instance.selectedCard)
                LocalGameManager.Instance.selectedCardPos = cardPos;
            else
                card.transform.position = cardPos;
            position++;
            zPos--;
        }
    }
}
