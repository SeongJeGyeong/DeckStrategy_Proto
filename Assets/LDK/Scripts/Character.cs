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
    [SerializeField] private StatusComponent statusComp;
    [SerializeField] private AttackComponent attackComp;

    public HealthComponent HealthComp => healthComp;
    public StatusComponent StatusComp => statusComp;
    public AttackComponent AtackComp => attackComp;

    public CharacterData characterData;
    public CharacterModelData characterModelData;
    public float maxSkillGauge { get; private set; }
    private int sequenceNumber;

    public bool isEnemy = false;

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
}
