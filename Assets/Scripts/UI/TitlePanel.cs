using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TitlePanel : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("[TitlePanel] Start clicked.");
        GameManager.Instance.SetPhase(GamePhase.Setup);
    }
}
 