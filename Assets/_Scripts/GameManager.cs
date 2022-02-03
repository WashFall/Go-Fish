using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int gameTurn = 0;
    public Player activePlayer;
    public GameObject grayScreen;
    public GameObject buttonPanel;
    public GameObject playerButton;
    public GameObject selectedCard;
    public Vector3 selectedCardPos;
    public List<Player> players = new List<Player>();

    private CardDealer dealer;
    private CardAnimations animator = new CardAnimations();
    private CardSteal cardsteal = new CardSteal();
    private CardShowHand cardShowHand = new CardShowHand();
    private CardSwitchPlayer cardSwitchPlayer = new CardSwitchPlayer();
    private Player targetedPlayer;
    private List<Button> buttons = new List<Button>();

    private delegate void PlayerCountListeners(List<Player> players);
    private PlayerCountListeners playerCountListeners;

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
        //playerController = GetComponent<PlayerController>();
        dealer = GetComponent<CardDealer>();
        AddPlayerCountListeners();
    }

    private void AddPlayerCountListeners()
    {
        playerCountListeners += dealer.PlayerCountListener;
    }

    public void PlayerCount(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            Player player = new Player();
            players.Add(player);
        }
        activePlayer = players[0];
        playerCountListeners(players);
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
        cardShowHand.ShowHand(activePlayer);
    }

    public void TurnCards()
    {
        foreach (Player player in players)
        {
            foreach (GameObject card in player.Cards)
            {
                animator.CardRotate(card);
            }
        }
    }

    public void SwitchPlayer()
    {
        cardSwitchPlayer.SwitchPlayer(activePlayer, gameTurn, players);
        ShowHand();
        foreach (GameObject card in activePlayer.Cards)
            animator.CardShake(card);
        gameTurn++;
    }

    public void SelectedCard(GameObject card)
    {
        selectedCard = card;
        selectedCardPos = card.transform.position;
        card.transform.position = new Vector3(selectedCardPos.x, selectedCardPos.y, -1);
        grayScreen.SetActive(true);
        Vector3 oldScale = card.transform.localScale;
        card.transform.DOMove(new Vector3(0, 0, -1), 0.2f).SetEase(Ease.OutSine);
        card.transform.DOScale(oldScale * 1.3f, 0.2f);
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

        cardShowHand.ShowHand(GameManager.Instance.activePlayer);
    }
}