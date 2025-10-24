using UnityEngine;

public class Result_Combat : MonoBehaviour
{
    [SerializeField]
    private DataCenter dataCenter;

    [Header("MVP ���Ե�")]
    [SerializeField] private CharacterSlotUI[] mvpSlots;

    [Header("��� ������ (���� ������� �޾ƿ� ĳ���͵�)")]
    [SerializeField] private OwnedCharacterInfo[] mvpCharacters; 

    void Start()
    {
        ApplyResults();
    }

    public void ApplyResults()
    {
        int count = Mathf.Min(mvpSlots.Length, mvpCharacters.Length);

        for (int i = 0; i < count; i++)
        {
            CharacterData data = dataCenter.characterDataTable.FindCharacterData(mvpCharacters[i].characterID);
            CharacterModelData modelData = dataCenter.characterModelDataTable.FindCharacterModel(mvpCharacters[i].characterModelID);

            if (mvpSlots[i] != null)
                mvpSlots[i].SetData(data, modelData);
        }
    }
}