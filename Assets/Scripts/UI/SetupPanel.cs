using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class SetupPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text setupText;
    [SerializeField] private float delayBeforeAdvance = 1.5f; 
    
    private void OnEnable()
    {
        if (setupText != null)
        {
            setupText.text = "Shuffling roles and archetypes...";
        }

        Debug.Log("[SetupPanel] Generating players...");
        var players = RoleAssigner.CreatePlayers();
        GameManager.Instance.SetPlayers(players);

        Invoke(nameof(AdvanceToRoleReveal), delayBeforeAdvance);
    }

    private void AdvanceToRoleReveal()
    {
        GameManager.Instance.ResetToFirstPlayer();
        GameManager.Instance.SetPhase(GamePhase.RoleReveal);
    }
}