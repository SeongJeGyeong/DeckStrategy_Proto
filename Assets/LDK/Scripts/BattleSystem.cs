using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Team team;
    public GameObject[] friendlySlots = new GameObject[5];
    public GameObject[] enemySlots = new GameObject[1];

    [SerializeField] private RectTransform panel;

    private List<Character> battleSequence = new List<Character>();
    private List<Image> sequenceImage = new List<Image>();

    [SerializeField]
    private GameObject iconPrefab;

    private void Start()
    {
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            CharacterBase freindlyBase = team.characters[i];
            friendlySlot.SetSelectedCharacter(freindlyBase, false);
        }

        LineupSlot enemySlot = enemySlots[0].GetComponent<LineupSlot>();
        CharacterBase enemyBase = team.characters[5];
        enemySlot.SetSelectedCharacter(enemyBase, true);
    }

public void Resort()
    {
        battleSequence.Clear();
        sequenceImage.Clear();

        for (int i = 0; i < slots.Length; i++)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            battleSequence.Add(slot.model);
        }

        battleSequence.Sort((x,y)=>x.characterBase.characterData.speed.CompareTo(y.characterBase.characterData.speed));
        battleSequence.Reverse();

        for(int i = 0; i < battleSequence.Count; i++)
        {
            var Icon = Instantiate(iconPrefab, panel);
            var portrait = Icon.transform.Find("Portrait")?.GetComponent<UnityEngine.UI.Image>();
            var mat = battleSequence[i].characterBase.characterModelData.material;
            portrait.color = mat.color;
        }
    }
    public void BattleStart()
    {

    }
}
