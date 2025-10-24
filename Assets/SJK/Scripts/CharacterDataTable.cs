using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Character Data Table", menuName = "Scriptable/Character Data Table", order = int.MaxValue)]
public class CharacterDataTable : ScriptableObject
{
    public List<CharacterData> characterDataList = new List<CharacterData>();

    public CharacterData FindCharacterData(int ID)
    {
        foreach (CharacterData data in characterDataList)
        {
            if (data.ID == ID)
            {
                return data;
            }
        }

        return null;
    }
}
