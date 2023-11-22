using UnityEngine;
using UnityEditor;

public class SpawnerConfigWindow : EditorWindow
{
    private SpawnerConfig spawnerConfig;
    private Vector2 scrollPosition = Vector2.zero;
    private int space = 25;

    [MenuItem("Tools/SpawnerConfig")]
    public static void ShowWindow()
    {
        GetWindow<SpawnerConfigWindow>("SpawnerConfig Editor");

        SpawnerConfig selectedCameraConfig = Selection.activeObject as SpawnerConfig; // Automatically assigns the selected SpawnerConfig
        if (selectedCameraConfig != null)
        {
            GetWindow<SpawnerConfigWindow>("SpawnerConfig Editor").spawnerConfig = selectedCameraConfig;
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        spawnerConfig = EditorGUILayout.ObjectField("Spawner Config Scriptable Object", spawnerConfig, typeof(SpawnerConfig), false) as SpawnerConfig;

        if (spawnerConfig == null)
        {
            EditorGUILayout.HelpBox("Please assign a SpawnerConfig.", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawDefaultInspectorWindow();

        EditorGUILayout.EndScrollView();
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(spawnerConfig);
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