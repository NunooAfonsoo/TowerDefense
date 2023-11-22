using UnityEngine;
using UnityEditor;

public class TurretConfigWindow : EditorWindow
{
    private TurretConfig turretConfig;
    private Vector2 scrollPosition = Vector2.zero;
    private int space = 25;

    [MenuItem("Tools/TurretConfig")]
    public static void ShowWindow()
    {
        GetWindow<TurretConfigWindow>("TurretConfig Editor");

        TurretConfig selectedCameraConfig = Selection.activeObject as TurretConfig; // Automatically assigns the selected TurretConfig
        if (selectedCameraConfig != null)
        {
            GetWindow<TurretConfigWindow>("TurretConfig Editor").turretConfig = selectedCameraConfig;
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        turretConfig = EditorGUILayout.ObjectField("Turret Config Scriptable Object", turretConfig, typeof(TurretConfig), false) as TurretConfig;

        if (turretConfig == null)
        {
            EditorGUILayout.HelpBox("Please assign a TurretConfig.", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawDefaultInspectorWindow();

        EditorGUILayout.EndScrollView();
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(turretConfig);
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