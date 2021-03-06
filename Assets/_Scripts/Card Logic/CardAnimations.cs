using UnityEngine;
using DG.Tweening;

public class CardAnimations
{
    public void CardRotate(GameObject card)
    {
        card.transform.DORotate(new Vector3(0, 180, 0), 0.5f, RotateMode.LocalAxisAdd);
    }

    public void ShowHand(PlayerData activePlayer)
    {
        float position = 0.5f;
        int zPos = activePlayer.Cards.Count;
        foreach (GameObject card in activePlayer.Cards)
        {
            Vector3 cardPos = LocalGameManager.Instance.CardSpawnLocations(position, zPos);
            card.SetActive(true);
            card.transform.DOMove(cardPos, 0.3f).SetEase(Ease.InQuad);
            CardRotate(card);
            position++;
            zPos--;
        }
    }

    public void CardShake(PlayerData activePlayer)
    {
        float position = 0.5f;
        int zPos = activePlayer.Cards.Count;
        foreach (GameObject card in activePlayer.Cards)
        {
            Vector3 cardPos = LocalGameManager.Instance.CardSpawnLocations(position, zPos);

            card.transform.DOMove(cardPos, 0.3f).SetEase(Ease.InQuad);

            position++;
            zPos--;
        }
    }

    public void CardGroup(GameObject card)
    {
        card.transform.DOMoveX(0, 0.3f).SetEase(Ease.InQuad);
    }

    public void CardSelectMove(GameObject card, Vector3 oldScale)
    {
        card.transform.DOMove(new Vector3(0, 0, -1), 0.2f).SetEase(Ease.OutSine);
        card.transform.DOScale(oldScale * 1.3f, 0.2f);
    }

    public void CardDeSelectMove(GameObject card, Vector3 cardScale)
    {
        card.transform.DOMove(LocalGameManager.Instance.selectedCardPos, 0.2f).SetEase(Ease.OutSine);
        card.transform.DOScale(cardScale, 0.2f);
    }
}
