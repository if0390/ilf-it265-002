#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class PreparePanels
{
    public static void Execute()
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas == null) { Debug.LogError("[PreparePanels] No Canvas"); return; }

        string[] panels = { "TitlePanel", "GameplayPanel", "VotingPanel", "RoleRevealPanel", "GameOverPanel" };
        foreach (var p in panels)
        {
            var t = canvas.transform.Find(p);
            if (t == null) continue;
            var img = t.GetComponent<Image>();
            if (img == null) continue;
            img.color = Color.white;
            img.type = Image.Type.Simple;
            img.preserveAspect = false;
            EditorUtility.SetDirty(img);
        }

        // Title PNG already has the wordmark — hide overlay text
        var title = canvas.transform.Find("TitlePanel");
        if (title != null)
        {
            var titleText = title.Find("TitleText");
            var subtitleText = title.Find("SubtitleText");
            if (titleText != null) titleText.gameObject.SetActive(false);
            if (subtitleText != null) subtitleText.gameObject.SetActive(false);
        }

        EditorUtility.SetDirty(canvas);
        AssetDatabase.SaveAssets();
        Debug.Log("[PreparePanels] Panels prepped.");
    }
}
#endif
