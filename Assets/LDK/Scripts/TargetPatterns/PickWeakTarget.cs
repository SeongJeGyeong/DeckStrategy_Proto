using UnityEngine;
using Utils.Enums;
using System.Collections.Generic;

public class PickWeakTarget : AttackTargetSelectorBase
{
    public PickWeakTarget(BattleSystem battle) : base(battle) { }

    public override LineupSlot SelectTarget(Character Attacker)
    {
        if (battle == null || Attacker == null)
            return null;

        List<LineupSlot> slots = GetAliveTargetList(Attacker);
        if (slots == null)
            return null;

        Utils.Enums.EAttributeType type = Attacker.characterData.type;

        switch (type)
        {
            case EAttributeType.ROCK: //�������� type�� ������ ������ �����ִ��� 5��(Front)���� Ȯ����
                {
                    for (int i = slots.Count - 1; i >= 0; i--)
                    {
                        if (slots[i].character.characterData.type == EAttributeType.SCISSORS)
                        {
                            return slots[i];
                        }
                    }
                }
                break;//goto case EAttributeType.PAPER; //������ ������밡 ������ �ٸ� ��� ����
            case EAttributeType.PAPER:
                {
                    for (int i = slots.Count - 1; i >= 0; i--)
                    {
                        if (slots[i].character.characterData.type == EAttributeType.ROCK)
                        {
                            return slots[i];
                        }
                    }
                }
                break;//goto case EAttributeType.SCISSORS;
            case EAttributeType.SCISSORS:
                {
                    for (int i = slots.Count - 1; i >= 0; i--)
                    {
                        if (slots[i].character.characterData.type == EAttributeType.PAPER)
                        {
                            return slots[i];
                        }
                    }
                }
                break; //goto case EAttributeType.ROCK;
        }

        return null;
    }
}
