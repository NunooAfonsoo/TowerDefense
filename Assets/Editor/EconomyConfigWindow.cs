using UnityEngine;
using UnityEditor;

public class EconomyConfigWindow : EditorWindow
{
    private EconomyConfig economyConfig;
    private Vector2 scrollPosition = Vector2.zero;
    private int space = 25;

    [MenuItem("Tools/EconomyConfig")]
    public static void ShowWindow()
    {
        GetWindow<EconomyConfigWindow>("EconomyConfig Editor");

        EconomyConfig selectedCameraConfig = Selection.activeObject as EconomyConfig; // Automatically assigns the selected EconomyConfig
        if (selectedCameraConfig != null)
        {
            GetWindow<EconomyConfigWindow>("EconomyConfig Editor").economyConfig = selectedCameraConfig;
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        economyConfig = EditorGUILayout.ObjectField("Economy Config Scriptable Object", economyConfig, typeof(EconomyConfig), false) as EconomyConfig;

        if (economyConfig == null)
        {
            EditorGUILayout.HelpBox("Please assign a EconomyConfig.", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawDefaultInspectorWindow();

        EditorGUILayout.EndScrollView();
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(economyConfig);
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