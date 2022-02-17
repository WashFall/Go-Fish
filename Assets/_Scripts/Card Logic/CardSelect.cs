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
        onCardSelect += LocalGameManager.Instance.OnCardSelected;
    }

    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(LocalGameManager.Instance.state == GameState.RoundActive)
        {
            if (Physics.Raycast(ray, out hit) && LocalGameManager.Instance.selectedCard == null)
            {
                large = true;
                LocalGameManager.Instance.SelectedCard(gameObject);
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
        if(LocalGameManager.Instance.state != GameState.WinState)
        {
            LocalGameManager.Instance.animator.CardDeSelectMove(LocalGameManager.Instance.selectedCard, cardScale);
            large = false;
            LocalGameManager.Instance.doneButton.SetActive(true);
            LocalGameManager.Instance.buttonPanel.SetActive(false);
            LocalGameManager.Instance.selectedCard = null;
            LocalGameManager.Instance.grayScreen.SetActive(false);
        }
    }
}
