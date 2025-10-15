using UnityEngine;

[CreateAssetMenu(fileName = "Character Model", menuName = "Scriptable/Character Model Data", order = int.MaxValue)]
public class CharacterModelData : ScriptableObject
{
    public int ID;
    public Material material;
}
