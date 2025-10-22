using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(UserData.Team))]
public class TeamDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // �⺻ �Ӽ��� ã��
        SerializedProperty charactersProp = property.FindPropertyRelative("characters");

        // Layout ������� �׸��� ���� BeginProperty ȣ��
        EditorGUI.BeginProperty(position, label, property);

        // Foldout (Team �׸� ����/��ġ��)
        property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, true);
        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;

            // characters �迭 ǥ��
            EditorGUILayout.LabelField("Characters");

            // ���� ũ��� ����
            int maxSize = 5;
            charactersProp.arraySize = maxSize;

            for (int i = 0; i < maxSize; i++)
            {
                var element = charactersProp.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(element, new GUIContent($"Slot {i + 1}"));
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }
}