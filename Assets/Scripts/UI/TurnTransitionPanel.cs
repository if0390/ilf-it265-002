using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class TurnTransitionPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text passText;
    [SerializeField] private TMP_Text instructionText;
 
    private void OnEnable()
    {
        if (GameManager.Instance.AllPlayersDone())
        {
            GameManager.Instance.ResetToFirstPlayer();

            if (!GameManager.Instance.RoleRevealComplete)
            {
                Debug.Log("[TurnTransitionPanel] Initial role reveals done. Starting gameplay.");
                GameManager.Instance.MarkRoleRevealComplete();
                GameManager.Instance.StartGameplay();
            }
            else
            {
                Debug.Log("[TurnTransitionPanel] Round complete. Advancing to next round.");
                GameManager.Instance.AdvanceRound();
                GameManager.Instance.SetPhase(GamePhase.Gameplay);
            }
            return;
        }
 
        var player = GameManager.Instance.GetCurrentPlayer();
        if (passText != null)
        {
            passText.text = $"Pass the device to\n<size=130%><b>Player {player.PlayerNumber}</b></size>";
        }
        if (instructionText != null)
        {
            instructionText.text = "Only Player " + player.PlayerNumber + " should press Ready.\n" +
                                   "Make sure no one else is looking at the screen.";
        }
    }
 
    public void OnReadyButtonClicked()
    {
        var nextPhase = GameManager.Instance.RoleRevealComplete ? GamePhase.Gameplay : GamePhase.RoleReveal;
        Debug.Log($"[TurnTransitionPanel] Player {GameManager.Instance.GetCurrentPlayer().PlayerNumber} is ready. Going to {nextPhase}.");
        GameManager.Instance.SetPhase(nextPhase);
    }
}