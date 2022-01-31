using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public Player activePlayer;
    public GameObject grayScreen;
    public GameObject playingCard;
    public Vector3 selectedCardPos;

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
        Application.targetFrameRate = 90;
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

    public void OnCardSelected(GameObject card)
    {
        print(card.GetComponent<PlayingCard>().cardName);
    }
}

public enum GameState
{
    ViewCards,
    SelectCard,

}
