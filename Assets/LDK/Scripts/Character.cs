using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

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
    [SerializeField] private StatusEffectComponent statusComp;
    [SerializeField] private AttackComponent attackComp;
    [SerializeField] private CharStatusComponent charStatusComp;
    public HealthComponent HealthComp => healthComp;
    public StatusEffectComponent StatusComp => statusComp;
    public AttackComponent AtackComp => attackComp;
    public CharStatusComponent CharStatusComp => charStatusComp;

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
        //StatusComp.TurnAll(); // �����ֱ� ��
    }

    public void EndTurn()
    {
        statusComp.TurnAll();
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
}
