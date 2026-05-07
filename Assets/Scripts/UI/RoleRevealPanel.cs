using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoleRevealPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text roleText;
    [SerializeField] private TMP_Text archetypeText;
    [SerializeField] private TMP_Text descriptionText;

    [Header("Backgrounds (by Archetype + Role)")]
    [SerializeField] private Sprite warriorDreamerBg;
    [SerializeField] private Sprite warriorNightmareBg;
    [SerializeField] private Sprite singerDreamerBg;
    [SerializeField] private Sprite singerNightmareBg;
    [SerializeField] private Sprite queenDreamerBg;
    [SerializeField] private Sprite queenNightmareBg;
    [SerializeField] private Sprite twinADreamerBg;
    [SerializeField] private Sprite twinANightmareBg;
    [SerializeField] private Sprite twinBDreamerBg;
    [SerializeField] private Sprite twinBNightmareBg;

    private void OnEnable()
    {
        var player = GameManager.Instance.GetCurrentPlayer();

        var bg = GetComponent<Image>();
        if (bg != null)
        {
            var sprite = SpriteFor(player.Archetype, player.Role);
            if (sprite != null) bg.sprite = sprite;
        }

        if (roleText != null)
        {
            string colorTag = player.Role == PlayerRole.Dreamer ? "#5BBFD9" : "#D9344F";
            roleText.text = $"You are a\n<color={colorTag}><size=150%><b>{player.GetRoleName()}</b></size></color>";
        }

        if (archetypeText != null)
        {
            archetypeText.text = $"Archetype: <b>{player.GetArchetypeName()}</b>";
        }

        if (descriptionText != null)
        {
            if (player.Role == PlayerRole.Dreamer)
            {
                descriptionText.text = "Work with your fellow Dreamers to collect Dream Fragments and escape Wonderland. Beware of Nightmares hiding among you.";
            }
            else
            {
                descriptionText.text = "Sabotage the Dreamers in secret. Help your fellow Nightmares survive and outnumber the Dreamers to corrupt Wonderland.";
            }
        }
    }

    private Sprite SpriteFor(Archetype a, PlayerRole r)
    {
        bool dreamer = r == PlayerRole.Dreamer;
        switch (a)
        {
            case Archetype.Warrior: return dreamer ? warriorDreamerBg : warriorNightmareBg;
            case Archetype.Singer:  return dreamer ? singerDreamerBg  : singerNightmareBg;
            case Archetype.Queen:   return dreamer ? queenDreamerBg   : queenNightmareBg;
            case Archetype.TwinA:   return dreamer ? twinADreamerBg   : twinANightmareBg;
            case Archetype.TwinB:   return dreamer ? twinBDreamerBg   : twinBNightmareBg;
        }
        return null;
    }

    public void OnContinueButtonClicked()
    {
        Debug.Log($"[RoleRevealPanel] Player {GameManager.Instance.GetCurrentPlayer().PlayerNumber} done viewing role. Starting gameplay.");
        GameManager.Instance.MarkRoleRevealComplete();
        GameManager.Instance.StartGameplay();
    }
}
