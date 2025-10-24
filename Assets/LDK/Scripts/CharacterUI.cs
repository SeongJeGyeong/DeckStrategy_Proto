using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Character;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public struct StatusSpritePair
{
    public string Name;
    public Sprite Sprite;
}

public class CharacterUI : MonoBehaviour
{
    private Character owner;
    //[SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform Panel;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider gaugeSlider;

    [SerializeField] private Button damageButton;
    [SerializeField] private Button effectButton;
    [SerializeField] private Button attackButton;

    [SerializeField] private List<StatusSpritePair> spritePairs;

    private Dictionary<string, Sprite> statusSprites;
    private Dictionary<string, Image> activeIcons;

    private Camera mainCamera;
    private RectTransform rectTransform;
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 2f, 0);


    public void Init(Character character)
    {
        Debug.Log("Init");

        owner = character.GetComponent<Character>();

        activeIcons = new Dictionary<string, Image>();
        healthSlider.maxValue = owner.characterData.maxHp;
        healthSlider.value = owner.characterData.maxHp;

        gaugeSlider.maxValue = owner.maxSkillGauge;
        gaugeSlider.value = owner.maxSkillGauge;

        owner.HealthComp.OnDamaged += HealthUpdate;

        owner.StatusComp.OnEffectAdded += OnEffectAdded;
        owner.StatusComp.OnEffectRemoved += OnEffectRemoved; 
    }
    private void Awake()
    {
        statusSprites = new Dictionary<string, Sprite>();
        foreach (var pair in spritePairs)
        {
            if (!statusSprites.ContainsKey(pair.Name))
                statusSprites.Add(pair.Name, pair.Sprite);
        }

        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        //if (!damageButton)
        //    damageButton = GetComponentInChildren<Button>(true); // 자식에서 버튼 찾기

        if (damageButton)
        {
            damageButton.onClick.RemoveAllListeners();
            damageButton.onClick.AddListener(OnClick_Damage);
        }

        //if (!effectButton)
        //    effectButton = GetComponentInChildren<Button>(true);

        if (effectButton)
        {
            effectButton.onClick.RemoveAllListeners();
            effectButton.onClick.AddListener(OnClick_Effect);
        }

        //if (!attackButton)
        //    attackButton = GetComponentInChildren<Button>(true);

        if (attackButton)
        {
            attackButton.onClick.RemoveAllListeners();
            attackButton.onClick.AddListener(OnClick_Attack);
        }
    }

    private void HealthUpdate(float CurrHp)
    {
        healthSlider.value = CurrHp;
    }
    private void GaugeUpdate(float CurrGauge)
    {
        gaugeSlider.value = CurrGauge;
    }
    private void OnEffectAdded(StatusEffect effect)
    {
        if (activeIcons.TryGetValue(effect.Name,out Image image))
        {
            image.GetComponentInChildren<TextMeshProUGUI>().text = $"Stack : {effect.Stack}";
            return;
        }

        GameObject go = new GameObject("StatusIcon", typeof(Image));
        Image icon = go.GetComponent<Image>();

        icon.gameObject.SetActive(true);
        icon.transform.SetParent(Panel, false);
        icon.rectTransform.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        icon.color = Color.white;

        //GameObject Text = new GameObject("StackText", typeof(RectTransform), typeof(TextMeshProUGUI));
        //Text text = Text.GetComponent<Text>();

        //text.transform.SetParent(icon.rectTransform, false);
        //text.fontSize = 36;
        //text.alignment = TextAnchor.MiddleCenter;
        //text.minWidth = 200;
        //text.minHeight = 50;

        var textGO = new GameObject("Text (TMP)", typeof(RectTransform), typeof(TextMeshProUGUI));
        textGO.transform.SetParent(icon.transform, false);

        var rt = (RectTransform)textGO.transform;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(200f, 64f);
        rt.anchoredPosition = new Vector2(0f, 32f);

        var label = textGO.GetComponent<TextMeshProUGUI>();
        label.text = $"Stack : {effect.Stack}";
        label.fontSize = 36;
        label.alignment = TextAlignmentOptions.Midline;
        label.overflowMode = TextOverflowModes.Overflow;
        label.raycastTarget = false;
        label.color = Color.red;

        // 3) 아이콘 사전에 등록(라벨 업데이트용 접근 경로 확보)
        activeIcons.TryAdd(effect.Name, icon);

        if (statusSprites.TryGetValue(effect.Name, out Sprite sprite))
        {
            icon.sprite = sprite;
        }
        else { icon.sprite = null; }

        icon.enabled = true;
        activeIcons.TryAdd(effect.Name, icon);
    }
    private void OnEffectRemoved(StatusEffect effect)
    {
        if (!activeIcons.TryGetValue(effect.Name, out Image icon))
            return;

        Destroy(icon.gameObject);
        activeIcons.Remove(effect.Name);
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
    public void OnClick_Attack()
    {
        owner.AtackComp.Attack();
    }

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        rectTransform.position = screenPos;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        gameObject.SetActive(true);
        LineupSlot slot = target.GetComponent<LineupSlot>();
    }

    public void ReleaseTarget()
    {
        gameObject.SetActive(false);
        target = null;
    }
}
