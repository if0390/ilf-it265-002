#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class WireAudio
{
    public static void Execute()
    {
        AssetDatabase.Refresh();
        const string path = "Assets/Audio/Music/spencer_yk-fairytale-dream-151967.mp3";
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

        var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogError($"[WireAudio] Could not load AudioClip at {path}");
            return;
        }

        var go = GameObject.Find("GameManager");
        if (go == null) { Debug.LogError("[WireAudio] GameManager not found"); return; }

        var src = go.GetComponent<AudioSource>();
        if (src == null) { Debug.LogError("[WireAudio] No AudioSource on GameManager"); return; }

        src.clip = clip;
        src.loop = true;
        src.playOnAwake = true;
        src.volume = 0.5f;

        EditorUtility.SetDirty(go);
        EditorUtility.SetDirty(src);
        AssetDatabase.SaveAssets();

        Debug.Log("[WireAudio] AudioSource wired with clip.");
    }
}
#endif
