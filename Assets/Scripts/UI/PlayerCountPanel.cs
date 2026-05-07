using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class PlayerCountPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text playerCountText;
    [SerializeField] private int requiredPlayerCount = 5;
 
    private void OnEnable()
    {
        if (playerCountText != null)
        {
            playerCountText.text =
                $"This game requires <b>{requiredPlayerCount} players</b>.\n\n" +
                $"Make sure all {requiredPlayerCount} players are present, then press Continue.";
        }
    }
 
    public void OnContinueButtonClicked()
    {
        Debug.Log("[PlayerCountPanel] Continue clicked.");
        GameManager.Instance.SetPlayerCount(requiredPlayerCount);
        GameManager.Instance.SetPhase(GamePhase.Setup);
    }
 
    public void OnBackButtonClicked()
    {
        Debug.Log("[PlayerCountPanel] Back clicked.");
        GameManager.Instance.SetPhase(GamePhase.Title);
    }
}
 