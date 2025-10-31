using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;
using static Character;

public class Character : MonoBehaviour
{
    public struct StatusEffect
    {
        public IStatusEffect statusEffect;
        public string Name; //�̸� ������
        public int Stack; // ���� �� +
        public int RemainsTurn; // �����ο��� ���� �� < �̹����ο��� ���� �� �� ��� �̹����ο��� ���� ���� ������.
    }

    [SerializeField] private HealthComponent healthComp;
    [SerializeField] private StatusEffectComponent statusEffectComp;
    [SerializeField] private AttackComponent attackComp;
    [SerializeField] private StatusComponent statusComp;
    public HealthComponent HealthComp => healthComp;
    public StatusEffectComponent StatusEffectComp => statusEffectComp;
    public AttackComponent AtackComp => attackComp;
    public StatusComponent StatusComp => statusComp;

    public CharacterData characterData;
    public CharacterModelData characterModelData;
    public float maxSkillGauge { get; private set; }

    public bool isEnemy = false;
    public float combatPower { get; private set; }

    public void Start()
    {
        
    }

    public void Update()
    {

    }

    public void EndTurn()
    {
        statusEffectComp.TurnAll();
    }

    public void UpdateCombatPower()
    {
        if(characterData == null)
        {
            combatPower = 0;
        }
        else
        {
            combatPower = characterData.maxHp + characterData.attack + characterData.defense + characterData.speed;
        }
    }
    public EAttributeType GetAttributeType()
    {
        return characterData.type;
    }

    private void OnEnable()
    {
        
    }
}
