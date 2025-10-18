using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public struct StatusEffect
    {
        public IStatusEffect statusEffect;
        public string Name; //�̸� ������
        public int Stack; // ���� �� +
        public int RemainsTurn; // �����ο��� ���� �� < �̹����ο��� ���� �� �� ��� �̹����ο��� ���� ���� ������.
    }

    public CharacterBase characterBase;
    [SerializeField] private HealthComponent healthComp;
    [SerializeField] private StatusComponent statusComp;
    public HealthComponent HealthComp => healthComp;
    public StatusComponent StatusComp => statusComp;

    public float maxSkillGauge { get; private set; }
    private int sequenceNumber;

    StatusEffect testeffect;

    public void Start()
    {
        
    }

    public void Update()
    {
        StatusComp.TurnAll();
    }
    public void SetCharacterBase(CharacterBase character)
    {
        characterBase = character;
    }
}
