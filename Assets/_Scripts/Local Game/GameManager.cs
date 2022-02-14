using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { GameSetup, RoundActive, RoundEnd, WinState }

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [HideInInspector]
    public GameObject newCard;
    [HideInInspector]
    public GameObject selectedCard;
    [HideInInspector]
    public Vector3 selectedCardPos;

    public int playerCount;
    public int gameTurn = 0;
    public GameState state;
    public Text scoreText;
    public Player activePlayer;
    public GameObject grayScreen;
    public GameObject doneButton;
    public GameObject buttonPanel;
    public GameObject playerButton;
    public List<Player> players = new List<Player>();
    public CardAnimations animator = new CardAnimations();
    public GenerateDeck generateDeck = new GenerateDeck();

    private Player targetedPlayer;
    //private SaveData saveData = new SaveData();
    //private JsonSTest jsonSTest = new JsonSTest();
    private CardSteal cardsteal = new CardSteal();
    private PointGain pointGain = new PointGain();
    private UpdateHand updateHand = new UpdateHand();
    private List<Button> buttons = new List<Button>();
    private PlayerListMaker playerListMaker = new PlayerListMaker();
    private CardSwitchPlayer cardSwitchPlayer = new CardSwitchPlayer();


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Application.targetFrameRate = 90;
        state = GameState.GameSetup;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.GameSetup:
                break;
            case GameState.RoundActive:
                break;
            case GameState.RoundEnd:
                break;
            case GameState.WinState:
                break;
        }
    }

    public void PlayerCount()
    {
        this.playerCount = LocalPlayMenu.playerAmount;
        players = playerListMaker.PlayerListCreator(playerCount, players);
        PlayerButtonCreator(playerCount);
    }

    public void PlayerButtonCreator(int playerCount)
    {
        var rectTransform = buttonPanel.GetComponent<RectTransform>();
        float panelHeight = rectTransform.sizeDelta.y;
        for (int i = 1; i < playerCount; i++)
        {
            GameObject button = Instantiate(playerButton, buttonPanel.transform, false);
            button.transform.localPosition = new Vector3(0, (panelHeight / (playerCount - 1)) * i, 0);

            button.GetComponent<Button>().onClick.AddListener(() => PlayerChoice(button.GetComponent<Button>()));
        }
        buttons.AddRange(buttonPanel.GetComponentsInChildren<Button>());
    }

    public void ShowHand()
    {
        UpdateGameState(GameState.RoundActive);
        animator.ShowHand(activePlayer);
        scoreText.text = activePlayer.Name + " Score: " + activePlayer.Points;
    }

    public void TurnCards()
    {
        foreach (GameObject card in activePlayer.Cards)
        {
            animator.CardRotate(card);
        }
        
        foreach (GameObject card in activePlayer.Cards)
            animator.CardGroup(card);
    }

    public void SwitchPlayer()
    {
        cardSwitchPlayer.SwitchPlayer(activePlayer, gameTurn, players);
        ShowHand();

        animator.CardShake(activePlayer);
        gameTurn++;
    }

    public void SelectedCard(GameObject card)
    {
        selectedCard = card;
        selectedCardPos = card.transform.position;
        card.transform.position = new Vector3(selectedCardPos.x, selectedCardPos.y, -1);
        grayScreen.SetActive(true);
        doneButton.SetActive(false);
        Vector3 oldScale = card.transform.localScale;
        animator.CardSelectMove(card, oldScale);
    }

    public void OnCardSelected(GameObject card)
    {
        bool indexFix = false;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != activePlayer && !indexFix)
            {
                buttons[i].GetComponentInChildren<Text>().text = players[i].Name;
            }
            else if (players[i] != activePlayer && indexFix)
            {
                buttons[i - 1].GetComponentInChildren<Text>().text = players[i].Name;
            }
            else if (players[i] == activePlayer)
            {
                indexFix = true;
            }
        }
        buttonPanel.SetActive(true);
    }

    public void PlayerChoice(Button button)
    {
        int oldPoints = activePlayer.Points;
        foreach (Player player in players)
        {
            if (player.Name == button.GetComponentInChildren<Text>().text)
            { 
                targetedPlayer = player;
                newCard = cardsteal.StealCards(selectedCard, targetedPlayer);
            }
        }

        if(newCard != null)
            activePlayer.Points += pointGain.CheckIfFourCards(newCard);
        updateHand.ShowUpdatedHand(activePlayer);
        if(state == GameState.RoundEnd || activePlayer.Points > oldPoints)
            selectedCard.GetComponent<CardSelect>().DeselectCard();

        scoreText.text = activePlayer.Name + " Score: " + activePlayer.Points;
    }

    public Vector3 CardSpawnLocations(float position, int zPos)
    {
        Vector3 screenScale = new Vector3(Screen.width / activePlayer.Cards.Count * position, Screen.height / 5, 0);
        Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
        cardPos.z = zPos;
        return cardPos;
    }
}