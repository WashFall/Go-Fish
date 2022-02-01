using DG.Tweening;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    Vector3 cardScale;
    bool large = false;
    private delegate void OnCardSelect(GameObject card);
    OnCardSelect onCardSelect;

    private void Start()
    {
        cardScale = transform.localScale;
        onCardSelect += GameManager.Instance.OnCardSelected;
    }

    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && GameManager.Instance.selectedCard == null)
        {
            large = true;
            GameManager.Instance.SelectedCard(gameObject);
            onCardSelect(gameObject);
        }
        else if(Physics.Raycast(ray,out hit) && large)
        {
            transform.DOMove(GameManager.Instance.selectedCardPos, 0.2f).SetEase(Ease.OutSine);
            transform.DOScale(cardScale, 0.2f);
            large = false;
            GameManager.Instance.buttonPanel.SetActive(false);
            GameManager.Instance.selectedCard = null;
            GameManager.Instance.grayScreen.SetActive(false);
        }
    }
}
