using UnityEngine;
using UnityEditor;

public class ProjectileConfigWindow : EditorWindow
{
    private ProjectileConfig projectileConfig;
    private Vector2 scrollPosition = Vector2.zero;
    private int space = 25;

    [MenuItem("Tools/ProjectileConfig")]
    public static void ShowWindow()
    {
        GetWindow<ProjectileConfigWindow>("ProjectileConfig Editor");

        ProjectileConfig selectedCameraConfig = Selection.activeObject as ProjectileConfig; // Automatically assigns the selected ProjectileConfig
        if (selectedCameraConfig != null)
        {
            GetWindow<ProjectileConfigWindow>("ProjectileConfig Editor").projectileConfig = selectedCameraConfig;
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        projectileConfig = EditorGUILayout.ObjectField("Projectile Config Scriptable Object", projectileConfig, typeof(ProjectileConfig), false) as ProjectileConfig;

        if (projectileConfig == null)
        {
            EditorGUILayout.HelpBox("Please assign a ProjectileConfig.", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawDefaultInspectorWindow();

        EditorGUILayout.EndScrollView();
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(projectileConfig);
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