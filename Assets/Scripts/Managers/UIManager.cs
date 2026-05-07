using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject playerCountPanel;
    [SerializeField] private GameObject setupPanel;
    [SerializeField] private GameObject turnTransitionPanel;
    [SerializeField] private GameObject roleRevealPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject votingPanel;       // Day 2
    [SerializeField] private GameObject gameOverPanel;     // Day 3

    public void ShowPanelForPhase(GamePhase phase)
    {
        HideAllPanels();

        switch (phase)
        {
            case GamePhase.Title:             ShowPanel(titlePanel); break;
            case GamePhase.PlayerCountSelect: ShowPanel(playerCountPanel); break;
            case GamePhase.Setup:             ShowPanel(setupPanel); break;
            case GamePhase.TurnTransition:    ShowPanel(turnTransitionPanel); break;
            case GamePhase.RoleReveal:        ShowPanel(roleRevealPanel); break;
            case GamePhase.Gameplay:          ShowPanel(gameplayPanel); break;
            case GamePhase.Voting:            ShowPanel(votingPanel); break;
            case GamePhase.GameOver:          ShowPanel(gameOverPanel); break;
        }
    }

    private void ShowPanel(GameObject panel)
    {
        if (panel != null) panel.SetActive(true);
        else Debug.LogWarning("[UIManager] Tried to show a panel not assigned in Inspector (normal during early development).");
    }

    private void HideAllPanels()
    {
        if (titlePanel != null) titlePanel.SetActive(false);
        if (playerCountPanel != null) playerCountPanel.SetActive(false);
        if (setupPanel != null) setupPanel.SetActive(false);
        if (turnTransitionPanel != null) turnTransitionPanel.SetActive(false);
        if (roleRevealPanel != null) roleRevealPanel.SetActive(false);
        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (votingPanel != null) votingPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }
}