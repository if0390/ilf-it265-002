using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GamePhase CurrentPhase { get; private set; }
    public int PlayerCount { get; private set; } = 5;
    public List<Player> Players { get; private set; } = new List<Player>();
    public int CurrentPlayerIndex { get; private set; } = 0;
    public WorldState CurrentWorldState { get; private set; } = WorldState.Dream;
    public int CurrentRound { get; private set; } = 1;
    public bool RoleRevealComplete { get; private set; } = false;
    public bool VotingHappened { get; private set; } = false;
    public WinResult Winner { get; private set; }

    private List<CardType> deck = new List<CardType>();
    public List<CardType> ConsumedPile { get; private set; } = new List<CardType>();
    public int DeckCount => deck.Count;

    public HashSet<BoardLocation> LockedLocations { get; private set; } = new HashSet<BoardLocation>();

    [SerializeField] private UIManager uiManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    private void Start()
    {
        SetPhase(GamePhase.Title);
    }

    public void SetPhase(GamePhase newPhase)
    {
        CurrentPhase = newPhase;
        Debug.Log($"[GameManager] Phase changed to: {newPhase}");

        if (uiManager != null) uiManager.ShowPanelForPhase(newPhase);
        else Debug.LogWarning("[GameManager] UIManager reference missing in Inspector.");
    }

    public void SetPlayerCount(int count)
    {
        PlayerCount = count;
        Debug.Log($"[GameManager] Player count set to: {count}");
    }

    public void SetPlayers(List<Player> players)
    {
        Players = players;
        CurrentPlayerIndex = 0;
        Debug.Log($"[GameManager] {players.Count} players initialized.");
    }

    public Player GetCurrentPlayer()
    {
        if (Players.Count == 0) return null;
        return Players[CurrentPlayerIndex];
    }

    public void AdvanceToNextPlayer()
    {
        CurrentPlayerIndex++;
        Debug.Log($"[GameManager] Advanced to player index: {CurrentPlayerIndex}");
    }

    public bool AllPlayersDone()
    {
        return CurrentPlayerIndex >= Players.Count;
    }

    public void ResetToFirstPlayer()
    {
        CurrentPlayerIndex = 0;
    }

    public void FlipWorldState()
    {
        CurrentWorldState = CurrentWorldState == WorldState.Dream ? WorldState.Nightmare : WorldState.Dream;
        Debug.Log($"[GameManager] World flipped to: {CurrentWorldState}");
    }

    public void AdvanceRound()
    {
        CurrentRound++;
        LockedLocations.Clear();
        Debug.Log($"[GameManager] Advanced to round: {CurrentRound}");
    }

    public void LockLocation(BoardLocation loc)
    {
        LockedLocations.Add(loc);
        Debug.Log($"[GameManager] Locked location: {loc}");
    }

    public void MarkRoleRevealComplete()
    {
        RoleRevealComplete = true;
    }

    public void MarkVotingHappened()
    {
        VotingHappened = true;
    }

    public WinResult CheckWinConditions()
    {
        foreach (var p in Players)
        {
            if (p.IsAlive && p.Role == PlayerRole.Dreamer
                && p.CurrentLocation == BoardLocation.MirrorGate
                && p.FragmentsCollected >= 3)
            {
                return new WinResult(PlayerRole.Dreamer,
                    $"Player {p.PlayerNumber} ({p.Archetype}) escaped through the Mirror Gate with 3 Dream Fragments. The Dreamers wake up.");
            }
        }

        if (ConsumedPile.Count >= 20)
        {
            return new WinResult(PlayerRole.Nightmare,
                $"The Consumed pile filled to {ConsumedPile.Count}. Wonderland devours the Dreamers — the Nightmares win.");
        }

        int dreamers = 0;
        foreach (var p in Players)
        {
            if (p.IsAlive && p.Role == PlayerRole.Dreamer) dreamers++;
        }
        if (dreamers == 0 && Players.Count > 0)
        {
            return new WinResult(PlayerRole.Nightmare,
                "All Dreamers have been woken up. Wonderland falls silent — the Nightmares win.");
        }

        return null;
    }

    public void DeclareWinner(WinResult result)
    {
        Winner = result;
        SetPhase(GamePhase.GameOver);
    }

    public void ResetForNewGame()
    {
        Players = new List<Player>();
        CurrentPlayerIndex = 0;
        CurrentWorldState = WorldState.Dream;
        CurrentRound = 1;
        RoleRevealComplete = false;
        VotingHappened = false;
        Winner = null;
        deck.Clear();
        ConsumedPile.Clear();
        LockedLocations.Clear();
    }

    public void StartGameplay()
    {
        BuildAndShuffleDeck();
        DealInitialHands(3);
        CurrentRound = 1;
        ResetToFirstPlayer();
        SetPhase(GamePhase.Gameplay);
    }

    private void BuildAndShuffleDeck()
    {
        deck.Clear();
        var allCards = new[]
        {
            CardType.DreamFragment,
            CardType.WorldFlip,
            CardType.GuidingLight,
            CardType.LockedDoor,
            CardType.FadingMemory
        };
        foreach (var c in allCards)
            for (int i = 0; i < 5; i++) deck.Add(c);

        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var tmp = deck[i];
            deck[i] = deck[j];
            deck[j] = tmp;
        }
        Debug.Log($"[GameManager] Deck built and shuffled: {deck.Count} cards");
    }

    private void DealInitialHands(int handSize)
    {
        foreach (var p in Players)
        {
            p.Hand.Clear();
            for (int i = 0; i < handSize; i++)
            {
                var card = DrawCard();
                if (card.HasValue) p.Hand.Add(card.Value);
            }
        }
        Debug.Log($"[GameManager] Dealt initial hands of {handSize} to {Players.Count} players");
    }

    public CardType? DrawCard()
    {
        if (deck.Count == 0) return null;
        var card = deck[deck.Count - 1];
        deck.RemoveAt(deck.Count - 1);
        return card;
    }

    public void ConsumeCard(CardType card)
    {
        ConsumedPile.Add(card);
        Debug.Log($"[GameManager] Consumed {card} (pile: {ConsumedPile.Count})");
    }
}