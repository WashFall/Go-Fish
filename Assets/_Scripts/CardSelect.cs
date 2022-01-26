using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    PlayerController playerController;
    Vector3 cardScale;
    bool large = false;

    private void Start()
    {
        cardScale = transform.localScale;
    }

    private void Update()
    {
        
    }

    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && !large && GameManager.playingCard == null)
        {
            large = true;
            GameManager.Instance.SelectedCard(gameObject);
        }
        else if(Physics.Raycast(ray,out hit) && large)
        {
            transform.localScale = cardScale;
            transform.position = GameManager.Instance.selectedCardPos;
            large = false;
            GameManager.playingCard = null;
            GameManager.Instance.grayScreen.SetActive(false);
        }
    }
}
