using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayPanel : MonoBehaviour
{
    [Header("Header")]
    [SerializeField] private TMP_Text playersOverviewText;
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text locationText;
    [SerializeField] private TMP_Text actionStatusText;

    [Header("Containers")]
    [SerializeField] private Transform moveButtonContainer;
    [SerializeField] private Transform cardButtonContainer;

    [Header("Buttons")]
    [SerializeField] private Button endTurnButton;

    [Header("Prefabs")]
    [SerializeField] private GameObject moveButtonPrefab;
    [SerializeField] private GameObject cardButtonPrefab;

    [Header("AI Pacing")]
    [SerializeField] private float aiActionDelay = 1.0f;

    private const int HumanSeat = 1;

    private bool hasMoved;
    private bool hasPlayedCard;

    private void OnEnable()
    {
        StartCurrentPlayerTurn();
    }

    private void StartCurrentPlayerTurn()
    {
        hasMoved = false;
        hasPlayedCard = false;

        var player = GameManager.Instance.GetCurrentPlayer();
        UpdateHeader();
        UpdatePlayersOverview();

        if (player.PlayerNumber == HumanSeat)
        {
            SetHumanUIActive(true);
            RefreshHumanUI();
        }
        else
        {
            SetHumanUIActive(false);
            ClearContainer(moveButtonContainer);
            ClearContainer(cardButtonContainer);
            StartCoroutine(PlayAITurn(player));
        }
    }

    private IEnumerator PlayAITurn(Player player)
    {
        SetActionStatus($"<b>Player {player.PlayerNumber} ({player.Archetype})</b> is taking their turn...");
        yield return new WaitForSeconds(aiActionDelay);

        if (Random.value > 0.5f)
        {
            var connections = LocationGraph.GetConnections(player.CurrentLocation);
            var available = new List<BoardLocation>();
            foreach (var loc in connections)
                if (!GameManager.Instance.LockedLocations.Contains(loc)) available.Add(loc);

            if (available.Count > 0)
            {
                var dest = available[Random.Range(0, available.Count)];
                Debug.Log($"[AI] Player {player.PlayerNumber} moves {player.CurrentLocation} -> {dest}");
                player.CurrentLocation = dest;
                SetActionStatus($"Player {player.PlayerNumber} moved to <b>{LocationGraph.GetDisplayName(dest)}</b>.");
                UpdatePlayersOverview();
                yield return new WaitForSeconds(aiActionDelay);
            }
            else
            {
                SetActionStatus($"Player {player.PlayerNumber} found every path locked.");
                yield return new WaitForSeconds(aiActionDelay);
            }
        }
        else
        {
            SetActionStatus($"Player {player.PlayerNumber} stayed at {LocationGraph.GetDisplayName(player.CurrentLocation)}.");
            yield return new WaitForSeconds(aiActionDelay);
        }

        if (Random.value > 0.5f && player.Hand.Count > 0)
        {
            var card = player.Hand[Random.Range(0, player.Hand.Count)];
            player.Hand.Remove(card);
            var effectMsg = CardEffects.Resolve(card, player);
            GameManager.Instance.ConsumeCard(card);
            Debug.Log($"[AI] Player {player.PlayerNumber} played {card}: {effectMsg}");
            SetActionStatus($"Player {player.PlayerNumber} played <b>{CardInfo.GetDisplayName(card)}</b> — {effectMsg}.");
            UpdateHeader();
            UpdatePlayersOverview();
            yield return new WaitForSeconds(aiActionDelay);
        }
        else
        {
            SetActionStatus($"Player {player.PlayerNumber} did not play a card.");
            yield return new WaitForSeconds(aiActionDelay);
        }

        var drawn = GameManager.Instance.DrawCard();
        if (drawn.HasValue) player.Hand.Add(drawn.Value);
        UpdateHeader();

        SetActionStatus($"Player {player.PlayerNumber} ended their turn.");
        yield return new WaitForSeconds(aiActionDelay);

        AdvanceTurn();
    }

    public void OnEndTurnClicked()
    {
        var player = GameManager.Instance.GetCurrentPlayer();
        if (player.PlayerNumber != HumanSeat) return;

        var drawn = GameManager.Instance.DrawCard();
        if (drawn.HasValue) player.Hand.Add(drawn.Value);

        AdvanceTurn();
    }

    private void AdvanceTurn()
    {
        var winner = GameManager.Instance.CheckWinConditions();
        if (winner != null)
        {
            GameManager.Instance.DeclareWinner(winner);
            return;
        }

        GameManager.Instance.AdvanceToNextPlayer();

        for (int safety = 0; safety < 20; safety++)
        {
            if (GameManager.Instance.AllPlayersDone())
            {
                if (GameManager.Instance.CurrentRound >= 3 && !GameManager.Instance.VotingHappened)
                {
                    GameManager.Instance.SetPhase(GamePhase.Voting);
                    return;
                }
                GameManager.Instance.ResetToFirstPlayer();
                GameManager.Instance.AdvanceRound();
            }
            var p = GameManager.Instance.GetCurrentPlayer();
            if (p != null && p.IsAlive) break;
            GameManager.Instance.AdvanceToNextPlayer();
        }

        StartCurrentPlayerTurn();
    }

    private void RefreshHumanUI()
    {
        ClearContainer(moveButtonContainer);
        ClearContainer(cardButtonContainer);

        var player = GameManager.Instance.GetCurrentPlayer();

        if (locationText != null)
            locationText.text = $"You are at: <b>{LocationGraph.GetDisplayName(player.CurrentLocation)}</b>";

        if (!hasMoved)
        {
            foreach (var loc in LocationGraph.GetConnections(player.CurrentLocation))
            {
                if (GameManager.Instance.LockedLocations.Contains(loc)) continue;
                BoardLocation captured = loc;
                CreateButton(moveButtonPrefab, moveButtonContainer,
                    LocationGraph.GetDisplayName(loc),
                    () => OnMoveClicked(captured));
            }
        }

        if (!hasPlayedCard)
        {
            foreach (var card in player.Hand)
            {
                CardType captured = card;
                CreateButton(cardButtonPrefab, cardButtonContainer,
                    CardInfo.GetDisplayName(card),
                    () => OnCardClicked(captured));
            }
        }

        var pending = new List<string>();
        if (!hasMoved) pending.Add("Move");
        if (!hasPlayedCard) pending.Add("Play Card");
        SetActionStatus(pending.Count == 0
            ? "Ready to end turn."
            : "Optional this turn: " + string.Join(", ", pending));
    }

    private void OnMoveClicked(BoardLocation destination)
    {
        if (hasMoved) return;
        var player = GameManager.Instance.GetCurrentPlayer();
        Debug.Log($"[GameplayPanel] Player {player.PlayerNumber} moved {player.CurrentLocation} -> {destination}");
        player.CurrentLocation = destination;
        hasMoved = true;
        UpdatePlayersOverview();
        RefreshHumanUI();
    }

    private void OnCardClicked(CardType card)
    {
        if (hasPlayedCard) return;
        var player = GameManager.Instance.GetCurrentPlayer();
        if (!player.Hand.Remove(card)) return;
        var effectMsg = CardEffects.Resolve(card, player);
        GameManager.Instance.ConsumeCard(card);
        Debug.Log($"[GameplayPanel] Player {player.PlayerNumber} played {card}: {effectMsg}");
        hasPlayedCard = true;
        UpdateHeader();
        UpdatePlayersOverview();
        RefreshHumanUI();
        SetActionStatus($"You played <b>{CardInfo.GetDisplayName(card)}</b> — {effectMsg}.");
    }

    private void UpdateHeader()
    {
        if (headerText == null) return;
        var gm = GameManager.Instance;
        var human = gm.Players.Count > 0 ? gm.Players[0] : null;
        string frags = human != null ? $"Fragments: {human.FragmentsCollected}/3" : "";
        headerText.text = $"Round {gm.CurrentRound}    World: <b>{gm.CurrentWorldState}</b>    {frags}    Consumed: {gm.ConsumedPile.Count}/20    Deck: {gm.DeckCount}";
    }

    private void UpdatePlayersOverview()
    {
        if (playersOverviewText == null) return;
        var sb = new StringBuilder();
        sb.AppendLine("<b>Players</b>");
        foreach (var p in GameManager.Instance.Players)
        {
            var marker = p.PlayerNumber == HumanSeat ? " (you)" : "";
            var status = p.IsAlive ? "" : " — eliminated";
            sb.AppendLine($"P{p.PlayerNumber}: {p.Archetype}{marker} @ {LocationGraph.GetDisplayName(p.CurrentLocation)}{status}");
        }
        playersOverviewText.text = sb.ToString();
    }

    private void SetActionStatus(string text)
    {
        if (actionStatusText != null) actionStatusText.text = text;
    }

    private void SetHumanUIActive(bool active)
    {
        if (moveButtonContainer != null) moveButtonContainer.gameObject.SetActive(active);
        if (cardButtonContainer != null) cardButtonContainer.gameObject.SetActive(active);
        if (endTurnButton != null) endTurnButton.gameObject.SetActive(active);
        if (locationText != null) locationText.gameObject.SetActive(active);
    }

    private static void CreateButton(GameObject prefab, Transform parent, string label, UnityEngine.Events.UnityAction onClick)
    {
        if (prefab == null || parent == null) return;
        var go = Instantiate(prefab, parent);
        var text = go.GetComponentInChildren<TMP_Text>();
        if (text != null) text.text = label;
        var btn = go.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(onClick);
        }
    }

    private static void ClearContainer(Transform container)
    {
        if (container == null) return;
        for (int i = container.childCount - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }
}
