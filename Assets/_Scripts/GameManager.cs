using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public Vector3 selectedCardPos;
    public GameObject grayScreen;
    public static GameObject playingCard;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SelectedCard(GameObject card)
    {
        grayScreen.SetActive(true);
        playingCard = card;
        Vector3 oldScale = card.transform.localScale;
        Vector3 newScale = oldScale * 1.3f;
        selectedCardPos = card.transform.position;
        card.transform.position = new Vector3(0,0,-1);
        card.transform.localScale = newScale;
    }
}
