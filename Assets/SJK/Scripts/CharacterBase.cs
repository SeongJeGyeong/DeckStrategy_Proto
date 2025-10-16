using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Base", menuName = "Scriptable/Character Base", order = int.MaxValue)]
public class CharacterBase : ScriptableObject
{
    public CharacterData characterData;
    public int Level;
    public CharacterModelData characterModelData;
}