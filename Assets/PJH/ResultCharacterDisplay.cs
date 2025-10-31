using UnityEngine;
using TMPro;

public class Result_CharacterDisplay : MonoBehaviour
{
    [SerializeField] private Transform characterParent; // ĳ���� ������Ʈ�� ������ �θ� (Empty)
    [SerializeField] private GameObject capsulePrefab;  // �ӽ� Capsule Prefab
    [SerializeField] private float spacing = 2f;        // ĳ���� �� ����

    [System.Serializable]
    public class CharacterData
    {
        public string name;
        public int contribution;
    }

    public CharacterData[] characters;

    public void DisplayCharacters()
    {
        // ������ �ִ� Capsule ���� (�ٽ� ǥ���� ��� ���)
        foreach (Transform child in characterParent)
            Destroy(child.gameObject);

        // ĳ���� ����ŭ Capsule ����
        for (int i = 0; i < characters.Length; i++)
        {
            GameObject cap = Instantiate(capsulePrefab, characterParent);
            cap.name = characters[i].name;
            cap.transform.localPosition = new Vector3(i * spacing, 0, 0);
        }
    }
}