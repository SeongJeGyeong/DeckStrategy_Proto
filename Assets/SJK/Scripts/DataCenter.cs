using System.Collections.Generic;
using UnityEngine;

public class DataCenter : MonoBehaviour
{
    [SerializeField]
    private OwnedCharacterTable ownedCharacterTable;
    [SerializeField]
    public CharacterDataTable characterDataTable;
    [SerializeField]
    public CharacterModelDataTable characterModelDataTable;

    public CharacterData FindCharacterData(int ID)
    {
        foreach (CharacterData data in characterDataTable.characterDataList)
        {
            if (data.ID == ID)
            {
                return data;
            }
        }

        return null;
    }

    public CharacterModelData FindCharacterModel(int ID)
    {
        foreach (CharacterModelData modelData in characterModelDataTable.characterModelDataList)
        {
            if (modelData.ID == ID)
            {
                return modelData;
            }
        }

        return null;
    }

    public List<OwnedCharacterInfo> GetOwnedCharacterList()
    {
        return ownedCharacterTable.ownedCharacterList;
    }
}
