using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static Character;

[System.Serializable]
public struct StatusSpritePair
{
    public string Name;
    public Sprite Sprite;
}

public class CharacterUI : MonoBehaviour
{

    [SerializeField] private Character owner;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider gaugeSlider;

    [SerializeField] private List<StatusSpritePair> spritePairs;
    //[SerializeField] private ObjectPool<Image> iconPool;

    private Dictionary<string, Sprite> statusSprites;
    private Dictionary<string, Image> activeIcons;

    private void Awake()
    {
        statusSprites = new Dictionary<string, Sprite>();

        foreach (var pair in spritePairs)
        {
            if (!statusSprites.ContainsKey(pair.Name))
                statusSprites.Add(pair.Name, pair.Sprite);
        }

        activeIcons = new Dictionary<string, Image>();

        healthSlider.maxValue = owner.characterBase.characterData.maxHp;
        healthSlider.value = owner.characterBase.characterData.maxHp;

        gaugeSlider.maxValue = owner.MaxSkillGauge;
        gaugeSlider.value = owner.MaxSkillGauge;

        owner.HealthComp.OnDamaged += HealthUpdate;

        owner.StatusComp.OnEffectAdded += OnEffectAdded;
        owner.StatusComp.OnEffectRemoved += OnEffectRemoved;
    }
    private void Start()
    {
        //activeIcons = new Dictionary<string, Image>();

        //healthSlider.maxValue = owner.characterBase.characterData.maxHp;
        //healthSlider.value = owner.characterBase.characterData.maxHp;

        //gaugeSlider.maxValue = owner.MaxSkillGauge;
        //gaugeSlider.value = owner.MaxSkillGauge;

        //owner.HealthComp.OnDamaged += HealthUpdate;

        //owner.StatusComp.OnEffectAdded += OnEffectAdded;
        //owner.StatusComp.OnEffectRemoved += OnEffectRemoved; //임시로 awake로 옮김
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
        if (activeIcons.ContainsKey(effect.Name))
            return;

        GameObject go = new GameObject("StatusIcon", typeof(Image));
        Image icon = go.GetComponent<Image>();

        icon.gameObject.SetActive(true);
        icon.transform.SetParent(canvas.transform, false);
        icon.rectTransform.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        icon.rectTransform.localPosition = new Vector3(-10, 3.0f, 0);
        icon.color = Color.white;

        if (statusSprites.TryGetValue(effect.Name, out Sprite sprite))
        {
            icon.sprite = sprite;
        }
        else { icon.sprite = null; }

        icon.enabled = true;
        activeIcons.TryAdd(effect.Name,icon);
    }
    private void OnEffectRemoved(StatusEffect effect)
    {
        if (!activeIcons.TryGetValue(effect.Name, out Image icon))
            return;

        icon.sprite = null;
        icon.color = new Color(1, 1, 1, 1);
        Transform.Destroy(icon.transform);
        icon.gameObject.SetActive(false);
        icon.enabled = false;

        activeIcons.Remove(effect.Name);
    }
}
