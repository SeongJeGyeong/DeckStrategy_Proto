using NUnit.Framework.Interfaces;
using UnityEngine;

public class CharactersList : MonoBehaviour
{
    public CharacterBase[] characters;
    [SerializeField]
    private GameObject iconPrefab;
    [SerializeField]
    private Transform contentParent;

    private void Start()
    {
        PopulateScrollView();
    }

    private void PopulateScrollView()
    {
        foreach(Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach(var character in characters)
        {
            var itemUI = Instantiate(iconPrefab, contentParent.transform);
            var slot = itemUI.GetComponent<CharacterIcon>(); // �����տ� ���� UI ��ũ��Ʈ
            slot.SetData(character);
        }
    }
}
