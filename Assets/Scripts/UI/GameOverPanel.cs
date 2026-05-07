using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text reasonText;
    [SerializeField] private TMP_Text rolesText;

    [Header("Backgrounds")]
    [SerializeField] private Sprite dreamerWinBg;
    [SerializeField] private Sprite nightmareWinBg;

    private void OnEnable()
    {
        var winner = GameManager.Instance.Winner;

        var bg = GetComponent<Image>();
        if (bg != null && winner != null)
        {
            var sprite = winner.Winner == PlayerRole.Dreamer ? dreamerWinBg : nightmareWinBg;
            if (sprite != null) bg.sprite = sprite;
        }

        if (headerText != null)
        {
            if (winner != null)
            {
                string colorTag = winner.Winner == PlayerRole.Dreamer ? "#5BBFD9" : "#D9344F";
                headerText.text = $"<color={colorTag}><b>{winner.Winner}s Win</b></color>";
            }
            else
            {
                headerText.text = "Game Over";
            }
        }

        if (reasonText != null)
            reasonText.text = winner != null ? winner.Reason : "";

        if (rolesText != null)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<b>Final roles:</b>");
            foreach (var p in GameManager.Instance.Players)
            {
                var youTag = p.PlayerNumber == 1 ? " (you)" : "";
                var aliveTag = p.IsAlive ? "" : " — eliminated";
                string roleColor = p.Role == PlayerRole.Dreamer ? "#5BBFD9" : "#D9344F";
                sb.AppendLine($"P{p.PlayerNumber}{youTag}: {p.Archetype} — <color={roleColor}>{p.GetRoleName()}</color>{aliveTag}");
            }
            rolesText.text = sb.ToString();
        }
    }

    public void OnPlayAgainClicked()
    {
        GameManager.Instance.ResetForNewGame();
        GameManager.Instance.SetPhase(GamePhase.Title);
    }
}
