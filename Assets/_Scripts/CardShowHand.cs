using UnityEngine;

public class CardShowHand
{
    public void ShowHand(Player player)
    {
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
}
