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
        if(GameManager.Instance.state == GameState.RoundActive)
        {
            if (Physics.Raycast(ray, out hit) && GameManager.Instance.selectedCard == null)
            {
                large = true;
                GameManager.Instance.SelectedCard(gameObject);
                onCardSelect(gameObject);
            }
            else if(Physics.Raycast(ray,out hit) && large)
            {
                DeselectCard();
            }
        }
    }

    public void DeselectCard()
    {
        GameManager.Instance.animator.CardDeSelectMove(GameManager.Instance.selectedCard, cardScale);
        large = false;
        GameManager.Instance.doneButton.SetActive(true);
        GameManager.Instance.buttonPanel.SetActive(false);
        GameManager.Instance.selectedCard = null;
        GameManager.Instance.grayScreen.SetActive(false);
    }
}
