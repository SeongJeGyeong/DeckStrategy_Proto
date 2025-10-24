using NUnit.Framework.Interfaces;
using UnityEngine;

public class CharactersList : MonoBehaviour
{
    [SerializeField]
    private DataCenter dataCenter;

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

        if (dataCenter.ownedCharacterTable == null) return;

        foreach (OwnedCharacterInfo character in dataCenter.ownedCharacterTable.ownedCharacterList)
        {
            var itemUI = Instantiate(iconPrefab, contentParent.transform);
            var slot = itemUI.GetComponent<CharacterIcon>(); // 프리팹에 붙은 UI 스크립트

            var statusData = dataCenter.characterDataTable.FindCharacterData(character.characterID);
            var modelData = dataCenter.characterModelDataTable.FindCharacterModel(character.characterModelID);

            slot.SetData(statusData, modelData, character.characterLevel);
        }
    }
}
