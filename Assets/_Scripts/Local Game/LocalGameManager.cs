using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocalGameManager : MonoBehaviour
{
    private static LocalGameManager _instance;
    public static LocalGameManager Instance { get { return _instance; } }

    [HideInInspector]
    public GameObject newCard;
    [HideInInspector]
    public GameObject selectedCard;
    [HideInInspector]
    public Vector3 selectedCardPos;

    public int playerCount;
    public int gameTurn = 0;
    public Text scoreText;
    public GameState state;
    public PlayerData activePlayer;
    public GameObject grayScreen;
    public GameObject doneButton;
    public GameObject buttonPanel;
    public GameObject playerButton;
    public Text gameResult;
    public List<PlayerData> players = new List<PlayerData>();
    public CardAnimations animator = new CardAnimations();
    public GenerateDeck generateDeck = new GenerateDeck();

    private PlayerData targetedPlayer;
    //private SaveData saveData = new SaveData();
    //private JsonSTest jsonSTest = new JsonSTest();
    private GameEnd gameEnd = new GameEnd();
    private CardSteal cardsteal = new CardSteal();
    private PointGain pointGain = new PointGain();
    private UpdateHand updateHand = new UpdateHand();
    private List<Button> buttons = new List<Button>();
    private PointCompare pointCompare = new PointCompare();
    private ScoreDisplay scoreDisplay = new ScoreDisplay();
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
                SceneManager.LoadScene("WinScreen");
                pointCompare.GetWinner(players);
                break;
        }
    }

    public void DeclareWinner()
    {
       Text scoreResult = Instantiate(gameResult, Vector3.zero, Quaternion.identity, 
            GameObject.FindGameObjectWithTag("Canvas").transform);
        scoreResult.transform.localPosition = Vector3.zero;
        scoreDisplay.DisplayScore(scoreResult);
    }

    public void PlayerCount()
    {
        playerCount = LocalPlayMenu.playerAmount;
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

        int nextPlayer = (gameTurn + 1) % players.Count;
        scoreText.text = players[nextPlayer].Name + " Score: " + players[nextPlayer].Points;
    }

    public void SwitchPlayer()
    {
        cardSwitchPlayer.SwitchPlayer(activePlayer, gameTurn, players);
        if (activePlayer.Cards.Count == 0 && GetComponent<CardDealer>().cardPool.Count > 0)
        {
            newCard = GetComponent<CardDealer>().Deal();
            activePlayer.Cards.Add(newCard);
            newCard.SetActive(true);
        }
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
        foreach (PlayerData player in players)
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
            

        if (activePlayer.Cards.Count == 0 && GetComponent<CardDealer>().cardPool.Count > 0)
        {
            newCard = GetComponent<CardDealer>().Deal();
            newCard.transform.rotation = Quaternion.Euler(0, 0, 0);
            activePlayer.Cards.Add(newCard);
            newCard.SetActive(true);
        }
        scoreText.text = activePlayer.Name + " Score: " + activePlayer.Points;

        gameEnd.WinCondition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            activePlayer.Points += 1;

            scoreText.text = activePlayer.Name + " Score: " + activePlayer.Points;

            gameEnd.WinCondition();
        }
    }

    public Vector3 CardSpawnLocations(float position, int zPos)
    {
        Vector3 screenScale = new Vector3(Screen.width / activePlayer.Cards.Count * position, Screen.height / 5, 0);
        Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
        cardPos.z = zPos;
        return cardPos;
    }
}