using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using UnityEngine;
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

    [SerializeField] private Button damageButton;
    [SerializeField] private Button effectButton;

    [SerializeField] private List<StatusSpritePair> spritePairs;

    private Dictionary<string, Sprite> statusSprites;
    private Dictionary<string, Image> activeIcons;

    [SerializeField] private RectTransform iconRoot;

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

        gaugeSlider.maxValue = owner.maxSkillGauge;
        gaugeSlider.value = owner.maxSkillGauge;

        owner.HealthComp.OnDamaged += HealthUpdate;

        owner.StatusComp.OnEffectAdded += OnEffectAdded;
        owner.StatusComp.OnEffectRemoved += OnEffectRemoved;

        if(!iconRoot)
        {
            var go = new GameObject("IconRoot", typeof(RectTransform), typeof(HorizontalLayoutGroup), typeof(ContentSizeFitter));
            iconRoot = go.GetComponent<RectTransform>();
            iconRoot.SetParent(canvas.transform, false);
            iconRoot.anchorMin = new Vector2(0, 1);
            iconRoot.anchorMax = new Vector2(0, 1);
            iconRoot.pivot = new Vector2(0, 1);
            iconRoot.anchoredPosition = new Vector2(-40f, 8f); // 머리 위 기준 좌표(원래 쓰던 값)

            var h = go.GetComponent<HorizontalLayoutGroup>();
            h.spacing = 2f;                    // 아이콘 간 간격
            h.childAlignment = TextAnchor.UpperLeft;
            h.childForceExpandWidth = false;
            h.childForceExpandHeight = false;

            var fitter = go.GetComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }
    private void Start()
    {
        if (!damageButton)
            damageButton = GetComponentInChildren<Button>(true); // 자식에서 버튼 찾기

        if (damageButton)
        {
            damageButton.onClick.RemoveAllListeners();
            damageButton.onClick.AddListener(OnClick_Damage);
        }

        if (!effectButton)
            effectButton = GetComponentInChildren<Button>(true);

        if (effectButton)
        {
            effectButton.onClick.RemoveAllListeners();
            effectButton.onClick.AddListener(OnClick_Effect);
        }
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
        //if (activeIcons.ContainsKey(effect.Name))
        //    return;

        GameObject go = new GameObject($"StatusIcon_{effect.Name}", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.SetParent(iconRoot, false);
        rt.sizeDelta = new Vector2(10, 5);

        Image icon = go.GetComponent<Image>();
        icon.preserveAspect = true;
        icon.color = Color.white;
        icon.rectTransform.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        if (statusSprites.TryGetValue(effect.Name, out Sprite sprite))
        {
            icon.sprite = sprite;
        }

        activeIcons.TryAdd(effect.Name,icon);

        LayoutRebuilder.ForceRebuildLayoutImmediate(iconRoot);
    }
    private void OnEffectRemoved(StatusEffect effect)
    {
        if (!activeIcons.TryGetValue(effect.Name, out Image icon))
            return;

        Destroy(icon.gameObject);
        activeIcons.Remove(effect.Name);

        LayoutRebuilder.ForceRebuildLayoutImmediate(iconRoot);
    }
    public void OnClick_Damage()
    {
        owner.HealthComp.TakeDamage(5);
    }
    public void OnClick_Effect()
    {
        StatusEffect effect = new StatusEffect();
        effect.Name = "Burn";
        effect.Stack = 3;
        effect.RemainsTurn = 3;
        effect.statusEffect = new BurnEffect();
        owner.StatusComp.AddEffect(effect);
    }
}
