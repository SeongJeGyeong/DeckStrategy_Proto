using System;
using UnityEngine;

public class StatusComponent : MonoBehaviour
{
    private Character owner;

    public float attack { get; private set; }
    public float defense { get; private set; }
    public float speed { get; private set; }

    public float totalDamageDealt { get; private set; }
    public float totalDamageTaken { get; private set; }
    public float totalHealingDone { get; private set; }

    private void Start()
    {
        owner = GetComponent<Character>();
        if (owner == null) return;
        attack = owner.characterData.attack;
        defense = owner.characterData.defense;
        speed = owner.characterData.speed;
    }

    public void UpdateDamageDealt(float damage)
    {
        totalDamageDealt += damage;
    }
    public void UpdateDamageTaken(float damage)
    {
        totalDamageTaken += damage;
    }
    public void UpdateHealingDone(float damage)
    {
        totalHealingDone += damage;
    }
    public float CalculateMVPScore()
    {
        return totalDamageDealt + totalDamageTaken + totalHealingDone;
    }
}
