using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Utils.Enums;
using static Character;

public class Character : MonoBehaviour
{
    public struct StatusEffect
    {
        public IStatusEffect statusEffect;
        public string Name; //이름 같으면
        public int Stack; // 스택 수 +
        public int RemainsTurn; // 이전부여된 라운드 수 < 이번에부여된 라운드 수 일 경우 이번에부여된 라운드 수로 덮어씌운다.
    }

    private HealthComponent healthComp;
    private StatusEffectComponent statusEffectComp;
    private AttackComponent attackComp;
    private ScoreComponent scoreComp;
    public HealthComponent HealthComp => healthComp;
    public StatusEffectComponent StatusEffectComp => statusEffectComp;
    public AttackComponent AtackComp => attackComp;
    public ScoreComponent ScoreComp => scoreComp;

    public OwnedCharacterInfo characterInfo;

    public CharacterData characterData { get; private set; }
    public CharacterModelData characterModelData { get; private set; }

    public float maxSkillGauge { get; private set; }

    public bool isEnemy = false;
    public bool isAlive = true;
    public float combatPower { get; private set; }

    [SerializeField]
    GameObject characterUIPrefab;

    CharacterInfoUI characterFollowUI;
    CharacterUI chracterBattleUI;

    private void Awake()
    {
        healthComp = GetComponent<HealthComponent>();
        statusEffectComp = GetComponent<StatusEffectComponent>();
        attackComp = GetComponent<AttackComponent>();
        scoreComp = GetComponent<ScoreComponent>();

        Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        foreach (var canvas in canvases)
        {
            if (canvas.CompareTag("CharacterUI"))
            {
                GameObject ui = Instantiate(characterUIPrefab, canvas.transform);
                characterFollowUI = ui.GetComponentInChildren<CharacterInfoUI>();
                characterFollowUI.SetTarget(transform);
                characterFollowUI.gameObject.SetActive(false);

                chracterBattleUI = ui.GetComponentInChildren<CharacterUI>();
                chracterBattleUI.Init(this);
                chracterBattleUI.SetTarget(transform);
                chracterBattleUI.gameObject.SetActive(false);

                break;
            }
        }
    }

    public void EndTurn()
    {
        statusEffectComp.TurnAll();
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

    public void SetCharacterData(OwnedCharacterInfo info)
    {
        DataCenter dataCenter = FindAnyObjectByType<DataCenter>();
        if (dataCenter == null) return;

        CharacterData data = dataCenter.FindCharacterData(info.characterID);
        CharacterModelData modelData = dataCenter.FindCharacterModel(info.characterModelID);

        if (data == null || modelData == null) return;

        characterInfo = info;
        HealthComp.SetHp(data.maxHp);

        characterData = data;
        characterModelData = modelData;
        GetComponent<MeshRenderer>().material.color = modelData.material.color;
        gameObject.SetActive(true);
        if (characterFollowUI != null)
        {
            characterFollowUI.SetCharacterInfo(data.type, info.characterLevel);
            characterFollowUI.gameObject.SetActive(true);
        }
        if (chracterBattleUI != null)
        {
        }

        UpdateCombatPower();
    }

    public void ClearCharacterInfo()
    {
        characterData = null;
        characterModelData = null;
        gameObject.SetActive(false);
        if (characterFollowUI != null)
        {
            characterFollowUI.gameObject.SetActive(false);
        }

        UpdateCombatPower();
    }

    public void ActiveBattleUI()
    {
        chracterBattleUI.Init(this);
        chracterBattleUI.gameObject.SetActive(true);
        characterFollowUI.gameObject.SetActive(false);
    }
}
