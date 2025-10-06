using UnityEditor;
using UnityEngine;

public class PublishingNotice : EditorWindow
{
    [MenuItem("File/Publish to Itch", priority = 210)]
    public static void OpenWindow() => GetWindow<PublishingNotice>();

    void CreateGUI()
    {
        titleContent = new GUIContent("Publishing Notice");
        minSize = new Vector2(480, 240);
        maxSize = new Vector2(480, 240);
    }

    void OnGUI()
    {
        GUIStyle style = EditorStyles.label;
        style.alignment = TextAnchor.MiddleCenter;
        style.wordWrap = true;

        EditorGUILayout.LabelField("If you want to publish to itch, ping Generalisk & he'll handle it for you. He'll have it all built and published within 24 hours.", style);
    }
}
