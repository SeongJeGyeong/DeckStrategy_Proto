using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable/Character Data", order = int.MaxValue)]
public class CharacterData : ScriptableObject
{
    public int ID;

    public float maxHp;
    public string characterName;
    public AttributeType type;
    public float attack;
    public float defense;
    public float speed;
}
