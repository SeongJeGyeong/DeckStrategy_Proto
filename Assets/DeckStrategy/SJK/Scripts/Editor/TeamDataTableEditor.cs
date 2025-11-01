using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TeamDataTable))]
public class TeamDataTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty teamsProp = serializedObject.FindProperty("teams");
        int maxTeams = TeamDataTable.MaxTeams;

        // �迭 ũ�⸦ ������ ���� (�׻� 8�� ����)
        if (teamsProp.arraySize != maxTeams)
        {
            while (teamsProp.arraySize < maxTeams)
                teamsProp.InsertArrayElementAtIndex(teamsProp.arraySize);

            while (teamsProp.arraySize > maxTeams)
                teamsProp.DeleteArrayElementAtIndex(teamsProp.arraySize - 1);
        }

        // ����
        EditorGUILayout.LabelField($"Teams", EditorStyles.boldLabel);

        // �� ���Ե� ǥ��
        for (int i = 0; i < maxTeams; i++)
        {
            SerializedProperty teamProp = teamsProp.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(teamProp, new GUIContent($"Team {i + 1}"), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}