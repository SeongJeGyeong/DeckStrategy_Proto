using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IBattleable
{
    struct StatusEffect
    {
        string Name;
        int Stack;
        int RemainsTurn;
    }

    [SerializeField]
    CharacterBase characterBase;

    private float CurrHp;
    private float SkillGauge;
    private int SequenceNumber;

    public Slider HealthSlider;

    public virtual void TakeDamage(float amount)
    {
        CurrHp -= amount;
        HealthSlider.value = CurrHp;
        print($"{name} 데미지 받음");

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
        CurrHp = characterBase.MaxHp;
    }

    private void OnEnable()
    {
        HealthSlider.gameObject.SetActive(true);
        HealthSlider.maxValue = characterBase.MaxHp;
        HealthSlider.value = CurrHp;
    }

    public void Update()
    {
        
    }
}
