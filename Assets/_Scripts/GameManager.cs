using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int gameTurn = 0;
    public Player activePlayer;
    public GameObject grayScreen;
    public GameObject selectedCard;
    public Vector3 selectedCardPos;
    public List<Player> players = new List<Player>();

    private CardDealer dealer;
    private PlayerController playerController;
    private delegate void PlayerCountListeners(List<Player> players);
    private PlayerCountListeners playerCountListeners;

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
        playerController = GetComponent<PlayerController>();
        dealer = GetComponent<CardDealer>();
        AddPlayerCountListeners();
    }

    private void AddPlayerCountListeners()
    {
        playerCountListeners += playerController.PlayerCountListener;
        playerCountListeners += dealer.PlayerCountListener;
    }

    public void PlayerCount(int playerCount)
    {
        for(int i = 0; i < playerCount; i++)
        {
            Player player = new Player();
            players.Add(player);
        }
        activePlayer = players[0];
        playerCountListeners(players);
    }

    public void ShowHand()
    {
        playerController.ShowHand(activePlayer);
    }

    public void TurnCards()
    {
        playerController.TurnCards();
    }

    public void SwitchPlayer()
    {
        playerController.SwitchPlayer(activePlayer, gameTurn);
        gameTurn++;
    }

    public void SelectedCard(GameObject card)
    {
        selectedCardPos = card.transform.position;
        card.transform.position = new Vector3(selectedCardPos.x, selectedCardPos.y, -1);
        grayScreen.SetActive(true);
        selectedCard = card;
        Vector3 oldScale = card.transform.localScale;
        card.transform.DOMove(new Vector3(0, 0, -1), 0.2f).SetEase(Ease.OutSine);
        card.transform.DOScale(oldScale * 1.3f, 0.2f);
        //Vector3 newScale = oldScale * 1.3f;
        //card.transform.localScale = newScale;
    }

    public void OnCardSelected(GameObject card)
    {
        
    }
}

public enum GameState
{
    ViewCards,
    SelectCard,

}
