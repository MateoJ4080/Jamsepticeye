using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Object), true)]
class HideScriptField : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        DrawPropertiesExcluding(serializedObject, "m_Script");
        if (EditorGUI.EndChangeCheck())
        { serializedObject.ApplyModifiedProperties(); }
    }
}
