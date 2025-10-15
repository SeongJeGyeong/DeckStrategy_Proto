using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IBattleable
{
    public struct StatusEffect
    {
        string Name;
        int Stack;
        int RemainsTurn;
    }

    public CharacterBase characterBase;
    private float CurrHp;
    private float SkillGauge;
    private int SequenceNumber;

    public List<StatusEffect> ActiveStatusEffects;
    StatusEffect testeffect;
    public event Action<float> OnDamaged;

    public virtual void TakeDamage(float amount)
    {
        CurrHp -= amount;
        print($"{name} 데미지 받음");
        OnDamaged?.Invoke(CurrHp);

        if(CurrHp <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {

    }
    public void Awake()
    {
        CurrHp = characterBase.characterData.maxHp;
        testeffect = new StatusEffect();
    }

    public void Update()
    {

    }
}
