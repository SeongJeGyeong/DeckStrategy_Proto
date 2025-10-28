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
        public string Name; //이름 같으면
        public int Stack; // 스택 수 +
        public int RemainsTurn; // 이전부여된 라운드 수 < 이번에부여된 라운드 수 일 경우 이번에부여된 라운드 수로 덮어씌운다.
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
        //StatusComp.TurnAll(); // 보여주기 용
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
