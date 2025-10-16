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
        public string Name; //이름 같으면
        public int Stack; // 스택 수 +
        public int RemainsTurn; // 이전부여된 라운드 수 < 이번에부여된 라운드 수 일 경우 이번에부여된 라운드 수로 덮어씌운다.
    }

    public CharacterBase characterBase;
    public HealthComponent healthComp;
    public StatusComponent statusComp;
    public float MaxSkillGauge { get; private set; }
    private int SequenceNumber;

    StatusEffect testeffect;

    public void Start()
    {
        healthComp = GetComponent<HealthComponent>();
        statusComp = GetComponent<StatusComponent>();

        testeffect = new StatusEffect();
        testeffect.statusEffect = new BurnEffect();
        testeffect.Stack = 1;
        testeffect.RemainsTurn = 1;
        testeffect.Name = "burn";

        statusComp.AddEffect(testeffect);
    }

    public void Update()
    {
        statusComp.TickAll();
    }
}
