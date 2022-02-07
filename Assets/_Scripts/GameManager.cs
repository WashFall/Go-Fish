using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int playerCount;
    public int gameTurn = 0;
    public Player activePlayer;
    public GameObject grayScreen;
    public GameObject buttonPanel;
    public GameObject playerButton;
    public GameObject selectedCard;
    public Vector3 selectedCardPos;
    public List<Player> players = new List<Player>();
    public CardAnimations animator = new CardAnimations();
    public GenerateDeck generateDeck = new GenerateDeck();

    private Player targetedPlayer;
    //private SaveData saveData = new SaveData();
    //private JsonSTest jsonSTest = new JsonSTest();
    private CardSteal cardsteal = new CardSteal();
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
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        string savedata = saveData.Save(activePlayer);
    //        jsonSTest.Save(activePlayer.Name, savedata);
    //    }
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        jsonSTest.Load();
    //    }
    //}

    public void PlayerCount(int playerCount)
    {
        this.playerCount = playerCount;
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
        animator.ShowHand(activePlayer);
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
        foreach (Player player in players)
        {
            if (player.Name == button.GetComponentInChildren<Text>().text)
            { 
                targetedPlayer = player;
                cardsteal.StealCards(selectedCard, targetedPlayer);
            }
        }

        updateHand.ShowUpdatedHand(activePlayer);
    }

    public Vector3 CardSpawnLocations(float position)
    {
        Vector3 screenScale = new Vector3(Screen.width / activePlayer.Cards.Count * position, Screen.height / 5, 0);
        Vector3 cardPos = Camera.main.ScreenToWorldPoint(screenScale);
        cardPos.z = 0;
        return cardPos;
    }
}