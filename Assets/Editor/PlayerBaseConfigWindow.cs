using UnityEngine;
using UnityEditor;

public class PlayerBaseConfigWindow : EditorWindow
{
    private PlayerBaseConfig playerBaseConfig;
    private Vector2 scrollPosition = Vector2.zero;
    private int space = 25;

    [MenuItem("Tools/PlayerBaseConfig")]
    public static void ShowWindow()
    {
        GetWindow<PlayerBaseConfigWindow>("PlayerBaseConfig Editor");

        PlayerBaseConfig selectedCameraConfig = Selection.activeObject as PlayerBaseConfig; // Automatically assigns the selected PlayerBaseConfig
        if (selectedCameraConfig != null)
        {
            GetWindow<PlayerBaseConfigWindow>("PlayerBaseConfig Editor").playerBaseConfig = selectedCameraConfig;
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        playerBaseConfig = EditorGUILayout.ObjectField("PlayerBase Config Scriptable Object", playerBaseConfig, typeof(PlayerBaseConfig), false) as PlayerBaseConfig;

        if (playerBaseConfig == null)
        {
            EditorGUILayout.HelpBox("Please assign a PlayerBaseConfig.", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawDefaultInspectorWindow();

        EditorGUILayout.EndScrollView();
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(playerBaseConfig);
        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren))
        {
            enterChildren = false;
            if (iterator.name == "m_Script") // We don't want to change the SO script reference
            {
                continue;
            }

            EditorGUILayout.PropertyField(iterator, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}