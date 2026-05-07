#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class AddTextBackgrounds
{
    public static void Execute()
    {
        string[] paths = {
            "Canvas/GameplayPanel/PlayersOverviewText",
            "Canvas/GameplayPanel/HeaderText",
            "Canvas/GameplayPanel/LocationText",
            "Canvas/GameplayPanel/ActionStatusText"
        };

        foreach (var path in paths)
        {
            var textGO = GameObject.Find(path);
            if (textGO == null) { Debug.LogWarning($"[AddTextBackgrounds] Missing: {path}"); continue; }

            var existing = textGO.transform.parent.Find(textGO.name + "Bg");
            if (existing != null) { Debug.Log($"[AddTextBackgrounds] Bg already exists for {path}"); continue; }

            var textRect = textGO.GetComponent<RectTransform>();
            if (textRect == null) continue;

            var bgGO = new GameObject(textGO.name + "Bg", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            bgGO.transform.SetParent(textGO.transform.parent, false);

            var bgRect = bgGO.GetComponent<RectTransform>();
            bgRect.anchorMin = textRect.anchorMin;
            bgRect.anchorMax = textRect.anchorMax;
            bgRect.anchoredPosition = textRect.anchoredPosition;
            bgRect.sizeDelta = textRect.sizeDelta + new Vector2(30, 15);
            bgRect.pivot = textRect.pivot;

            var img = bgGO.GetComponent<Image>();
            img.color = Color.white;

            // Move bg to sibling index just before the text so it renders behind
            int textIndex = textGO.transform.GetSiblingIndex();
            bgGO.transform.SetSiblingIndex(textIndex);

            EditorUtility.SetDirty(bgGO);
            EditorUtility.SetDirty(textGO);
        }

        var canvas = GameObject.Find("Canvas");
        if (canvas != null) EditorUtility.SetDirty(canvas);
        AssetDatabase.SaveAssets();
        Debug.Log("[AddTextBackgrounds] Done.");
    }
}
#endif
