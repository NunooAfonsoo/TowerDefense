using UnityEngine;
using UnityEditor;

public class CreepConfigWindow : EditorWindow
{
    private CreepConfig creepConfig;
    private Vector2 scrollPosition = Vector2.zero;
    private int space = 25;

    [MenuItem("Tools/CreepConfig")]
    public static void ShowWindow()
    {
        GetWindow<CreepConfigWindow>("CreepConfig Editor");

        CreepConfig selectedCameraConfig = Selection.activeObject as CreepConfig; // Automatically assigns the selected CreepConfig
        if (selectedCameraConfig != null)
        {
            GetWindow<CreepConfigWindow>("CreepConfig Editor").creepConfig = selectedCameraConfig;
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        creepConfig = EditorGUILayout.ObjectField("Creep Config Scriptable Object", creepConfig, typeof(CreepConfig), false) as CreepConfig;

        if (creepConfig == null)
        {
            EditorGUILayout.HelpBox("Please assign a CreepConfig.", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawDefaultInspectorWindow();

        EditorGUILayout.EndScrollView();
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(creepConfig);
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