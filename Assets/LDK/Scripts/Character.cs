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

    public float MaxSkillGauge { get; private set; }
    private int SequenceNumber;

    StatusEffect testeffect;

    public void Start()
    {
        testeffect = new StatusEffect();
        testeffect.statusEffect = new BurnEffect();
        testeffect.Stack = 1;
        testeffect.RemainsTurn = 1;
        testeffect.Name = "Burn";

        StatusComp.AddEffect(testeffect);
    }

    public void Update()
    {
        StatusComp.TickAll();
    }
}
