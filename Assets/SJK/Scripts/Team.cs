using UnityEngine;

public class Team : MonoBehaviour
{
    public CharacterBase[] characters = new CharacterBase[5];
    public int teamIndex;

    public void SaveTeam(CharacterBase[] savedCharacters)
    {
        characters = savedCharacters;
    }
}
