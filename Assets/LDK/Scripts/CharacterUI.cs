using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static Character;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Character owner;

    [SerializeField] private Transform statusContainer;
    [SerializeField] private Image statusiconPrefab;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider gaugeSlider;

    [SerializeField] private List<Sprite> statusSprites;

    private ObjectPool<Image> iconPool;
    private readonly Dictionary<string, Image> activeIcons = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {

    }
    private void Start()
    {
 
        healthSlider.maxValue = owner.characterBase.characterData.maxHp;
        healthSlider.value = owner.characterBase.characterData.maxHp;

        gaugeSlider.maxValue = owner.MaxSkillGauge;
        gaugeSlider.value = owner.MaxSkillGauge;

        owner.healthComp.OnDamaged += HealthUpdate;

        owner.statusComp.OnEffectAdded += OnEffectAdded;
        owner.statusComp.OnEffectRemoved += OnEffectRemoved;
    }

    private void HealthUpdate(float CurrHp)
    {
        healthSlider.value = CurrHp;
    }
    private void GaugeUpdate(float CurrGauge)
    {
        healthSlider.value = CurrGauge;
    }
    private void OnEffectAdded(StatusEffect effect)
    {
        //@TODO 이벤트 받으면 이미지 + 스프라이트 띄워야함
    }
    private void OnEffectRemoved(StatusEffect effect)
    {

    }
}