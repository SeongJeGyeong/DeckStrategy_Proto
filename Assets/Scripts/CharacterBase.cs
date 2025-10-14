using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Base", menuName = "Character Status Base", order = int.MaxValue)]
public class CharacterBase : ScriptableObject
{
    public float MaxHp;
    public int Level;
    public string Name;
    public AttributeType type;
    public float Atk;
    public float Def;
    public float Spd;


}