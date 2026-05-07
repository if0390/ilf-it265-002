using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VotingPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Transform voteButtonContainer;
    [SerializeField] private GameObject voteButtonPrefab;
    [SerializeField] private Button continueButton;

    private const int HumanSeat = 1;
    private bool votingComplete;

    private void OnEnable()
    {
        votingComplete = false;
        ClearContainer(voteButtonContainer);
        if (continueButton != null) continueButton.gameObject.SetActive(false);
        if (resultText != null) resultText.text = "";

        if (headerText != null)
            headerText.text = "<b>Voting Round</b>\nPick a player to wake up.";

        foreach (var p in GameManager.Instance.Players)
        {
            if (!p.IsAlive) continue;
            if (p.PlayerNumber == HumanSeat) continue;

            Player captured = p;
            var label = $"P{p.PlayerNumber}: {p.Archetype}";
            CreateButton(voteButtonPrefab, voteButtonContainer, label, () => OnVoteClicked(captured));
        }
    }

    private void OnVoteClicked(Player target)
    {
        if (votingComplete) return;
        votingComplete = true;
        ClearContainer(voteButtonContainer);

        var votes = new Dictionary<int, int>();
        AddVote(votes, target.PlayerNumber);
        Debug.Log($"[Voting] You voted for Player {target.PlayerNumber}");

        foreach (var p in GameManager.Instance.Players)
        {
            if (!p.IsAlive) continue;
            if (p.PlayerNumber == HumanSeat) continue;

            var candidates = new List<Player>();
            foreach (var c in GameManager.Instance.Players)
                if (c.IsAlive && c.PlayerNumber != p.PlayerNumber) candidates.Add(c);
            if (candidates.Count == 0) continue;

            var pick = candidates[Random.Range(0, candidates.Count)];
            AddVote(votes, pick.PlayerNumber);
            Debug.Log($"[Voting] Player {p.PlayerNumber} voted for Player {pick.PlayerNumber}");
        }

        int eliminatedSeat = -1;
        int max = 0;
        foreach (var kvp in votes)
        {
            if (kvp.Value > max) { max = kvp.Value; eliminatedSeat = kvp.Key; }
        }

        var sb = new StringBuilder();
        sb.AppendLine("<b>Vote tally:</b>");
        foreach (var p in GameManager.Instance.Players)
        {
            int n = votes.TryGetValue(p.PlayerNumber, out var v) ? v : 0;
            var youTag = p.PlayerNumber == HumanSeat ? " (you)" : "";
            sb.AppendLine($"P{p.PlayerNumber}{youTag}: {n} vote(s)");
        }
        sb.AppendLine();

        if (eliminatedSeat >= 1)
        {
            var elim = GameManager.Instance.Players[eliminatedSeat - 1];
            elim.IsAlive = false;
            sb.AppendLine($"<b>Player {elim.PlayerNumber} ({elim.Archetype})</b> was woken up.");
            sb.AppendLine($"They were a <b>{elim.GetRoleName()}</b>.");
            Debug.Log($"[Voting] Eliminated Player {elim.PlayerNumber} ({elim.GetRoleName()})");
        }
        else
        {
            sb.AppendLine("Tie — no one eliminated.");
        }

        if (resultText != null) resultText.text = sb.ToString();
        GameManager.Instance.MarkVotingHappened();
        if (continueButton != null) continueButton.gameObject.SetActive(true);
    }

    public void OnContinueButtonClicked()
    {
        var winner = GameManager.Instance.CheckWinConditions();
        if (winner != null)
        {
            GameManager.Instance.DeclareWinner(winner);
            return;
        }
        GameManager.Instance.ResetToFirstPlayer();
        GameManager.Instance.AdvanceRound();
        GameManager.Instance.SetPhase(GamePhase.Gameplay);
    }

    private static void AddVote(Dictionary<int, int> votes, int seat)
    {
        votes[seat] = votes.TryGetValue(seat, out var n) ? n + 1 : 1;
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
