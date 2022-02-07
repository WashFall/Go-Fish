using UnityEngine;
using DG.Tweening;

public class CardAnimations
{
    public void CardRotate(GameObject card)
    {
        card.transform.DORotate(new Vector3(0, 180, 0), 0.5f, RotateMode.LocalAxisAdd);
    }

    public void ShowHand(Player activePlayer)
    {
        float position = 0.5f;
        foreach (GameObject card in activePlayer.Cards)
        {
            Vector3 cardPos = GameManager.Instance.CardSpawnLocations(position);
            card.SetActive(true);
            card.transform.DOMove(cardPos, 0.3f).SetEase(Ease.InQuad);
            CardRotate(card);
            position++;
        }
    }

    public void CardShake(Player activePlayer)
    {
        float position = 0.5f;
        foreach (GameObject card in activePlayer.Cards)
        {
            Vector3 cardPos = GameManager.Instance.CardSpawnLocations(position);
            Sequence animationSequence = DOTween.Sequence();

            //animationSequence.Append(card.transform.DOShakePosition(0.1f, 0.9f));
            animationSequence.Append(card.transform.DOMove(cardPos, 0.3f).SetEase(Ease.InQuad));

            animationSequence.Play();
            position++;
        }
    }

    public void CardGroup(GameObject card)
    {
        card.transform.DOMoveX(0, 0.3f).SetEase(Ease.InQuad);
    }
}
