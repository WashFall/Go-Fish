using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    PlayerController playerController;
    Vector3 cardScale;
    bool large = false;
    private delegate void OnCardSelect(GameObject card);
    OnCardSelect onCardSelect;

    private void Start()
    {
        cardScale = transform.localScale;
        //onCardSelect += playerController.CardPick;
        onCardSelect += GameManager.Instance.OnCardSelected;
    }

    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && GameManager.Instance.playingCard == null)
        {
            large = true;
            GameManager.Instance.SelectedCard(gameObject);
            onCardSelect(gameObject);
        }
        else if(Physics.Raycast(ray,out hit) && large)
        {
            transform.localScale = cardScale;
            transform.position = GameManager.Instance.selectedCardPos;
            large = false;
            GameManager.Instance.playingCard = null;
            GameManager.Instance.grayScreen.SetActive(false);
        }
    }
}
