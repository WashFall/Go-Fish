using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class CardAnimations
{
    public void CardRotate(GameObject card)
    {
        card.transform.DORotate(new Vector3(0, 180, 0), 0.5f, RotateMode.LocalAxisAdd);
    }

    public async void CardShake(GameObject card)
    {
        Vector3 originalPos = card.transform.position;

        var tasks = new List<Task>();
        tasks.Add(card.transform.DOMoveX(0, 0.3f).SetEase(Ease.InQuad).AsyncWaitForCompletion());

        await Task.WhenAll(tasks);

        tasks.Add(card.transform.DOShakePosition(0.1f, 0.9f).AsyncWaitForCompletion());

        await Task.WhenAll(tasks);

        tasks.Add(card.transform.DOMoveX(originalPos.x, 0.3f).SetEase(Ease.InQuad).AsyncWaitForCompletion());
    }
}
